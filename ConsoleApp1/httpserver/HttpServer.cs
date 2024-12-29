using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using ConsoleApp1.httpserver.Endpoints;
using ConsoleApp1.Models;

namespace ConsoleApp1.httpserver
{
    class HttpServer
    {
        private TcpListener httpServer;

        public HttpServer(IPAddress ip, int port)
        {
            httpServer = new TcpListener(ip, port);
        }

        public void Handle()
        {
            httpServer.Start();
            while (true)
            {
                var clientSocket = httpServer.AcceptTcpClient();
                using var writer = new StreamWriter(clientSocket.GetStream()) { AutoFlush = true };
                using var reader = new StreamReader(clientSocket.GetStream());
                var httpresponse = new HttpResponse(writer);
                var userendpoint = new UserEndpoint();
                var httprequest = new HttpRequest(reader, httpresponse, userendpoint);
                Thread thread = new Thread(httprequest.handlerequest);
                thread.Start();
            }

        }



    }
}

//ru und handle im handle codebeispiel und response + request item 

