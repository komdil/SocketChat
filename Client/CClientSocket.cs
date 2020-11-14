using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Client
{
    public class CClientSocket : Socket
    {
        public CClientSocket() : base(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp)
        {

        }
    }
}
