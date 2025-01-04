using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleApp1.httpserver;
using ConsoleApp1.Logic;
using ConsoleApp1.Models;
using ConsoleApp1.Repository;

namespace ConsoleApp1.httpserver.Endpoints
{
    internal class UserEndpoint
    {


        public void users(HttpResponse httpresponse, User user, String method, string DBCONNECTIONSTRING)
        {
            BusinessLogic repository = new BusinessLogic(DBCONNECTIONSTRING);
            if(method == "POST")
            {
                if(repository.Checkusernameexists(user.Username) == false)
                {
                    repository.Adduser(user.Password, user.Username);
                    if (user.Username.Contains("420"))
                    {
                        //repository.addtostack(d7812bcd-1234-4f56-9abc-12de34f56abc)
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
            /*
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
            httpresponse.handleresponse();*/
        }
        public void sessions(HttpResponse httpresponse, User user, String method, string DBCONNECTIONSTRING)
        {
            BusinessLogic repository = new BusinessLogic(DBCONNECTIONSTRING);

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

            /*if (!Dictionary.existing_user.ContainsKey(user.Username))
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
               httpresponse.handleresponse();*/
        }

    }



}
