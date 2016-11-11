using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Windows;
using PrintIt_Desktop_4.Model.Configuration;
using WebSocketSharp;
using WebSocketSharp.Net;

namespace PrintIt_Desktop_4.Model.Core.Networking
{
    public class WebSocketWrapper
    {
        private EventHandler<ErrorEventArgs> _handler;
        private EventHandler<MessageEventArgs> _mHandler;
        private Thread _thread;
        private Object _threadArgs;
        private Queue<String> _messages = new Queue<string>(); 
        public WebSocketWrapper(String connectAddress, Cookie cookie)
        {
            var ts = new ParameterizedThreadStart(ThreadBody);
            _thread = new Thread(ts);
            _threadArgs = new object[] {connectAddress, cookie};
        }

        public void Start()
        {
            _thread.Start(_threadArgs);
        }

        public void SetErrorHandler(EventHandler<ErrorEventArgs> handler)
        {
            _handler = handler;
        }

        public void SetMessageHandler(EventHandler<MessageEventArgs> handler)
        {
            _mHandler = handler;
        }

        public void SendMessage(String message)
        {
            _messages.Enqueue(message);
        }

        private void ThreadBody(object param)
        {
            try
            {
                var input = (object[]) param;
                var address = (string) input[0];
                var cookie = (Cookie) input[1];
                using (var ws = new WebSocket(address))
                {
                    ws.SetCookie(cookie);
                    ws.OnError += _handler;
                    ws.OnMessage += _mHandler;
                    ws.Connect();
                    while (true)
                    {
                        if (_messages.Count > 0)
                        {
                            var cmd = _messages.Dequeue();
                            if (cmd.Equals(Config.Wrappers.GetStopMessage())) break;
                            ws.Send(cmd);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
