using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.httpserver
{
    class HttpResponse
    {
        private StreamWriter writer;
        public string Code;
        public string Body;

        public HttpResponse(StreamWriter writer)
        {
            this.writer = writer;
            Code = "";
            Body = "";
        }
        public void handleresponse()
        {

        }
    }
}

//http und models folder
