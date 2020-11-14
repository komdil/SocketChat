using Common;
using System;
using System.Net;
using System.Windows.Forms;


namespace Client
{
    public partial class ClientForm : Form
    {
        static int port = 8005;
        public ClientForm()
        {
            InitializeComponent();
        }

        private void Connect()
        {
            try
            {
                IPEndPoint ipPoint = null;
                try
                {
                    ipPoint = new IPEndPoint(IPAddress.Parse(textBox1.Text), port);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Connection error. Wrong IP address\n" + ex.Message);
                }
                if (ipPoint != null)
                {
                    CClientSocket socket = new CClientSocket();
                    CPool cPool = new CPool(socket);
                    cPool.Init(ipPoint);
                    cPool.Send(textBoxString.Text);
                    richTextBox1.AppendText(cPool.ProcessRead() + "\n");
                }
            }
            catch (Exception ex)
            {
                richTextBox1.AppendText("Communication error" + ex.Message + "\n");
            }
        }

        private void buttonSend_Click(object sender, EventArgs e)
        {
            Connect();
        }
    }
}
