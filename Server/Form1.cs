using Common;
using System;
using System.Net;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Server0
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        void Log(string text)
        {
            richTextBox1.Invoke(new Action(() =>
            {
                richTextBox1.AppendText(text + "\n");
            }));
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Task.Run(new Action(Connect));
        }

        void Connect()
        {
            int port = 8005;
            Log("Getting machine name...");
            string host = Dns.GetHostName();
            Log("Machine name " + host);

            Log("Getting ip-address... ");
            IPAddress IP = Dns.GetHostAddresses(host)[1];
            Log("IP addresses: " + IP.ToString());

            Log("Getting address for starting socket...");
            IPEndPoint ipPoint = new IPEndPoint(IPAddress.Parse(IP.ToString()), port);
            Log("Address: " + ipPoint);

            var listenSocket = new CListeningSocket();
            try
            {
                CPool cPool = new CPool(listenSocket);
                listenSocket.Bind(ipPoint);
                listenSocket.Listen(10);

                label2.Invoke(new Action(() => { label2.Text = "Server has been started"; }));
                Log(IP.ToString() + "Server started. Waiting for client connection...");

                while (true)
                {
                    var res = cPool.ProcessAccept();
                    Log($"Received message: {res.StringResult}");

                    //Sending to client
                    cPool.Send($"We received message {res.StringResult} from you. You are very COOOOl", res.handler);
                    cPool.ProcessClose(res.Item2);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
