using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Markup;

namespace PrintIt_Desktop_4.Other
{
    public class RichTextBoxHelper : DependencyObject
    {
        private static HashSet<Thread> _recursionProtection = new HashSet<Thread>();

        public static string GetDocumentRtf(DependencyObject obj)
        {
            return (string)obj.GetValue(DocumentRtfProperty);
        }

        public static void SetDocumentRtf(DependencyObject obj, string value)
        {
            _recursionProtection.Add(Thread.CurrentThread);
            obj.SetValue(DocumentRtfProperty, value);
            _recursionProtection.Remove(Thread.CurrentThread);
        }

        public static readonly DependencyProperty DocumentRtfProperty = DependencyProperty.RegisterAttached(
            "DocumentRtf",
            typeof(string),
            typeof(RichTextBoxHelper),
               new FrameworkPropertyMetadata
      {
        BindsTwoWayByDefault = true,
        PropertyChangedCallback = (obj, e) =>
        {
          var richTextBox = (RichTextBox)obj;

          var rtf = GetDocumentRtf(richTextBox);
          var doc = new FlowDocument();
          var range = new TextRange(doc.ContentStart, doc.ContentEnd);

          range.Load(new MemoryStream(Encoding.UTF8.GetBytes(rtf)), 
            DataFormats.Rtf);

          richTextBox.Document = doc;

          range.Changed += (obj2, e2) =>
          {
            if(richTextBox.Document==doc)
            {
              MemoryStream buffer = new MemoryStream();
              range.Save(buffer, DataFormats.Rtf);
              SetDocumentRtf(richTextBox, 
                Encoding.UTF8.GetString(buffer.ToArray()));
            }
          };
       }});

      
    }
}
