﻿using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Threading;
using MahApps.Metro.Controls.Dialogs;
using PrintIt_Desktop_4.Model.Abstractions;
using PrintIt_Desktop_4.Model.Configuration;
using PrintIt_Desktop_4.Model.Core.Networking;
using PrintIt_Desktop_4.Model.Enums;
using PrintIt_Desktop_4.Other;

namespace PrintIt_Desktop_4.ViewModels
{
    [Magic]
    class LoadAndLoginViewModel:ViewModelBase
    {
        #region private members
        private IDialogCoordinator _dialogCoordinator;
        private DispatcherTimer _timer = new DispatcherTimer() { Interval = new TimeSpan(0, 0, 0, 3), IsEnabled = false };
        #endregion

        #region constructors
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
            ToTop = SystemParameters.WorkArea.Height - LoginHeight;
            ToLeft = SystemParameters.WorkArea.Width - LoginWidth;
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
        #endregion

        #region properties
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
        public String SignInLogin { get; set; }
        public String SignUpLogin { get; set; }
        public String SignUpName { get; set; }
        public String State { get; set; }
        public SizeToContent SizeMode { get; set; }
        public bool ProgressRingActive { get; set; }
        public ICommand Loaded { get; set; }
        public ICommand MoveCompleated { get; set; }
        public ICommand HideCompleated { get; set; }
        public ICommand TransformCompleated { get; set; }
        #endregion

        #region interactions
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
            pVal = Config.GetPasswordValidator();
            lVal = Config.GetLoginValidator();
            var pValRes = pVal.Validate(password);
            var lValRes = lVal.Validate(SignUpLogin);
            foreach (var loginValidationState in lValRes)
            {
                if(loginValidationState!=LoginValidationState.Valid)
                errorMessages.Add(Localizer.GetString(loginValidationState));
            }
            foreach (var passwordValidationState in pValRes)
            {
                if(passwordValidationState!=PasswordValidationState.Valid)
                errorMessages.Add(Localizer.GetString(passwordValidationState));
            }
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

            pVal = Config.GetPasswordValidator();
            lVal = Config.GetLoginValidator();
            var pValRes = pVal.Validate(password);
            var lValRes = lVal.Validate(SignInLogin);
            foreach (var loginValidationState in lValRes)
            {
                if (loginValidationState != LoginValidationState.Valid)
                errorMessages.Add(Localizer.GetString(loginValidationState));
            }
            foreach (var passwordValidationState in pValRes)
            {
                if (passwordValidationState != PasswordValidationState.Valid)
                errorMessages.Add(Localizer.GetString(passwordValidationState));
            }

            }
            if (errorMessages.Count > 0) ShowErrorMessage(errorMessages);
            else
            {
                var data = new NameValueCollection();
                try
                {
                    data.Add(Config.GetSignInLoginParamName(), SignInLogin);
                    data.Add(Config.GetSignInPasswordParamName(), password);
                    var response = NetworkManager.SendPostRequest(data, Config.GetSignIn());
                    MessageBox.Show(response);
                }
                catch (Exception ex)
                {
                    ShowErrorMessage(new List<string>() { "Неможливо зв'язатися з сервером "});
                    MessageBox.Show(ex.Message);
                }
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
        #endregion

        #region animation finish behaviour
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

        private void LoadEnd()
        {
            ProgressRingActive = true;
            _timer.IsEnabled = true;
            _timer.Start();
        }

        private void MoveEnd()
        {
            State = "FinalTransform";
        }

        private void HideEnd()
        {
            SplashScreenVisibility = Visibility.Collapsed;
            ProgressRingActive = false;
            LoginVisibility = Visibility.Visible;
            State = "MoveBegin";
        }

        private void TransformEnd()
        {
            SizeMode = SizeToContent.Manual;
            Width=LoginWidth;
            Height=LoginHeight;
            ButtonsEnabled = true;
            WindowResizeMode = ResizeMode.CanMinimize;

            if (!NetworkManager.CanStartSession(Config.GetServerName()))
            ShowErrorMessage(new List<string>(){"Неможливо зв'язатися з сервером"});
        }
        #endregion
    }
}
