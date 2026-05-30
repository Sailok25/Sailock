using System;
using System.Security.Cryptography;
using OtpNet;

namespace Sailock.Services
{
    public class TotpService
    {
        private const int SecretSize = 20;

        /// <summary>
        /// Genera una nueva clave secreta aleatoria en Base32.
        /// Se llama una sola vez al activar 2FA.
        /// </summary>
        public string GenerateSecret()
        {
            byte[] secretBytes = RandomNumberGenerator.GetBytes(SecretSize);
            return Base32Encoding.ToString(secretBytes);
        }

        /// <summary>
        /// Genera la URI para el QR code compatible con Google Authenticator.
        /// </summary>
        public string GenerateQrUri(string secret, string accountName = "Sailock")
        {
            return $"otpauth://totp/{Uri.EscapeDataString(accountName)}" +
                   $"?secret={secret}&issuer=Sailock&algorithm=SHA1&digits=6&period=30";
        }

        /// <summary>
        /// Valida el código TOTP introducido por el usuario.
        /// Acepta una ventana de ±1 período (30s) para compensar desincronía de reloj.
        /// </summary>
        public bool Validate(string secret, string code)
        {
            if (string.IsNullOrWhiteSpace(secret) || string.IsNullOrWhiteSpace(code))
                return false;

            try
            {
                byte[] secretBytes = Base32Encoding.ToBytes(secret);
                var totp = new Totp(secretBytes);
                return totp.VerifyTotp(
                    code,
                    out _,
                    new VerificationWindow(previous: 1, future: 1));
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Genera el código actual (útil para testing).
        /// </summary>
        public string GetCurrentCode(string secret)
        {
            byte[] secretBytes = Base32Encoding.ToBytes(secret);
            var totp = new Totp(secretBytes);
            return totp.ComputeTotp();
        }
    }
}