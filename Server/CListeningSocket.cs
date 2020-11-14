using System.IO;
using System.Linq;
using System.Net.Sockets;

namespace Server0
{
    public class CListeningSocket : Socket
    {
        const string fileName = "MyFile.txt";

        public CListeningSocket() : base(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp)
        {

        }

        public string GetRowContent(string rowNumber)
        {
            if (int.TryParse(rowNumber, out int number))
            {
                var files = File.ReadAllLines(fileName).ToList();
                if (files.Count <= number || number < 0)
                {
                    return "Номер строки введен не правильно!";
                }
                else
                {
                    return files[number - 1];
                }
            }
            else
            {
                return "Номер строки введен не правильно! Он должен содержать только цифру";
            }
        }
    }
}
