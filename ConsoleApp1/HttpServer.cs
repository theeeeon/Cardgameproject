using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class HttpServer
    {
        private TcpListener httpServer;

        public HttpServer(IPAddress ip, int port)
        {
            httpServer = new TcpListener(ip, port);
        }



    }
}
