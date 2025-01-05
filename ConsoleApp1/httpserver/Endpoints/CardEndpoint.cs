﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using ConsoleApp1.Logic;
using ConsoleApp1.Models;
using Newtonsoft.Json;

namespace ConsoleApp1.httpserver.Endpoints
{
    public class CardEndpoint
    {
        public void packages(HttpResponse httpresponse, String Method, string DBCONNECTIONSTRING, string Authorization, string body)
        {
            if(Authorization == "Bearer admin-mtcgToken")
            {
                List<Card> package = JsonConvert.DeserializeObject<List<Card>>(body);
                BusinessLogic repository = new BusinessLogic(DBCONNECTIONSTRING);

                repository.Addpackage(package);

                httpresponse.Code = "201";
                httpresponse.handleresponse();
            }
            else
            {
                httpresponse.Code = "401 - admin only";
                httpresponse.handleresponse();
            }
        }

        public void transactions_packages(HttpResponse httpresponse, String Method, string DBCONNECTIONSTRING, string Authorization)
        {
            Authorization = Authorization.Replace("Bearer ", "").Replace("-mtcgToken", "");
            BusinessLogic repository = new BusinessLogic(DBCONNECTIONSTRING);


            if (repository.Checkusernameexists(Authorization))
            {
                try
                {
                    repository.Buypackage(Authorization);
                    httpresponse.Code = "201";
                    httpresponse.handleresponse();
                }
                catch (Exception ex) 
                {
                    if(ex.Message == "Money")
                    {
                        httpresponse.Code = "400 - Not enough Money";
                        httpresponse.handleresponse();
                    }
                    else
                    {
                        httpresponse.Code = "400 - No Packages";
                        httpresponse.handleresponse();
                    }
                        
                }
                
            }
            else
            {
                httpresponse.Code = "401 - Wrong Authorization";
                httpresponse.handleresponse();
            }
        }
    }
}
