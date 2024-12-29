using System;
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
        private UserEndpoint userendpoint;


        public HttpRequest(StreamReader reader, HttpResponse httpresponse, UserEndpoint userendpoint)
        {
            this.reader = reader;
            this.httpresponse = httpresponse;
            this.userendpoint = userendpoint;
            Method = "";
            Path = "";
            Body = "";
        }
        public void handlerequest()
        {

            var user = JsonSerializer.Deserialize<User>(Body);
            if (user == null || user.Username == "" || user.Password == "")
            {
                httpresponse.Code = "400";
                httpresponse.Body = "Username or password missing.";
                httpresponse.handleresponse();
            }
            else if (Method == "POST" && Path == "/users")
            {
                userendpoint.register(httpresponse, user);
            }
            else if (Method == "POST" && Path == "/sessions")
            {
                userendpoint.login(httpresponse, user);
            }
            else
            {
                httpresponse.Code = "404";
                httpresponse.Body = "Path/Method unknown.";
                httpresponse.handleresponse();
            }

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


        }

    }
}


