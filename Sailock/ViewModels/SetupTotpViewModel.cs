using System.Windows.Input;
using System.Windows.Media.Imaging;
using Sailock.Helpers;
using Sailock.Models;
using Sailock.Services;

namespace Sailock.ViewModels
{
    public class SetupTotpViewModel : ViewModelBase
    {
        private readonly TotpService _totp = new TotpService();
        private readonly StorageService _storage;
        private readonly AppData _appData;
        private readonly string _masterPassword;

        public string Secret { get; }

        private BitmapImage _qrImage;
        public BitmapImage QrImage
        {
            get => _qrImage;
            set => SetProperty(ref _qrImage, value);
        }

        private string _verificationCode;
        public string VerificationCode
        {
            get => _verificationCode;
            set => SetProperty(ref _verificationCode, value);
        }

        private string _errorMessage;
        public string ErrorMessage
        {
            get => _errorMessage;
            set => SetProperty(ref _errorMessage, value);
        }

        private bool _isVerified;
        public bool IsVerified
        {
            get => _isVerified;
            set => SetProperty(ref _isVerified, value);
        }

        public System.Action OnSetupComplete { get; set; }
        public System.Action OnCancelled { get; set; }

        public ICommand VerifyCommand { get; }
        public ICommand CancelCommand { get; }

        public SetupTotpViewModel(AppData appData, StorageService storage, string masterPassword)
        {
            _appData = appData;
            _storage = storage;
            _masterPassword = masterPassword;

            // Generar nueva clave secreta
            Secret = _totp.GenerateSecret();

            // Generar QR
            string uri = _totp.GenerateQrUri(Secret);
            QrImage = QrCodeHelper.GenerateQrBitmap(uri);

            VerifyCommand = new RelayCommand(_ => Verify());
            CancelCommand = new RelayCommand(_ => OnCancelled?.Invoke());
        }

        private void Verify()
        {
            if (!_totp.Validate(Secret, VerificationCode))
            {
                ErrorMessage = "Código incorrecto. Inténtalo de nuevo.";
                return;
            }

            // Guardar secret en AppData y persistir
            _appData.Settings.TotpSecret = Secret;
            _appData.Settings.Is2FAEnabled = true;
            _storage.Save(_appData, _masterPassword);

            IsVerified = true;
            OnSetupComplete?.Invoke();
        }
    }
}