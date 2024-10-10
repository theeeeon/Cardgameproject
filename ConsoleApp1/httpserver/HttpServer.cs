﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using ConsoleApp1.Classes;
using ConsoleApp1.httpserver.Endpoints;

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
                var httprequest = new HttpRequest(reader);
                var userendpoint = new UserEndpoint();
                httprequest.handlerequest();
                var user = JsonSerializer.Deserialize<User>(httprequest.Body);
                if (user == null || user.Username == "" || user.Password == "")
                {
                    httpresponse.Code = "400";
                    httpresponse.Body = "Username or password missing.";
                }
                else if (httprequest.Method == "POST" && httprequest.Path == "/users")
                {
                    userendpoint.register();
                }
                else if (httprequest.Method == "POST" && httprequest.Path == "/sessions")
                {
                    userendpoint.login();
                }
                else
                {
                    httpresponse.Code = "404";
                    httpresponse.Body = "Path unknown.";
                }
                httpresponse.Code = "200";
                httpresponse.handleresponse();
            }
            
        }



    }
}

//ru und handle im handle codebeispiel und response + request item 

                