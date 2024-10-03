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

var httpServer = new TcpListener(IPAddress.Loopback, 10001);
httpServer.Start();

while (true)
{
    // ----- 0. Accept the TCP-Client and create the reader and writer -----
    var clientSocket = httpServer.AcceptTcpClient(); }

//ru und handle im handle codebeispiel und response + request item 
