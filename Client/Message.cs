using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client
{
    public class Message
    {
        public string Text { get; }

        public Message(string text)
        {
            Text = text;
        }

        public byte[] ToByteArray()
        {
            Encoding encoding = Encoding.UTF8;
            return encoding.GetBytes(Text);
        }
    }
}
