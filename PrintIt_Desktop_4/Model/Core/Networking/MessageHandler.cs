using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using MahApps.Metro.Converters;
using Newtonsoft.Json.Linq;
using PrintIt_Desktop_4.Model.Configuration;
using PrintIt_Desktop_4.Model.Enums;
using PrintIt_Desktop_4.Other;
using WebSocketSharp;

namespace PrintIt_Desktop_4.Model.Core.Networking
{
    public static class MessageHandler
    {
        public delegate void DocumentEventHandler(Document document);
        public delegate void DocumentProgressEventHandler(int id,int progress);
        public static event DocumentEventHandler OnDocumentAdd;
        public static event DocumentProgressEventHandler OnDocumentProgressChange;

        public static void HandleWebSocketMessage(object sender, MessageEventArgs e)
        {
            var resJson = JObject.Parse(e.Data);

            if (resJson.HasValues)
            {
                try
                {
                    JToken token;
                    if (resJson.TryGetValue("type", out token))
                    {
                        var type = (String) token;// (string) resJson["type"];
                        if (type == "ping" || type == "welcome") return;
                        if (type == "confirm_subscription")
                        {
                            return;
                        }
                        if (type == "document")
                        {
                            //todo handle it
                            return;
                        }
                    }
                    else
                    {
                        //handle doc
                        var docData = (string)resJson["message"];
                        docData = StringHelper.DeleteSlashes(docData);
                        var docJson = JObject.Parse(docData);
                        if ((string)docJson["price"] == null)
                        {
                            docJson["price"] = 0;
                        }
                        if ((string)docJson["page_count"] == null)
                        {
                            docJson["page_count"] = 0;
                        }
                        Document doc = new Document(){Id = (int)docJson["id"],Name = (string)docJson["name"],Url = (string)docJson["url"],PageCount = (int)docJson["page_count"],
                            Price = (float)docJson["price"],State = HandleStateString((string)docJson["status"]),Selected = false, Progress = 0,OrientedDateMin = (string)docJson["oriented_date_min"],
                        OrientedDateMax = (string)docJson["oriented_date_max"],CreatedAt = (string)docJson["created_at"]};
                        OnDocumentAdd(doc);
                        NetworkManager.DownloadFile(Config.Networking.GetServerAddress()+doc.Url,Config.Storage.GetDirectoryLocation()+@"\Docs\"+ doc.Name, (s,args) =>
                        {
                            OnDocumentProgressChange(doc.Id, args.ProgressPercentage);
                        });
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Json error: "+ex.Message);
                    return;
                }
            }
            MessageBox.Show("Message: " + e.Data);
        }

        private static DocumentState HandleStateString(string data)
        {
            if (data == null) return DocumentState.Pending;
            //todo handle other
            return DocumentState.Pending;
        }

        public static void HandleWebsocketError(object sender, ErrorEventArgs e)
        {
            MessageBox.Show(e.Message);
        }
    }
}
