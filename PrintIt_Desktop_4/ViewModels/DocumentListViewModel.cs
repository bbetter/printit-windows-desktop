using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using PrintIt_Desktop_4.Model.Core;
using PrintIt_Desktop_4.Model.Core.Networking;
using PrintIt_Desktop_4.UserControls;

namespace PrintIt_Desktop_4.ViewModels
{
    public class DocumentListViewModel:ViewModelBase
    {
        public DocumentListViewModel()
        {
            MessageHandler.OnDocumentAdd += AddDocument;
            MessageHandler.OnDocumentProgressChange += ChangeProgress;
            Documents = new ObservableCollection<Document>();
            Documents.Add(new Document() { Name = "Doc1", Progress = 0});
            Documents.Add(new Document() { Name = "Doc2", Progress = 25 });
            Documents.Add(new Document() { Name = "Doc3", Progress = 50 });
            Documents.Add(new Document() { Name = "Doc4", Progress = 75 });
            Documents.Add(new Document() { Name = "Doc5", Progress = 100 });
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
    }
}
