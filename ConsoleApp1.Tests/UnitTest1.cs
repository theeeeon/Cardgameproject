using System.ComponentModel.DataAnnotations;
using ConsoleApp1.Repository;
using ConsoleApp1.Models;
using ConsoleApp1.httpserver;
using ConsoleApp1.httpserver.Endpoints;
using Npgsql;
using System.Data;
using ConsoleApp1.Logic;
using System.Net.Sockets;
using System.Net;


namespace ConsoleApp1.Tests
{
    public class Tests
    {
        [OneTimeSetUp]
        public void Setup()
        {
            //Db.Db_init("Host=localhost;Username=theowendel;Password=swen1;Database=projektdb");
        }

        /*[Test]
        public void Adding_User_check_if_exists()
        {
            BusinessLogic repository = new BusinessLogic("Host=localhost;Username=theowendel;Password=swen1;Database=projektdb");

            repository.Adduser("passwordtest", "testuser");

            Assert.That(Equals(repository.Checkusernameexists("testuser"), true));

        }

        [Test]
        public void Check_if_password_correct()
        {
            BusinessLogic repository = new BusinessLogic("Host=localhost;Username=theowendel;Password=swen1;Database=projektdb");
            repository.Adduser("passwordtest", "testuser");

            bool help = repository.Checkpasswordcorrect("passwordtest", "testuser");

            Assert.That(Equals(help, true));
        }*/

        [Test]
        public void Check_adding_5_Cards_to_Packages_Constructor()
        {
            List < Card > cards = new List<Card>();
            cards.Add(new SpellCard("1", "1", 1, "normal"));
            cards.Add(new SpellCard("1", "1", 1, "normal"));
            cards.Add(new SpellCard("1", "1", 1, "normal"));
            cards.Add(new SpellCard("1", "1", 1, "normal"));
            cards.Add(new SpellCard("1", "1", 1, "normal"));

            Packages package = new Packages(cards);
            Assert.That(Equals(package.package.Count, 5));
        }

        [Test]
        public void Check_adding_6_Cards_to_Packages_Constructor()
        {
            List<Card> cards = new List<Card>();
            cards.Add(new SpellCard("1", "1", 1, "normal"));
            cards.Add(new SpellCard("1", "1", 1, "normal"));
            cards.Add(new SpellCard("1", "1", 1, "normal"));
            cards.Add(new SpellCard("1", "1", 1, "normal"));
            cards.Add(new SpellCard("1", "1", 1, "normal"));
            cards.Add(new SpellCard("1", "1", 1, "normal"));

            Packages package = new Packages(cards);
            Assert.That(Equals(package.package.Count, 5));
        }

        [Test]
        public void Check_adding_4_Cards_to_Packages_Constructor()
        {
            List<Card> cards = new List<Card>();
            cards.Add(new SpellCard("1", "1", 1, "normal"));
            cards.Add(new SpellCard("1", "1", 1, "normal"));
            cards.Add(new SpellCard("1", "1", 1, "normal"));
            cards.Add(new SpellCard("1", "1", 1, "normal"));

            Packages package = new Packages(cards);
            Assert.That(Equals(package.package.Count, 0));
        }

        [Test]
        public void Check_if_Card_is_abstract()
        {
            Type type = typeof(Card);

            Assert.That(Equals(false, type.IsAbstract));
        }

        [Test]
        public void Check_if_MonsterCard_is_child()
        {
            Type type = typeof(MonsterCard);
            Type type1 = typeof(Card);

            Assert.That(Equals(true, type.IsSubclassOf(type1)));
        }

        [Test]
        public void Check_if_SpellCard_is_child()
        {
            Type type = typeof(SpellCard);
            Type type1 = typeof(Card);

            Assert.That(Equals(true, type.IsSubclassOf(type1)));
        }

        [Test]
        public void Check_if_packages_admin_only()
        {
            CardEndpoint cardendpoint = new CardEndpoint();
            HttpResponse httpresponse = new HttpResponse(new StreamWriter(new MemoryStream()));
            cardendpoint.packages(httpresponse, "POST", "", "Bearer - randomuser-mtcgToken", "");

            Assert.That(Equals(httpresponse.Code, "401 - admin only"));
        }

        [Test]
        public void Check_if_method_correct_users()
        {
            UserEndpoint userendpoint = new UserEndpoint();
            HttpResponse httpresponse = new HttpResponse(new StreamWriter(new MemoryStream()));
            userendpoint.users(httpresponse, "GET", "", "{\"Username\":\"kienboec\", \"Password\":\"daniel\"}");

            Assert.That(Equals(httpresponse.Code, "400 - Wrong Method"));
        }

        [Test]
        public void Check_if_method_correct_sessions()
        {
            UserEndpoint userendpoint = new UserEndpoint();
            HttpResponse httpresponse = new HttpResponse(new StreamWriter(new MemoryStream()));
            userendpoint.sessions(httpresponse, "GET", "", "{\"Username\":\"kienboec\", \"Password\":\"daniel\"}");

            Assert.That(Equals(httpresponse.Code, "400 - Wrong Method"));
        }

        [Test]
        public void Check_if_User_has_20_Money()
        {
            Models.User user = new Models.User("", "");

            Assert.That(Equals(user.Money, 20));
        }

        [Test]
        public void Check_if_User_has_100_elo()
        {
            Models.User user = new Models.User("", "");

            Assert.That(Equals(user.ELO, 100));
        }

        [Test]
        public void Check_if_cards_Method()
        {
            CardEndpoint cardendpoint = new CardEndpoint();
            HttpResponse httpresponse = new HttpResponse(new StreamWriter(new MemoryStream()));
            cardendpoint.cards(httpresponse, "POST", "", "Bearer - user-mtcgToken");

            Assert.That(Equals(httpresponse.Code, "400 - Wrong Method"));
        }

        [Test]
        public void Check_if_transactions_packages_Method()
        {
            CardEndpoint cardendpoint = new CardEndpoint();
            HttpResponse httpresponse = new HttpResponse(new StreamWriter(new MemoryStream()));
            cardendpoint.transactions_packages(httpresponse, "GET", "", "Bearer - user-mtcgToken");

            Assert.That(Equals(httpresponse.Code, "400 - Wrong Method"));
        }

        [Test]
        public void Check_if_deck_Method()
        {
            CardEndpoint cardendpoint = new CardEndpoint();
            HttpResponse httpresponse = new HttpResponse(new StreamWriter(new MemoryStream()));
            cardendpoint.deck(httpresponse, "POST", "", "Bearer - user-mtcgToken");

            Assert.That(Equals(httpresponse.Code, "400 - Wrong Method"));
        }

        [Test]
        public void Check_if_packages_Method()
        {
            CardEndpoint cardendpoint = new CardEndpoint();
            HttpResponse httpresponse = new HttpResponse(new StreamWriter(new MemoryStream()));
            cardendpoint.packages(httpresponse, "GET", "", "Bearer - user-mtcgToken", "");

            Assert.That(Equals(httpresponse.Code, "400 - Wrong Method"));
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