using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Input;
using PrintIt_Desktop_4.Model.Configuration;
using PrintIt_Desktop_4.Model.Core;
using PrintIt_Desktop_4.Model.Core.Networking;
using PrintIt_Desktop_4.Model.Core.Printing;
using PrintIt_Desktop_4.Model.Enums;
using PrintIt_Desktop_4.UserControls;

namespace PrintIt_Desktop_4.ViewModels
{
    public class DocumentListViewModel:ViewModelBase
    {
        public DocumentListViewModel()
        {
            MessageHandler.OnDocumentAdd += AddDocument;
            MessageHandler.OnDocumentProgressChange += ChangeProgress;
            CurrentState.CurrentQueueChecker.OnQueueJobChanged += ChangeState;
            Documents = new ObservableCollection<Document>();
            PrintCommand = new DelegateCommand(PrintSelected);
            CancelCommand = new DelegateCommand(CancelSelected);
            //Documents.Add(new Document() { Name = "Doc1", Progress = 0});
            //Documents.Add(new Document() { Name = "Doc2", Progress = 25 });
            //Documents.Add(new Document() { Name = "Doc3", Progress = 50 });
            //Documents.Add(new Document() { Name = "Doc4", Progress = 75 });
            //Documents.Add(new Document() { Name = "Doc5", Progress = 100 });
        }

        public ObservableCollection<Document> Documents { get; set; }
        public DocumentList CurrentControl { get; set; }
        private void AddDocument(Document doc)
        {
            CurrentControl.Dispatcher.BeginInvoke(new Action(() => Documents.Add(doc)));
            //Documents.Add(doc);
        }

        private void ChangeProgress(int id, int progress)
        {
            CurrentControl.Dispatcher.BeginInvoke(new Action(() =>
            {
                Documents.First(x => x.Id == id).Progress = progress;
            }));
        }

        private void ChangeState(string task, DocumentState state,int count, int done)
        {
            CurrentControl.Dispatcher.BeginInvoke(new Action(() =>
            {
                var document = Documents.First(x => x.Name == task);
                //var document = Documents.First();
                if (state == DocumentState.Done)
                {
                    if (document.State != DocumentState.Pending)
                    {
                        CurrentState.CurrentPrintQueue.Remove(task);
                        document.State = state;
                    }
                }
                else
                document.State = state;
                //MessageBox.Show(document.State.ToString());
                try
                {
                    var data = new NameValueCollection();
                    data.Add("status", document.State.ToString().ToLower());
                    NetworkManager.SendPutRequest(data, document.Url.Replace(@"download/", String.Empty));
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }));
        }

        public ICommand PrintCommand { get; set; }
        public ICommand CancelCommand { get; set; }

        private void PrintSelected()
        {
            foreach (var document in Documents)
            {
                if (document.Selected)
                {
                    //CurrentState.CurrentPrintQueue.Add("Print System Document");
                    PrinterManager.PrintDocument(document.Name,Config.Storage.GetDirectoryLocation()+@"\Docs\"+document.Name,CurrentState.DefaultPrinterName);
                    CurrentState.CurrentPrintQueue.Add(document.Name);
                    //MessageBox.Show(document.Url);
                    document.Selected = false;
                }
            }
        }

        private void CancelSelected()
        {
            foreach (var document in Documents)
            {
                if (document.Selected)
                {
                    document.State = DocumentState.Canceled;
                    try
                    {
                        var data = new NameValueCollection();
                        data.Add("status", "canceled");
                        NetworkManager.SendPutRequest(data, document.Url.Replace(@"download/", String.Empty));
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
            }
        }
    }
}
