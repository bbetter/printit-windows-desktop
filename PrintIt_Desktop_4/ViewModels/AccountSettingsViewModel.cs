using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using PrintIt_Desktop_4.Model.Configuration;
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
            OwnerName = info.OwnerName;
            OwnerSoname = info.OwnerSoname;
            ImageURI = Config.Networking.GetServerAddress() + info.ImageURI;
            Image = new BitmapImage();
            if(!String.IsNullOrEmpty(ImageURI))
                SetupImage();
            Available = !(info.Status == "offline" || info.Status == "on_maitanance");
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
        public BitmapImage Image { get; set; }

        private void SetupImage()
        {
            try
            {
                int BytesToRead = 100;

                WebRequest request = WebRequest.Create(new Uri(ImageURI, UriKind.Absolute));
                request.Timeout = -1;
                WebResponse response = request.GetResponse();
                Stream responseStream = response.GetResponseStream();
                BinaryReader reader = new BinaryReader(responseStream);
                MemoryStream memoryStream = new MemoryStream();

                byte[] bytebuffer = new byte[BytesToRead];
                int bytesRead = reader.Read(bytebuffer, 0, BytesToRead);

                while (bytesRead > 0)
                {
                    memoryStream.Write(bytebuffer, 0, bytesRead);
                    bytesRead = reader.Read(bytebuffer, 0, BytesToRead);
                }
                var image = new BitmapImage();
                image.BeginInit();
                memoryStream.Seek(0, SeekOrigin.Begin);

                image.StreamSource = memoryStream;
                image.EndInit();
                Image = image;
            }
            catch (Exception ex)
            {
                Image = new BitmapImage(new Uri(@"..\..\Images\printer_RGBA.png",UriKind.RelativeOrAbsolute));
                //MessageBox.Show(ex.Message);
            }
        }
    }
}
