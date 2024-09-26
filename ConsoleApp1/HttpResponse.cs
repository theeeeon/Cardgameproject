using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class HttpResponse
    {
        private StreamWriter writer;

        public HttpResponse(StreamWriter writer)
        {
            this.writer = writer;
        }
    }
}
