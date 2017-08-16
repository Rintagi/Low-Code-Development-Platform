using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;
using System.Drawing;
using System.IO;

namespace RO.Common3
{
    public class GoogleAuthenticator
    {
        /* how to use in page 
        // generate a screen or get the secret somewhere(say from user database)
        byte[] secret = RO.Common3.GoogleAuthenticator.GetNewSecret(); 
        // generate QR Code for mobile app
        string qrUrl = RO.Common3.GoogleAuthenticator.GetQRCodeEmbeddedImg("test app abcd", secret,0);
        // expected code need to match from input, should only test once
        string code = RO.Common3.GoogleAuthenticator.CalculateOneTimePassword(secret);
        // QR Code to load into mobile app generator (only when needed)
        cQR.ImageUrl = qrUrl;
        // for manually entered in mobile app if no camera
        cSecret.Text = RO.Common3.GoogleAuthenticator.Base32Encode(secret);
         */
        public static byte[] GetNewSecret()
        {
            RandomNumberGenerator rng = new RNGCryptoServiceProvider();
            byte[] tokenData = new byte[10]; // google use 80 bit key
            rng.GetBytes(tokenData);
            return tokenData;

        }
        public static string Base32Encode(byte[] data)
        {
            const int IN_BYTE_SIZE = 8;
            const int OUT_BYTE_SIZE = 5;
            char[] alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ234567".ToCharArray();

            int i = 0, index = 0, digit = 0;
            int current_byte, next_byte;
            StringBuilder result = new StringBuilder((data.Length + 7) * IN_BYTE_SIZE / OUT_BYTE_SIZE);

            while (i < data.Length)
            {
                current_byte = (data[i] >= 0) ? data[i] : (data[i] + 256); // Unsign

                /* Is the current digit going to span a byte boundary? */
                if (index > (IN_BYTE_SIZE - OUT_BYTE_SIZE))
                {
                    if ((i + 1) < data.Length)
                        next_byte = (data[i + 1] >= 0) ? data[i + 1] : (data[i + 1] + 256);
                    else
                        next_byte = 0;

                    digit = current_byte & (0xFF >> index);
                    index = (index + OUT_BYTE_SIZE) % IN_BYTE_SIZE;
                    digit <<= index;
                    digit |= next_byte >> (IN_BYTE_SIZE - index);
                    i++;
                }
                else
                {
                    digit = (current_byte >> (IN_BYTE_SIZE - (index + OUT_BYTE_SIZE))) & 0x1F;
                    index = (index + OUT_BYTE_SIZE) % IN_BYTE_SIZE;
                    if (index == 0)
                        i++;
                }
                result.Append(alphabet[digit]);
            }

            return result.ToString();
        }
        public static string GetQRCodeUrl(string identity, byte[] secret)
        {
            // https://code.google.com/p/google-authenticator/wiki/KeyUriFormat
            string base32Secret = GetSecretCode(secret);
            string keyUri = string.Format("otpauth://totp/{0}%3Fsecret%3D{1}", System.Web.HttpUtility.UrlEncode(identity), base32Secret);
            return String.Format("https://www.google.com/chart?chs=200x200&chld=M|0&cht=qr&chl=otpauth://totp/{0}%3Fsecret%3D{1}", System.Web.HttpUtility.UrlEncode(identity), base32Secret);
            //return String.Format("https://www.google.com/chart?chs=200x200&chld=M|0&cht=qr&chl={0}", System.Web.HttpUtility.UrlEncode(keyUri));
        }
        public static string GetQRCodeEmbeddedImg(string identity, byte[] secret, QRCoder.QRCodeGenerator.ECCLevel size)
        {
            string base32Secret = GetSecretCode(secret);
            string keyUri = string.Format("otpauth://totp/{0}?secret={1}", System.Web.HttpUtility.UrlEncode(identity), base32Secret);

            QRCoder.QRCodeGenerator qrGenerator = new QRCoder.QRCodeGenerator();
            QRCoder.QRCodeData qrCodeData = qrGenerator.CreateQrCode(keyUri, size);
            QRCoder.QRCode qrCode = new QRCoder.QRCode(qrCodeData);
            Bitmap qrCodeImage = qrCode.GetGraphic(size == QRCoder.QRCodeGenerator.ECCLevel.L ? 5 : (size == QRCoder.QRCodeGenerator.ECCLevel.M || size == QRCoder.QRCodeGenerator.ECCLevel.H ? 10 : 20));
            using (MemoryStream ms = new MemoryStream())
            {
                qrCodeImage.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                byte[] png = ms.GetBuffer();
                string imageUrl = "data:image/png;base64," + Convert.ToBase64String(png);
                return imageUrl;
            }
        }
        public static string GetSecretCode(byte[] secret)
        {
            byte[] secretHash = new MD5CryptoServiceProvider().ComputeHash(secret).Take(10).ToArray();
            return Base32Encode(secretHash);
        }
        public static string CalculateOneTimePassword(byte[] secret,int slotOffset = 0)
        {
            // https://tools.ietf.org/html/rfc4226
            int steps = 30; // 30 seconds for Google Authentication
            byte[] secretHash = new MD5CryptoServiceProvider().ComputeHash(secret).Take(10).ToArray();
            Int64 UnixTimestamp = GetUnixTimestamp();
            Int64 GoodTillTimestamp = Convert.ToInt64(UnixTimestamp / steps) + slotOffset;
            int SecondsRemain = steps - Convert.ToInt32(UnixTimestamp % steps);
            var data = BitConverter.GetBytes(GoodTillTimestamp).Reverse().ToArray();
            byte[] Hmac = new HMACSHA1(secretHash).ComputeHash(data);
            int Offset = Hmac.Last() & 0x0F;
            int OneTimePassword = (
                ((Hmac[Offset + 0] & 0x7f) << 24) |
                ((Hmac[Offset + 1] & 0xff) << 16) |
                ((Hmac[Offset + 2] & 0xff) << 8) |
                (Hmac[Offset + 3] & 0xff)
                    ) % 1000000;
            return string.Format("{0:000000}", OneTimePassword);
        }
        public static Int64 GetUnixTimestamp()
        {
            return Convert.ToInt64(Math.Round((DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0)).TotalSeconds));
        }
    }
}
