using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.RightsManagement;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Threading;
using MahApps.Metro.Controls.Dialogs;
using PrintIt_Desktop_4.Model.Abstractions;
using PrintIt_Desktop_4.Other;

namespace PrintIt_Desktop_4.ViewModels
{
    [Magic]
    class LoadAndLoginViewModel:ViewModelBase
    {
        private IDialogCoordinator _dialogCoordinator;
        private DispatcherTimer _timer = new DispatcherTimer() { Interval = new TimeSpan(0, 0, 0, 3), IsEnabled = false };
        public LoadAndLoginViewModel()
        {
            SignUpShowCommand = new DelegateCommand(ShowSignUp);
            SignUpCancelCommand = new DelegateCommand(HideSignUp);
            LoginVisibility = Visibility.Collapsed;
            SignUpVisibility = Visibility.Collapsed;
            SplashScreenVisibility = Visibility.Visible;
            _dialogCoordinator = DialogCoordinator.Instance;
            LoginCommand = new DelegateCommand<object>(SingIn);
            SignUpCommand = new DelegateCommand<object>(SingUp);
            SignUpName = "";
            SignUpLogin = "";
            SignInLogin = "";


            HeaderHeight = 0;
            ButtonsEnabled = false;
            WindowResizeMode = ResizeMode.NoResize;

            LoginWidth = 800;
            LoginHeight = 470;
            Width = 525;
            Height = 350;
            ToTop = System.Windows.SystemParameters.WorkArea.Height - LoginHeight;
            ToLeft = System.Windows.SystemParameters.WorkArea.Width - LoginWidth;
            ToTop /= 2;
            ToLeft /= 2;
            _timer.Tick += ((sender, args) =>
            {
                State = "HideSplash";
                SizeMode = SizeToContent.WidthAndHeight;
                _timer.Stop();
            });

            State = "-1";
            SizeMode = SizeToContent.Manual;
            ProgressRingActive = false;
            MoveCompleated = new DelegateCommand(MoveEnd);
            HideCompleated = new DelegateCommand(HideEnd);
            Loaded = new DelegateCommand(LoadEnd);
            TransformCompleated = new DelegateCommand(TransformEnd);
        }

        public int HeaderHeight { get; set; }
        public bool ButtonsEnabled { get; set; }
        public ResizeMode WindowResizeMode { get; set; }

        public ICommand LoginCommand { get; set; }
        public ICommand SignUpShowCommand { get; set; }
        public ICommand SignUpCommand { get; set; }
        public ICommand SignUpCancelCommand { get; set; }

        public Visibility LoginVisibility { get; set; }
        public Visibility SignUpVisibility { get; set; }
        public Visibility SplashScreenVisibility { get; set; }

        public double ToLeft { get; set; }
        public double ToTop { get; set; }
        public double LoginHeight { get; set; }
        public double LoginWidth { get; set; }

        public double Height { get; set; }
        public double Width { get; set; }

        private void ShowSignUp()
        {
            LoginVisibility = Visibility.Collapsed;
            SignUpVisibility = Visibility.Visible;
        }

        private void HideSignUp()
        {
            SignUpVisibility = Visibility.Collapsed;
            LoginVisibility = Visibility.Visible;
        }

        public String SignInLogin { get; set; }
        public String SignUpLogin { get; set; }
        public String SignUpName { get; set; }

        private void SingUp(object param)
        {
            var passwords = (Object[]) param;
            var password = (passwords[0] as PasswordBox).Password;
            var passwordRepeat = (passwords[1] as PasswordBox).Password;
            PasswordValidator pVal;
            LoginValidator lVal;
            var errorMessages = new List<String>();
            if (SignUpLogin.Equals("") || SignUpName.Equals("") || password.Equals("") || passwordRepeat.Equals(""))
                errorMessages.Add("Заповніть всі поля");
            else
            {
                //todo link to model
                /*
            pVal = Core.GetPasswordValidator();
            lVal = Core.GetLoginValidator();
            var pValRes = pVal.Validate(password);
            val lValRes = lVal.Validate(SignUpLogin);
             * for each error add it to err list
             * 
            */
                if(!password.Equals(passwordRepeat)) errorMessages.Add("Паролі відрізняються");
            }
            if(errorMessages.Count>0) ShowErrorMessage(errorMessages);
            else
            {
                //todo send SingUp request to server
            }
        }

        private void SingIn(object param)
        {
            var password = (param as PasswordBox).Password;
            PasswordValidator pVal;
            LoginValidator lVal;
            var errorMessages = new List<String>();
            if (SignInLogin.Equals("") || password.Equals(""))
                errorMessages.Add("Заповніть всі поля");
            else
            {
                //todo link to model
                /*
            pVal = Core.GetPasswordValidator();
            lVal = Core.GetLoginValidator();
            var pValRes = pVal.Validate(password);
            val lValRes = lVal.Validate(SignInLogin);
             * for each error add it to err list
             * 
            */
            }
            if (errorMessages.Count > 0) ShowErrorMessage(errorMessages);
            else
            {
                //todo send SingIn request to server
            }
        }

        private async void ShowErrorMessage(List<String> errors)
        {
            var sb = new StringBuilder("");
            foreach (var error in errors)
            {
                sb.AppendLine(error);
            }
            var dialogSettings = new MetroDialogSettings()
            {
                ColorScheme = MetroDialogColorScheme.Inverted
            };
            await _dialogCoordinator.ShowMessageAsync(this, errors.Count>1?"Помилки":"Помилка", sb.ToString(),MessageDialogStyle.Affirmative,dialogSettings);
        }

        public String State { get; set; }
        public SizeToContent SizeMode { get; set; }
        public bool ProgressRingActive { get; set; }
        public ICommand Loaded { get; set; }

        private void LoadEnd()
        {
            ProgressRingActive = true;
            _timer.IsEnabled = true;
            _timer.Start();
        }

        public ICommand MoveCompleated { get; set; }

        private void MoveEnd()
        {
            State = "FinalTransform";
        }

        public ICommand HideCompleated { get; set; }

        private void HideEnd()
        {
            SplashScreenVisibility = Visibility.Collapsed;
            ProgressRingActive = false;
            LoginVisibility = Visibility.Visible;
            State = "MoveBegin";
        }

        public ICommand TransformCompleated { get; set; }

        private void TransformEnd()
        {
            SizeMode = SizeToContent.Manual;
            Width=LoginWidth;
            Height=LoginHeight;
            ButtonsEnabled = true;
            WindowResizeMode = ResizeMode.CanMinimize;

            
        }
    }
}
