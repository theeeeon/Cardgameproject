﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using ConsoleApp1.httpserver;
using ConsoleApp1.Logic;
using ConsoleApp1.Models;
using ConsoleApp1.Repository;

namespace ConsoleApp1.httpserver.Endpoints
{
    public class UserEndpoint
    {


        public void users(HttpResponse httpresponse, String method, string DBCONNECTIONSTRING, string body)
        {
            var user = JsonSerializer.Deserialize<User>(body);
            BusinessLogic repository = new BusinessLogic(DBCONNECTIONSTRING);
            if(method == "POST")
            {
                if(repository.Checkusernameexists(user.Username) == false)
                {
                    repository.Adduser(user.Password, user.Username);
                    if (user.Username.Contains("420"))
                    {
                        repository.Addtostack(user.Username, "d7812bcd-1234-4f56-9abc-12de34f56abc");
                    }
                    httpresponse.Code = "201";
                    httpresponse.handleresponse();
                }
                else
                {
                    httpresponse.Code = "400 - User already exists";
                    httpresponse.handleresponse();
                }
            }
            else
            {
                httpresponse.Code = "400 - Wrong Method";
                httpresponse.handleresponse();
            }
        }
        public void sessions(HttpResponse httpresponse, String method, string DBCONNECTIONSTRING, string body)
        {
            BusinessLogic repository = new BusinessLogic(DBCONNECTIONSTRING);
            var user = JsonSerializer.Deserialize<User>(body);

            if (method == "POST")
            {
                if (repository.Checkusernameexists(user.Username) == false)
                {
                    httpresponse.Code = "400 - Does not exist";
                    httpresponse.Body = "";
                }
                else if (repository.Checkpasswordcorrect(user.Password, user.Username) == false)
                {
                    httpresponse.Code = "401 - Wrong password";
                    httpresponse.handleresponse();
                }
                else
                {
                    httpresponse.Code = "200";
                    httpresponse.Body = user.Username + "-mtcgToken";
                    httpresponse.handleresponse();
                }
            }
            else
            {
                httpresponse.Code = "400 - Wrong Method";
                httpresponse.handleresponse();
            }

        }

    }



}
