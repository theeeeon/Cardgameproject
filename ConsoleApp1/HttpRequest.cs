using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class HttpRequest
    {
        private StreamReader reader;

        public HttpRequest(StreamReader reader)
        {
            this.reader = reader;
        }
    }
}
