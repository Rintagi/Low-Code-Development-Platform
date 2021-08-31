using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// Required for all cognito examples
using Amazon;
using Amazon.CognitoIdentity;
using Amazon.CognitoIdentityProvider;
using Amazon.Extensions.CognitoAuthentication;
using Amazon.Runtime;


namespace RO.Common3.AwsClient
{
    public class AwsCognitoUser
    {
        public string userId = "gary.ng@robocoder.com";
        public string clientId = "859dg3djdknurqbpu1l639k1f";
        public string poolId = "ap-southeast-1_fG99nG2fq";
        public RegionEndpoint regionEndpoint;
        public AuthFlowResponse authResponse;
        public string refreshToken { get; set; }
        public DateTime goodTill { get; private set; }
        public string accessToken { get; set; }

        public AwsCognitoUser(){}

        public AwsCognitoUser(string _usrId, string _clientId, string _poolId, RegionEndpoint _regionEndpoint = null)
        {
            userId = _usrId;
            clientId = _clientId;
            poolId = _poolId;
            regionEndpoint = _regionEndpoint ?? FallbackRegionFactory.GetRegionEndpoint();
        }
        public async Task<string> GetCredsAsync(string userPassword)
        {
            AmazonCognitoIdentityProviderClient provider =
                new AmazonCognitoIdentityProviderClient(new Amazon.Runtime.AnonymousAWSCredentials(), regionEndpoint);
            CognitoUserPool userPool = new CognitoUserPool(poolId, clientId, provider);
            CognitoUser user = new CognitoUser(userId, clientId, userPool, provider);
            InitiateSrpAuthRequest authRequest = new InitiateSrpAuthRequest()
            {
                Password = userPassword
            };

            try
            {

                authResponse = await user.StartWithSrpAuthAsync(authRequest).ConfigureAwait(false);
                accessToken = authResponse.AuthenticationResult.AccessToken;
                refreshToken = authResponse.AuthenticationResult.RefreshToken;
                goodTill = DateTime.UtcNow.AddSeconds(authResponse.AuthenticationResult.ExpiresIn);

                return accessToken;
            }
            catch (Exception ex)
            {
                var x = ex.Message;
                throw;
            }
        }
        public async Task<string> GetAccessToken()
        {
            if (accessToken == null)
            {
                throw new Exception("need to login first");
            }

            if (DateTime.UtcNow >= goodTill)
            {
                await RefreshCredsAsync();
            }
            return accessToken;
        }

        public async Task<string> RefreshCredsAsync()
        {
            AmazonCognitoIdentityProviderClient provider = new AmazonCognitoIdentityProviderClient
                            (new AnonymousAWSCredentials(), regionEndpoint);
            CognitoUserPool userPool = new CognitoUserPool(poolId, clientId, provider);

            CognitoUser user = new CognitoUser(userId, clientId, userPool, provider);

            user.SessionTokens = new CognitoUserSession(null, null, authResponse.AuthenticationResult.RefreshToken, DateTime.Now, DateTime.Now.AddHours(1));

            InitiateRefreshTokenAuthRequest refreshRequest = new InitiateRefreshTokenAuthRequest()
            {
                AuthFlowType = AuthFlowType.REFRESH_TOKEN_AUTH
            };
            try
            {
                authResponse = await user.StartWithRefreshTokenAuthAsync(refreshRequest).ConfigureAwait(false);
                accessToken = authResponse.AuthenticationResult.AccessToken;
                refreshToken = authResponse.AuthenticationResult.RefreshToken;
                goodTill = DateTime.UtcNow.AddSeconds(authResponse.AuthenticationResult.ExpiresIn);
                return accessToken;
            }
            catch (Exception ex)
            {
                var x = ex.Message;
                throw;
            }
        }
    }
}
