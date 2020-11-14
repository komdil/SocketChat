using System.IO;
using System.Linq;
using System.Net.Sockets;

namespace Server0
{
    public class CListeningSocket : Socket
    {
        public CListeningSocket() : base(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp)
        {

        }
    }
}
