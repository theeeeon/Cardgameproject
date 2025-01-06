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
        private string DBCONNECTIONSTRING;

        public HttpServer(IPAddress ip, int port, string DBCONNECTIONSTRING)
        {
            httpServer = new TcpListener(ip, port);
            this.DBCONNECTIONSTRING = DBCONNECTIONSTRING;
        }

        public void Handle()
        {
            httpServer.Start();
            while (true)
            {
                var clientSocket = httpServer.AcceptTcpClient();
                /*using var writer = new StreamWriter(clientSocket.GetStream()) { AutoFlush = true };
                using var reader = new StreamReader(clientSocket.GetStream());
                var httpresponse = new HttpResponse(writer);
                var httprequest = new HttpRequest(reader, httpresponse, DBCONNECTIONSTRING);*/
                Thread thread = new Thread(() => Handleclient(clientSocket));
                thread.Start();
                //httprequest.handlerequest();
            }

        }

        public void Handleclient(TcpClient ding)
        {
            using var writer = new StreamWriter(ding.GetStream()) { AutoFlush = true };
            using var reader = new StreamReader(ding.GetStream());
            var httpresponse = new HttpResponse(writer);
            var httprequest = new HttpRequest(reader, httpresponse, DBCONNECTIONSTRING);
            httprequest.handlerequest();
        }



    }
}

//ru und handle im handle codebeispiel und response + request item 

