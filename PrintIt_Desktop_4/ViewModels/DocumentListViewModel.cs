using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using PrintIt_Desktop_4.Model.Core;

namespace PrintIt_Desktop_4.ViewModels
{
    public class DocumentListViewModel:ViewModelBase
    {
        public DocumentListViewModel()
        {
            Documents = new ObservableCollection<Document>();
            Documents.Add(new Document() { Name = "Doc1"});
            Documents.Add(new Document() { Name = "Doc2" });
            Documents.Add(new Document() { Name = "Doc3" });
            Documents.Add(new Document() { Name = "Doc4" });
            Documents.Add(new Document() { Name = "Doc5" });
        }

        public ObservableCollection<Document> Documents { get; set; }
    }
}
