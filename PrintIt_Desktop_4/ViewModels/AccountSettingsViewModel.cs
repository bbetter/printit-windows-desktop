using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;
using PrintIt_Desktop_4.Model.Core;
using PrintIt_Desktop_4.Other;

namespace PrintIt_Desktop_4.ViewModels
{
    [Magic]
    public class AccountSettingsViewModel:ViewModelBase
    {
        public AccountSettingsViewModel()
        {
            SaveCommand = new DelegateCommand(Save);
            CancelCommand = new DelegateCommand(Cancel);
            info = CurrentState.PrintSpotParameters;
            MapFrom();
        }

        public ICommand SaveCommand { get; set; }
        public ICommand CancelCommand { get; set; }

        private void Save()
        {
            MapTo();
        }

        private void Cancel()
        {
            info = CurrentState.PrintSpotParameters;
            MapFrom();
        }

        private void MapTo()
        {

        }

        private void MapFrom()
        {
            PrintSpotName = info.PrintSpotName;
            PrintSpotAddress = info.PrintSpotAddress;
            Description = info.Description;
            AdditionalInfo = info.AdditionalInfo;

        }

        public String PrintSpotName { get; set; }
        public String PrintSpotAddress { get; set; }
        public String OwnerName { get; set; }
        public String OwnerSoname { get; set; }

        public String Description { get; set; }
        public String AdditionalInfo { get; set; }
        public String ImageURI { get; set; }
        public String Status { get; set; }
        public bool Available { get; set; }

        private PrintSpotInfo info;
    }
}
