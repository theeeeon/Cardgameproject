﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using ConsoleApp1.Models;
using System.Text.Json;
using ConsoleApp1.httpserver.Endpoints;

namespace ConsoleApp1.httpserver
{
    class HttpRequest
    {
        private StreamReader reader;
        public string Method;
        public string Path;
        public string Body;
        private HttpResponse httpresponse;
        public string DBCONNECTIONSTRING;


        public HttpRequest(StreamReader reader, HttpResponse httpresponse, string DBCONNECTIONSTRING)
        {
            this.reader = reader;
            this.httpresponse = httpresponse;
            Method = "";
            Path = "";
            Body = "";
            this.DBCONNECTIONSTRING = DBCONNECTIONSTRING;
        }
        public void handlerequest()
        {

            string? line = reader.ReadLine();
            if (line != null)
            {
                Console.WriteLine(line);
                var parts = line.Split(' ');
                if (parts.Length >= 2)
                {
                    Method = parts[0];
                    Path = parts[1];
                }
            }

            int content_length = 0;
            String Authorization = "";
            while ((line = reader.ReadLine()) != null)
            {
                Console.WriteLine(line);
                if (line == "")
                {
                    break;
                }


                var parts_header = line.Split(':');
                if (parts_header.Length == 2 && parts_header[0] == "Content-Length")
                {
                    content_length = int.Parse(parts_header[1].Trim());
                }
                if (parts_header.Length == 2 && parts_header[0] == "Authorization")
                {
                    Authorization = parts_header[1].Trim();
                }
            }

            if (content_length > 0)
            {
                var data = new StringBuilder(200);
                char[] chars = new char[1024];
                int bytesReadTotal = 0;
                while (bytesReadTotal < content_length)
                {
                    var bytesRead = reader.Read(chars, 0, chars.Length);
                    bytesReadTotal += bytesRead;
                    if (bytesRead == 0)
                        break;
                    data.Append(chars, 0, bytesRead);
                }
                Console.WriteLine(data.ToString());
                Body = data.ToString();
            }

            User user = new User("", "");
            if (Path == "/users" || Path == "/sessions")
            {
                user = JsonSerializer.Deserialize<User>(Body);
            }

            
            UserEndpoint userendpoint = new UserEndpoint();
            CardEndpoint cardendpoint = new CardEndpoint();
            Dictionary<string, Action> paths_and_endpoints = new Dictionary<string, Action> {

                {"/users", () => userendpoint.users(httpresponse, user, Method, DBCONNECTIONSTRING)},
                {"/sessions", () => userendpoint.sessions(httpresponse, user, Method, DBCONNECTIONSTRING)},
                {"/packages", () => cardendpoint.packages()  }

            };

            if (paths_and_endpoints.ContainsKey(Path))
            {
                paths_and_endpoints[Path]();
            }
            else
            {
                httpresponse.Code = "404 - Path unknown";
                httpresponse.Body = "";
                httpresponse.handleresponse();
            }

            
            /*if (user == null || user.Username == "" || user.Password == "")
            {
                httpresponse.Code = "400";
                httpresponse.Body = "Username or password missing.";
                httpresponse.handleresponse();
            }
            else if (Method == "POST" && Path == "/users")
            {
                userendpoint.users(httpresponse, user, Method);
            }
            else if (Method == "POST" && Path == "/sessions")
            {
                userendpoint.sessions(httpresponse, user, Method);
            }
            else
            {
                httpresponse.Code = "404";
                httpresponse.Body = "Path/Method unknown.";
                httpresponse.handleresponse();
            }*/

        }

    }
}


