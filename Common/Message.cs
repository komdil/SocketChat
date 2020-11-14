using System;
using System.Collections.Generic;
using System.Text;

namespace Common
{
    public class Message
    {
        public string Text { get; }

        public Message(string text)
        {
            Text = text;
        }

        public Message(byte[] byteArr)
        {
            Encoding encoding = Encoding.Unicode;
            Text = encoding.GetString(byteArr);
        }

        public byte[] ToByteArray()
        {
            Encoding encoding = Encoding.Unicode;
            return encoding.GetBytes(Text);
        }
    }
}
