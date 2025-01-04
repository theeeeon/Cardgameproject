using System.ComponentModel.DataAnnotations;
using ConsoleApp1.Repository;
using ConsoleApp1.Models;
using ConsoleApp1.httpserver;
using Npgsql;
using System.Data;
using ConsoleApp1.Logic;


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
            cards.Add(new SpellCard("1", "1", 1, Spelltype.normal));
            cards.Add(new SpellCard("1", "1", 1, Spelltype.normal));
            cards.Add(new SpellCard("1", "1", 1, Spelltype.normal));
            cards.Add(new SpellCard("1", "1", 1, Spelltype.normal));
            cards.Add(new SpellCard("1", "1", 1, Spelltype.normal));

            Packages package = new Packages(cards);
            Assert.That(Equals(package.package.Count, 5));
        }

        [Test]
        public void Check_adding_6_Cards_to_Packages_Constructor()
        {
            List<Card> cards = new List<Card>();
            cards.Add(new SpellCard("1", "1", 1, Spelltype.normal));
            cards.Add(new SpellCard("1", "1", 1, Spelltype.normal));
            cards.Add(new SpellCard("1", "1", 1, Spelltype.normal));
            cards.Add(new SpellCard("1", "1", 1, Spelltype.normal));
            cards.Add(new SpellCard("1", "1", 1, Spelltype.normal));
            cards.Add(new SpellCard("1", "1", 1, Spelltype.normal));

            Packages package = new Packages(cards);
            Assert.That(Equals(package.package.Count, 5));
        }

        [Test]
        public void Check_adding_4_Cards_to_Packages_Constructor()
        {
            List<Card> cards = new List<Card>();
            cards.Add(new SpellCard("1", "1", 1, Spelltype.normal));
            cards.Add(new SpellCard("1", "1", 1, Spelltype.normal));
            cards.Add(new SpellCard("1", "1", 1, Spelltype.normal));
            cards.Add(new SpellCard("1", "1", 1, Spelltype.normal));

            Packages package = new Packages(cards);
            Assert.That(Equals(package.package.Count, 0));
        }

        [Test]
        public void Check_if_Card_is_abstract()
        {
            Type type = typeof(Card);

            Assert.That(Equals(true, type.IsAbstract));
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