using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Fido2NetLib;
using Fido2NetLib.Objects;
using RO.Common3;
using RO.Common3.Data;
using System.Threading.Tasks;

namespace RO.WebRules
{
    public class WebAuthnAssertion
    {
        public bool verified { get; set; }
        public string challenge { get; set; }
        public string credentialId { get; set; }
        public string publicKey { get; set; }
        public uint signingCounter { get; set; }
        public Guid aaguid { get; set; }
        public byte[] userHandle { get; set; }

    }

    /// <summary>
    /// Summary description for WebAuthn
    /// </summary>
    public class WebAuthn
    {
        public static string MakeWebAuthnAssertionRequest(Fido2Configuration fido2Config, byte[] challenge, List<PublicKeyCredentialDescriptor> allowedCredentials)
        {
            AuthenticatorSelection authSelection = new AuthenticatorSelection
            {
                RequireResidentKey = false,
                UserVerification = UserVerificationRequirement.Preferred,
                //                AuthenticatorAttachment = null,
            };
            AuthenticationExtensionsClientInputs clientExtensions = new AuthenticationExtensionsClientInputs
            {
                Extensions = true,
                SimpleTransactionAuthorization = string.Format("you are registering to {0}", fido2Config.ServerName),
                Location = true,
                UserVerificationMethod = true,
            };

            var fido2 = new Fido2(fido2Config);

            var assertionRequest = Fido2NetLib.AssertionOptions.Create(fido2Config, challenge, allowedCredentials, UserVerificationRequirement.Preferred, clientExtensions);
            string assertionRequestJson = assertionRequest.ToJson();
            return assertionRequestJson;
        }
        public static string MakeWebAuthnAttestationRequest(Fido2Configuration fido2Config, byte[] challenge, LoginUsr LUser, List<PublicKeyCredentialDescriptor> excludedCredentials)
        {

            string usrId = LUser.UsrId.ToString();
            string usrIdB64 = System.Convert.ToBase64String(usrId.ToUtf8ByteArray());
            Fido2User user = new Fido2User
            {
                DisplayName = LUser.UsrName,
                /* must be restricted to no more than than 64 for device like yubikey as it would fail without reason */
                //Name = (Guid.NewGuid().ToString() + " " + DateTime.UtcNow.ToString("o")).Left(64),
                //Id= Guid.NewGuid().ToString().ToUtf8ByteArray()
                Name = LUser.LoginName,
                Id = usrIdB64.ToUtf8ByteArray()
            };
            AuthenticatorSelection authenticatorSelection = new AuthenticatorSelection
            {
                RequireResidentKey = false,
                UserVerification = UserVerificationRequirement.Discouraged,
                //                 AuthenticatorAttachment = AuthenticatorAttachment.Platform,
            };
            AttestationConveyancePreference attConveyancePreference = AttestationConveyancePreference.None;
            AuthenticationExtensionsClientInputs clientExtensions = new AuthenticationExtensionsClientInputs
            {
                Extensions = true,
                SimpleTransactionAuthorization = string.Format("you are registering to {0}", fido2Config.ServerName),
                Location = true,
                UserVerificationMethod = true,
                BiometricAuthenticatorPerformanceBounds = new AuthenticatorBiometricPerfBounds
                {
                    FAR = float.MaxValue,
                    FRR = float.MaxValue
                }
            };

            var fido2 = new Fido2(fido2Config);

            // must do this for the verification to work
            var options = fido2.RequestNewCredential(user, excludedCredentials, authenticatorSelection, attConveyancePreference, clientExtensions);
            // the challenge is random byte but we need more info, replace it
            options.Challenge = challenge;
            var createRequest = Fido2NetLib.CredentialCreateOptions.Create(fido2Config
                , challenge, user, authenticatorSelection, attConveyancePreference
                , excludedCredentials != null && excludedCredentials.Count > 0 ? excludedCredentials : null
                , clientExtensions);
            string createRequestJson = options.ToJson();
            return createRequestJson;
        }
        public static WebAuthnAssertion VerifyAttestationResult(Fido2Configuration fido2Config, string requestJSON, string resultJSON)
        {
            var fido2 = new Fido2(fido2Config);
            IsCredentialIdUniqueToUserAsyncDelegate callback = (IsCredentialIdUniqueToUserParams args) =>
            {
                var id = args.CredentialId; // generated ID by authenticator(should be always unique for each authenticator, equivalent to client cert)
                var u = args.User;          // user info, kind of useless as this can be changed by js at client side
                return Task.FromResult(true);
            };
            var request = Newtonsoft.Json.JsonConvert.DeserializeObject<CredentialCreateOptions>(requestJSON);
            AuthenticatorAttestationRawResponse regResponse = Newtonsoft.Json.JsonConvert.DeserializeObject<AuthenticatorAttestationRawResponse>(resultJSON);
            var success = fido2.MakeNewCredentialAsync(regResponse, request, callback).Result;
            var clientDataJson = Newtonsoft.Json.JsonConvert.DeserializeObject<AuthenticatorResponse>(System.Text.UTF8Encoding.UTF8.GetString(regResponse.Response.ClientDataJson));
            var challenge = System.Convert.ToBase64String(clientDataJson.Challenge);
            var credentialId = System.Convert.ToBase64String(success.Result.CredentialId);
            var publicKey = System.Convert.ToBase64String(success.Result.PublicKey);
            var signingCounter = success.Result.Counter; // designed for replay attact prevention but useless for a multiple node situation
            var user = success.Result.User;
            var aaguid = success.Result.Aaguid;

            return new WebAuthnAssertion
            {
                verified = true,
                challenge = challenge,
                credentialId = credentialId,
                publicKey = publicKey,
                signingCounter = signingCounter,
                aaguid = aaguid,
                userHandle = request.User.Id
            };
        }
        public static WebAuthnAssertion VerifyAssertionResult(Fido2Configuration fido2Config, string requestJSON, string resultJSON, Func<string, byte[]> getStoredPublicKey)
        {

            var fido2 = new Fido2(fido2Config);

            IsUserHandleOwnerOfCredentialIdAsync callback = (args) =>
            {
                var u = args.UserHandle;
                var id = args.CredentialId;
                return Task.FromResult(true);
            };

            var request = Newtonsoft.Json.JsonConvert.DeserializeObject<AssertionOptions>(requestJSON);
            AuthenticatorAssertionRawResponse assertionResponse = Newtonsoft.Json.JsonConvert.DeserializeObject<AuthenticatorAssertionRawResponse>(resultJSON);
            var credentialId = System.Convert.ToBase64String(assertionResponse.Id);
            uint storedCounter = 0; // 0 means always success
            var publicKey = getStoredPublicKey(credentialId);
            var success = fido2.MakeAssertionAsync(assertionResponse, request, publicKey, storedCounter, callback).Result;
            var clientDataJson = Newtonsoft.Json.JsonConvert.DeserializeObject<AuthenticatorResponse>(System.Text.UTF8Encoding.UTF8.GetString(assertionResponse.Response.ClientDataJson));
            var challenge = clientDataJson.Challenge;

            return new WebAuthnAssertion
            {
                verified = true,
                challenge = System.Convert.ToBase64String(clientDataJson.Challenge),
                credentialId = credentialId,
                publicKey = System.Convert.ToBase64String(publicKey),
                userHandle = assertionResponse.Response.UserHandle
            };
        }
        public static WebAuthnAssertion GetAssertionIdInfo(string resultJSON)
        {

            AuthenticatorAssertionRawResponse assertionResponse = Newtonsoft.Json.JsonConvert.DeserializeObject<AuthenticatorAssertionRawResponse>(resultJSON);
            var clientData = Newtonsoft.Json.JsonConvert.DeserializeObject<AuthenticatorResponse>(System.Text.UTF8Encoding.UTF8.GetString(assertionResponse.Response.ClientDataJson));
            var credentialId = assertionResponse.Id;
            var userHandle = assertionResponse.Response.UserHandle;
            var challenge = clientData.Challenge;
            return new WebAuthnAssertion
            {
                verified = false,
                credentialId = System.Convert.ToBase64String(credentialId),
                challenge = System.Convert.ToBase64String(challenge),
                userHandle = userHandle
            };
        }

    }
}
