using System.ComponentModel.DataAnnotations;
using ConsoleApp1.Repository;
using ConsoleApp1.Models;
using ConsoleApp1.httpserver;
using Npgsql;
using System.Data;


namespace ConsoleApp1.Tests
{
    public class Tests
    {
        [OneTimeSetUp]
        public void Setup()
        {
            Db.Db_init("Host=localhost;Username=theowendel;Password=swen1;Database=projektdb");
        }

        [Test]
        public void Adding_User_check_if_exists()
        {
            UserRepository repository = new UserRepository("Host=localhost;Username=theowendel;Password=swen1;Database=projektdb");

            repository.Adduser("passwordtest", "testuser");

            Assert.That(Equals(repository.Checkusernameexists("testuser"), true));

        }

        [Test]
        public void Check_if_password_correct()
        {
            UserRepository repository = new UserRepository("Host=localhost;Username=theowendel;Password=swen1;Database=projektdb");
            //repository.Adduser("passwordtest", "testuser");

            bool help = repository.Checkpasswordcorrect("passwordtest", "testuser");

            Assert.That(Equals(help, true));
        }
    }
}

//Arrange 

//Objekte machen intizieren 

//Act 

//führen aus 

//Assert

//kommt raus?ASsert.That(Equals(winner, card))

//verschiedene testcases (sehen bei nuwebsite)
//substitute.icard, interface icard, card.damage.returns(), lightsaber mock