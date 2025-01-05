using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.httpserver
{
    public class HttpResponse
    {
        private StreamWriter writer;
        public string Code;
        public string Body;
        public string Version;
        public string Headername;
        public string Headervalue;


        public HttpResponse(StreamWriter writer)
        {
            this.writer = writer;
            Code = "";
            Body = "";
            Version = "HTTP/1.1";
            Headername = "Content-type";
            Headervalue = "JSON";
        }
        public void handleresponse()
        {
            string headerline = $"{Headername}: {Headervalue}";
            Console.WriteLine($"{Version} {Code}");
            writer.WriteLine($"{Version} {Code}");
            Console.WriteLine(headerline);
            writer.WriteLine(headerline);

            Console.WriteLine("");
            writer.WriteLine("");

            Console.WriteLine($"{Body}");
            writer.WriteLine($"{Body}");
        }
    }
}

//http und models folder
