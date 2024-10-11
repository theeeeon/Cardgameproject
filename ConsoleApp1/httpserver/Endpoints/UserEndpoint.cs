using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleApp1.httpserver;
using ConsoleApp1.Models;

namespace ConsoleApp1.httpserver.Endpoints
{
    internal class UserEndpoint
    {


        public void register(HttpResponse httpresponse, User user)
        {
            if (Dictionary.existing_user.ContainsKey(user.Username))
            {
                httpresponse.Code = "400";
                httpresponse.Body = "HTTP 400 - User already exists";
            }
            else
            {
                Dictionary.existing_user.Add(user.Username, user.Password);
                httpresponse.Code = "201";
                httpresponse.Body = "HTTP 201";
            }
            httpresponse.handleresponse();
        }
        public void login(HttpResponse httpresponse, User user)
        {
            if (!Dictionary.existing_user.ContainsKey(user.Username))
            {
                httpresponse.Code = "400";
                httpresponse.Body = "Does not exist";
            }
            else if (Dictionary.existing_user[user.Username] != user.Password)
            {
                httpresponse.Code = "400";
                httpresponse.Body = "HTTP 400 Wrong password";
            }
            else
            {
                httpresponse.Code = "200";
                httpresponse.Body = "HTTP 200 " + user.Username + "-mctgToken";
            }
            httpresponse.handleresponse();
        }

    }



}
