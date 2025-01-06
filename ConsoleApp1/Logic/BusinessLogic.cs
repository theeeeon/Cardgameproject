using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleApp1.Models;
using Npgsql;

namespace ConsoleApp1.Logic
{
        public class BusinessLogic
        {
            private readonly string connectionstring;

            public BusinessLogic(string connectionstring)
            {
                this.connectionstring = connectionstring;
            }
            public bool Checkusernameexists(string username)
            {
                using (IDbConnection connection = new NpgsqlConnection(connectionstring))
                {
                    using (IDbCommand command = connection.CreateCommand())
                    {
                        command.CommandText = @"SELECT *
                                        FROM person
                                        WHERE name = @name";

                        connection.Open();

                        var p_username = command.CreateParameter();
                        p_username.DbType = DbType.String;
                        p_username.ParameterName = "name";
                        p_username.Value = username;
                        command.Parameters.Add(p_username);

                        using (IDataReader reader = command.ExecuteReader())
                        {
                            return reader.Read();
                        }
                    }
                }
            }

            public bool Checkpasswordcorrect(string password, string username)
            {
                using (IDbConnection connection = new NpgsqlConnection(connectionstring))
                {
                    using (IDbCommand command = connection.CreateCommand())
                    {
                        command.CommandText = @"SELECT password
                                        FROM person
                                        WHERE name = @name";

                        connection.Open();

                        var p_username = command.CreateParameter();
                        p_username.DbType = DbType.String;
                        p_username.ParameterName = "name";
                        p_username.Value = username;
                        command.Parameters.Add(p_username);

                        using (IDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                if (reader.GetString(0) == password) //hash außerhalb funktion gemacht
                                    return true;
                                else
                                    return false;
                            }
                            else
                                return false;
                        }
                    }
                }
            }

            public void Adduser(string password, string username)//durch zB trycatch prüfen
            {
                User user = new User(username, password);//hash außerhalb funktiom/schon mitgegeben

                using (IDbConnection connection = new NpgsqlConnection(connectionstring))
                {
                    using (IDbCommand command = connection.CreateCommand())
                    {
                        connection.Open();
                        command.CommandText = @"INSERT INTO person (name, password, elo, coins, games_played)
                                            VALUES (@name, @password, @elo, @coins, @games_played)"
                        ;

                        var p_username = command.CreateParameter();
                        p_username.DbType = DbType.String;
                        p_username.ParameterName = "name";
                        p_username.Value = user.Username;
                        command.Parameters.Add(p_username);

                        var p_password = command.CreateParameter();
                        p_password.DbType = DbType.String;
                        p_password.ParameterName = "password";
                        p_password.Value = user.Password;
                        command.Parameters.Add(p_password);

                        var p_elo = command.CreateParameter();
                        p_elo.DbType = DbType.Int32;
                        p_elo.ParameterName = "elo";
                        p_elo.Value = user.ELO;
                        command.Parameters.Add(p_elo);

                        var p_coins = command.CreateParameter();
                        p_coins.DbType = DbType.Int32;
                        p_coins.ParameterName = "coins";
                        p_coins.Value = user.Money;
                        command.Parameters.Add(p_coins);

                        var games_played = command.CreateParameter();
                        games_played.DbType = DbType.Int32;
                        games_played.ParameterName = "games_played";
                        games_played.Value = 0;
                        command.Parameters.Add(games_played);

                    command.ExecuteNonQuery();
                    }
                }

            }

            public void Addcard(string ID, string name, double damage, string spelltype, string monstertype)
            {
                

                Card card = new MonsterCard(ID, name, damage, monstertype, spelltype);
                
    
                using (IDbConnection connection = new NpgsqlConnection(connectionstring))
                {
                    using (IDbCommand command = connection.CreateCommand())
                    {
                        connection.Open();
                        command.CommandText = @"INSERT INTO Card (CardID, name, damage, spelltype, monstertype)
                                            VALUES (@CardID, @name, @damage, @spelltype, @monstertype)"
                        ;

                        var p_ID = command.CreateParameter();
                        p_ID.DbType = DbType.String;
                        p_ID.ParameterName = "CardID";
                        p_ID.Value = card.ID;
                        command.Parameters.Add(p_ID);

                    var p_name = command.CreateParameter();
                        p_name.DbType = DbType.String;
                        p_name.ParameterName = "name";
                        p_name.Value = card.Name;
                        command.Parameters.Add(p_name);

                        var p_damage = command.CreateParameter();
                        p_damage.DbType = DbType.Int32;
                        p_damage.ParameterName = "damage";
                        p_damage.Value = card.Damage;
                        command.Parameters.Add(p_damage);

                        var p_spelltype = command.CreateParameter();
                        p_spelltype.DbType = DbType.String;
                        p_spelltype.ParameterName = "spelltype";
                        p_spelltype.Value = card.Spelltype;
                        command.Parameters.Add(p_spelltype);

                        var p_monstertype = command.CreateParameter();
                        p_monstertype.DbType = DbType.String;
                        p_monstertype.ParameterName = "monstertype";
                        p_monstertype.Value = monstertype;
                        command.Parameters.Add(p_monstertype);

                        command.ExecuteNonQuery();
                    }
                }

            }

            public void Addpackage(List<Card> package)
            {
                foreach (Card card in package) {
                    if (card.Spelltype == null)
                    {
                        card.Spelltype = "";
                    }
                Addcard(card.ID, card.Name, card.Damage, card.Spelltype, "");
                }

                using (IDbConnection connection = new NpgsqlConnection(connectionstring))
                {
                    using (IDbCommand command = connection.CreateCommand())
                    {
                        connection.Open();
                        command.CommandText = @"INSERT INTO Packages (Card1, Card2, Card3, Card4, Card5)
                                            VALUES (@Card1, @Card2, @Card3, @Card4, @Card5)"
                        ;

                        var Card1 = command.CreateParameter();
                        Card1.DbType = DbType.String;
                        Card1.ParameterName = "Card1";
                        Card1.Value = package[0].ID;
                        command.Parameters.Add(Card1);

                        var Card2 = command.CreateParameter();
                        Card2.DbType = DbType.String;
                        Card2.ParameterName = "Card2";
                        Card2.Value = package[1].ID;
                        command.Parameters.Add(Card2);

                        var Card3 = command.CreateParameter();
                        Card3.DbType = DbType.String;
                        Card3.ParameterName = "Card3";
                        Card3.Value = package[2].ID;
                        command.Parameters.Add(Card3);

                        var Card4 = command.CreateParameter();
                        Card4.DbType = DbType.String;
                        Card4.ParameterName = "Card4";
                        Card4.Value = package[3].ID;
                        command.Parameters.Add(Card4);
                        
                        var Card5 = command.CreateParameter();
                        Card5.DbType = DbType.String;
                        Card5.ParameterName = "Card5";
                        Card5.Value = package[4].ID;
                        command.Parameters.Add(Card5);

                        command.ExecuteNonQuery();
                    }
                }
            }

            

            public List<string> Getanddeletepackage()
            {
            int Id = 0;
            List<string> package = new List<string>();
            using (IDbConnection connection = new NpgsqlConnection(connectionstring))
            {
                using (IDbCommand command = connection.CreateCommand())
                {
                    command.CommandText = @"SELECT Pa_Id, Card1, Card2, Card3, Card4, Card5
                                        FROM Packages
                                        ";

                    connection.Open();

                    using (IDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            Id = (int)reader["Pa_Id"];
                            package.Add(reader["Card1"].ToString());
                            package.Add(reader["Card2"].ToString());
                            package.Add(reader["Card3"].ToString());
                            package.Add(reader["Card4"].ToString());
                            package.Add(reader["Card5"].ToString());
                            Deletepackage(Id);
                        }
                        
                    }
                }
            }

            return package;
            }

            public void Addtostack(string username, string CardID)
            {
            using (IDbConnection connection = new NpgsqlConnection(connectionstring))
            {
                using (IDbCommand command = connection.CreateCommand())
                {
                    connection.Open();
                    command.CommandText = @"INSERT INTO Stack (Name, CardID)
                                            VALUES (@Name, @CardID)"
                    ;

                    var name = command.CreateParameter();
                    name.DbType = DbType.String;
                    name.ParameterName = "Name";
                    name.Value = username;
                    command.Parameters.Add(name);

                    var ID = command.CreateParameter();
                    ID.DbType = DbType.String;
                    ID.ParameterName = "CardID";
                    ID.Value = CardID;
                    command.Parameters.Add(ID);

                    command.ExecuteNonQuery();
                }
            }
        }
            

            public void Buypackage(string username)
            {

            if (Checkmoney(username) == false)
            {
                throw new Exception("Money");
            }
            else
            {
                List<string> package = Getanddeletepackage();

                if(package.Count == 0)
                {
                    throw new Exception("No Package");
                }

                foreach (string name in package)
                {
                    Addtostack(username, name);
                }
                Paymoney(username);
            }
            }

            public bool Checkmoney(string username)
            {
            using (IDbConnection connection = new NpgsqlConnection(connectionstring))
            {
                using (IDbCommand command = connection.CreateCommand())
                {
                    connection.Open();
                    command.CommandText = @"SELECT coins
                                FROM Person
                                WHERE name = @name"
                    ;

                    var name = command.CreateParameter();
                    name.DbType = DbType.String;
                    name.ParameterName = "name";
                    name.Value = username;
                    command.Parameters.Add(name);

                    command.ExecuteNonQuery();

                    using (IDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {

                            /*ConsoleColor originalColor = Console.ForegroundColor;
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.WriteLine((int)reader["coins"]);
                            Console.ForegroundColor = originalColor;*/

                            if ((int)reader["coins"] > 4)
                            {
                                return true;
                            }
                            else
                            {
                                return false;
                            }
                        }
                        else
                        {
                            return false;
                        }

                    }
                }
            }

        }

            public void Paymoney(string username)
            {
            using (IDbConnection connection = new NpgsqlConnection(connectionstring))
            {
                using (IDbCommand command = connection.CreateCommand())
                {
                    connection.Open();
                    command.CommandText = @"UPDATE Person
                                SET coins = coins - 5
                                WHERE name = @name"
                    ;

                    var name = command.CreateParameter();
                    name.DbType = DbType.String;
                    name.ParameterName = "name";
                    name.Value = username;
                    command.Parameters.Add(name);

                    command.ExecuteNonQuery();
                }
            }

            }

            public void Deletepackage(int ID)
            {
            using (IDbConnection connection = new NpgsqlConnection(connectionstring))
            {
                using (IDbCommand command = connection.CreateCommand())
                {
                    connection.Open();
                    command.CommandText = @"DELETE FROM Packages
                                            WHERE Pa_Id = @Pa_Id"
                    ;

                    var id = command.CreateParameter();
                    id.DbType = DbType.Int32;
                    id.ParameterName = "Pa_Id";
                    id.Value = ID;
                    command.Parameters.Add(id);

                    command.ExecuteNonQuery();
                }
            }
        }

        public List<Card> Getcards(string username)
        {
            List<string> ids = new List<string>();
            List<Card> cards = new List<Card>();

            using (IDbConnection connection = new NpgsqlConnection(connectionstring))
            {
                using (IDbCommand command = connection.CreateCommand())
                {
                    connection.Open();
                    command.CommandText = @"SELECT CardID FROM Stack
                                            WHERE Name = @Name"
                    ;

                    var Name = command.CreateParameter();
                    Name.DbType = DbType.String;
                    Name.ParameterName = "Name";
                    Name.Value = username;
                    command.Parameters.Add(Name);

                    using (IDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            ids.Add(reader["CardID"].ToString());
                        }

                    }
                }
            }

            foreach(string id in ids)
            {
                try
                {
                    cards.Add(Getcard(id));
                }
                catch(Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }

            return cards;
        }

        public Card Getcard(string id)
        {

            using (IDbConnection connection = new NpgsqlConnection(connectionstring))
            {
                using (IDbCommand command = connection.CreateCommand())
                {
                    connection.Open();
                    command.CommandText = @"SELECT * FROM Card
                                            WHERE CardID = @CardID"
                    ;

                    var Name = command.CreateParameter();
                    Name.DbType = DbType.String;
                    Name.ParameterName = "CardID";
                    Name.Value = id;
                    command.Parameters.Add(Name);

                    using (IDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            Card card = new Card(id, reader["name"].ToString(), (int)reader["damage"], reader["spelltype"].ToString());
                            return card;
                        }
                        else
                        {
                            throw new Exception("Card not in card Table");
                        }

                    }
                }
            }
        }

        public List<Card> Getdeckcards(string username)
        {
            List<string> ids = new List<string>();
            List<Card> cards = new List<Card>();

            using (IDbConnection connection = new NpgsqlConnection(connectionstring))
            {
                using (IDbCommand command = connection.CreateCommand())
                {
                    connection.Open();
                    command.CommandText = @"SELECT CardID FROM Deck
                                            WHERE Name = @Name"
                    ;

                    var Name = command.CreateParameter();
                    Name.DbType = DbType.String;
                    Name.ParameterName = "Name";
                    Name.Value = username;
                    command.Parameters.Add(Name);

                    using (IDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            ids.Add(reader["CardID"].ToString());
                        }

                    }
                }
            }

            foreach (string id in ids)
            {
                try
                {
                    cards.Add(Getcard(id));
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }

            return cards;
        }

        public bool CardinStackCheck(string id, string username)
        {
            using (IDbConnection connection = new NpgsqlConnection(connectionstring))
            {
                using (IDbCommand command = connection.CreateCommand())
                {
                    command.CommandText = @"SELECT Name, CardID
                                        FROM Stack
                                        WHERE Name = @name AND CardID = @cardid";

                    connection.Open();

                    var p_username = command.CreateParameter();
                    p_username.DbType = DbType.String;
                    p_username.ParameterName = "name";
                    p_username.Value = username;
                    command.Parameters.Add(p_username);

                    var p_id = command.CreateParameter();
                    p_id.DbType = DbType.String;
                    p_id.ParameterName = "cardid";
                    p_id.Value = id;
                    command.Parameters.Add(p_id);

                    using (IDataReader reader = command.ExecuteReader())
                    {
                        return reader.Read();
                    }
                }
            }
        }

        public void CreateDeck(List<string> cards, string username)
        {
            if(cards.Count != 4)
            {
                throw new Exception("Not 4 Cards!");
            }
            foreach(string card in cards)
            {
                if(CardinStackCheck(card, username) == false)
                {
                    throw new Exception("Not own Card!");
                }
            }

            if(DeckexistsalreadyCheck(username) == false)
            {
                foreach(string card in cards)
                {
                    Addcardtodeck(card, username);
                }
            }
            else
            {
                Deletedeck(username);
                foreach (string card in cards)
                {
                    Addcardtodeck(card, username);
                }
            }


        }

        public void Deletedeck(string username)
        {
            using (IDbConnection connection = new NpgsqlConnection(connectionstring))
            {
                using (IDbCommand command = connection.CreateCommand())
                {
                    connection.Open();
                    command.CommandText = @"DELETE FROM Deck
                                WHERE Name = @name"
                    ;

                    var name = command.CreateParameter();
                    name.DbType = DbType.String;
                    name.ParameterName = "name";
                    name.Value = username;
                    command.Parameters.Add(name);

                    command.ExecuteNonQuery();
                }
            }
        }

        public void Addcardtodeck(string id, string username)
        {
            using (IDbConnection connection = new NpgsqlConnection(connectionstring))
            {
                using (IDbCommand command = connection.CreateCommand())
                {
                    connection.Open();
                    command.CommandText = @"INSERT INTO Deck (Name, CardID)
                                            VALUES (@Name, @CardID)"
                    ;

                    var name = command.CreateParameter();
                    name.DbType = DbType.String;
                    name.ParameterName = "Name";
                    name.Value = username;
                    command.Parameters.Add(name);

                    var ID = command.CreateParameter();
                    ID.DbType = DbType.String;
                    ID.ParameterName = "CardID";
                    ID.Value = id;
                    command.Parameters.Add(ID);

                    command.ExecuteNonQuery();
                }
            }
        }

        public bool DeckexistsalreadyCheck(string username)
        {
            using (IDbConnection connection = new NpgsqlConnection(connectionstring))
            {
                using (IDbCommand command = connection.CreateCommand())
                {
                    command.CommandText = @"SELECT *
                                        FROM Deck
                                        WHERE Name = @name";

                    connection.Open();

                    var p_username = command.CreateParameter();
                    p_username.DbType = DbType.String;
                    p_username.ParameterName = "name";
                    p_username.Value = username;
                    command.Parameters.Add(p_username);

                    using (IDataReader reader = command.ExecuteReader())
                    {
                        return reader.Read();
                    }
                }
            }
        }

        public Dictionary<string, int> GetELO(string username)
        {

            Dictionary<string, int> ELO = new Dictionary<string, int> {

                {"ELO", 100},
                {"Games Played", 0}

            };

            using (IDbConnection connection = new NpgsqlConnection(connectionstring))
            {
                using (IDbCommand command = connection.CreateCommand())
                {
                    command.CommandText = @"SELECT ELO, games_played
                                        FROM Person
                                        WHERE Name = @name";

                    connection.Open();

                    var p_username = command.CreateParameter();
                    p_username.DbType = DbType.String;
                    p_username.ParameterName = "name";
                    p_username.Value = username;
                    command.Parameters.Add(p_username);

                    using (IDataReader reader = command.ExecuteReader())
                    {
                        if(reader.Read())
                        {
                            ELO["ELO"] = (int)reader["ELO"];
                            ELO["Games Played"] = (int)reader["games_played"];
                        }
                    }
                }
            }

            return ELO;
        }

        public List<Tuple<string, int>> GetAllELO()
        {
            List<Tuple<string, int>> list = new List<Tuple<string, int>>();

            using (IDbConnection connection = new NpgsqlConnection(connectionstring))
            {
                using (IDbCommand command = connection.CreateCommand())
                {
                    command.CommandText = @"SELECT name, elo
                                        FROM Person";

                    connection.Open();

                    using (IDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            list.Add(Tuple.Create(reader["name"].ToString(), (int)reader["elo"]));
                        }
                    }
                }
            }


            list.Sort((x, y) => y.Item2.CompareTo(x.Item2));

            return list;
        }

    }
}
