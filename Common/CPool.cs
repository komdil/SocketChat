using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Common
{
    public class CPool
    {
        public Socket Socket { get; }
        public CPool(Socket socket)
        {
            Socket = socket;
        }

        public void Init(IPEndPoint ipPoint)
        {
            Socket.Connect(ipPoint);
        }

        public string ProcessRead()
        {
            // Receiving request
            var data = new byte[256];
            int bytes = 0;
            do
            {
                bytes = Socket.Receive(data, data.Length, 0);
                string acceptedString = Encoding.Unicode.GetString(data, 0, bytes);
                return acceptedString;
            }
            while (Socket.Available > 0);
        }

        public void Send(string data, Socket socket = null)
        {
            var message = new Message(data);
            if (socket == null)
                socket = Socket;
            socket.Send(message.ToByteArray());
        }

        public (string StringResult, Socket handler) ProcessAccept()
        {
            var handler = Socket.Accept();
            byte[] data = new byte[256]; 
            string response = string.Empty;
            do
            {
                try
                {
                    handler.Receive(data);
                    Message msg = new Message(data);
                    response = msg.Text;
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error: " + ex.Message);
                    Console.WriteLine("StackTrace: " + ex.StackTrace);
                    response = "Communication error!";
                }
                return (response, handler);
            }
            while (handler.Available > 0);
        }

        public void ProcessClose(Socket socket)
        {
            socket.Shutdown(SocketShutdown.Both);
            socket.Close();
        }
    }
}
