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
                        command.CommandText = @"INSERT INTO person (name, password, elo, coins)
                                            VALUES (@name, @password, @elo, @coins)"
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

                        command.ExecuteNonQuery();
                    }
                }

            }

            public void Addcard(string ID, string name, int damage, Spelltype spelltype, Monstertype? monstertype = null)
            {
                List <Card> Card = new List<Card>();
                Monstertype type = Monstertype.Spell;

                if (monstertype.HasValue)
                {
                    Card card = new MonsterCard(ID, name, damage, monstertype.Value, spelltype);
                    Card.Add(card);
                    type = monstertype.Value;
                }
                else { 
                    Card card = new SpellCard(ID, name, damage, spelltype);
                    Card.Add(card);
                }
                
    
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
                        p_ID.Value = Card[0].ID;
                        command.Parameters.Add(p_ID);

                    var p_name = command.CreateParameter();
                        p_name.DbType = DbType.String;
                        p_name.ParameterName = "name";
                        p_name.Value = Card[0].Name;
                        command.Parameters.Add(p_name);

                        var p_damage = command.CreateParameter();
                        p_damage.DbType = DbType.Int32;
                        p_damage.ParameterName = "damage";
                        p_damage.Value = Card[0].Damage;
                        command.Parameters.Add(p_damage);

                        var p_spelltype = command.CreateParameter();
                        p_spelltype.DbType = DbType.String;
                        p_spelltype.ParameterName = "spelltype";
                        p_spelltype.Value = Card[0].type;
                        command.Parameters.Add(p_spelltype);

                        var p_monstertype = command.CreateParameter();
                        p_monstertype.DbType = DbType.String;
                        p_monstertype.ParameterName = "coins";
                        p_monstertype.Value = type;
                        command.Parameters.Add(p_monstertype);

                        command.ExecuteNonQuery();
                    }
                }

            }

            public void Addpackage(List<Card> package)
            {
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
                        Card2.Value = package[2].ID;
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

        }
    }
