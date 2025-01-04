using System.Net;
using System.Text.Json.Nodes;
using ConsoleApp1.Models;
using ConsoleApp1.httpserver;
using ConsoleApp1.Repository;
using Newtonsoft.Json;
using Npgsql;

namespace ConsoleApp1
{
    internal class Program
    {
        private const string DBCONNECTIONSTRING = "Host=localhost;Username=theowendel;Password=swen1;Database=projektdb";

        static void Main(string[] args)
        {
            Db.Db_init(DBCONNECTIONSTRING);
            HttpServer server = new HttpServer(IPAddress.Loopback, 10001, DBCONNECTIONSTRING);
            server.Handle();
        }
    }
}
