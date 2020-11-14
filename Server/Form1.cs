using Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
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
            Log("Получение имя компьютера...");
            string host = Dns.GetHostName();
            Log("Имя компьютера " + host);

            Log("Получение ip-адреса... ");
            IPAddress IP = Dns.GetHostAddresses(host)[1];
            Log("IP адреса: " + IP.ToString());

            Log("Получаем адреса для запуска сокета...");
            IPEndPoint ipPoint = new IPEndPoint(IPAddress.Parse(IP.ToString()), port);
            Log("Адрес: " + ipPoint);
            // создаем сокет
            var listenSocket = new CListeningSocket();
            try
            {
                CPool cPool = new CPool(listenSocket);
                // связываем сокет с локальной точкой, по которой будем принимать данные
                listenSocket.Bind(ipPoint);
                // начинаем прослушивание
                listenSocket.Listen(10);

                label2.Invoke(new Action(() => { label2.Text = "Сервер запущен"; }));
                Log(IP.ToString() + " Сервер запущен. Ожидание подключений...");

                while (true)
                {
                    var res = cPool.ProcessAccept();
                    Log($"Получено сообщение {res.StringResult}");
                    var contentOfRow = listenSocket.GetRowContent(res.StringResult);
                    cPool.Send(contentOfRow, res.handler);
                    // закрываем сокет
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
