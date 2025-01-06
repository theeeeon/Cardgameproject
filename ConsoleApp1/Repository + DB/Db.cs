using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleApp1.Logic;
using Npgsql;
using NpgsqlTypes;

namespace ConsoleApp1.Repository
{
    public static class Db
    {
        public static void Db_init(string connectionstring)
        {
            var builder = new NpgsqlConnectionStringBuilder(connectionstring);

            string dbName = builder.Database;

            builder.Remove("Database");
            string cs = builder.ToString();

            using (IDbConnection connection = new NpgsqlConnection(cs))
            {
                connection.Open();

                using (IDbCommand cmd = connection.CreateCommand())
                {
                    cmd.CommandText = $"DROP DATABASE IF EXISTS {dbName} WITH (force)";
                    cmd.ExecuteNonQuery();

                    cmd.CommandText = $"CREATE DATABASE {dbName}";
                    cmd.ExecuteNonQuery();
                }

                connection.ChangeDatabase(dbName);


                using (IDbCommand cmd = connection.CreateCommand())
                {
                    cmd.CommandText = @"
                        CREATE TABLE IF NOT EXISTS Person (
                            P_Id SERIAL PRIMARY KEY, 
                            name VARCHAR(50) UNIQUE NOT NULL,
                            password VARCHAR(100) NOT NULL,
                            elo INT,
                            coins INT,
                            games_played INT
                        )
                    ";
                    cmd.ExecuteNonQuery();

                    cmd.CommandText = @"
                        CREATE TABLE IF NOT EXISTS Card (
                            C_Id SERIAL PRIMARY KEY,
                            CardID VARCHAR(50) UNIQUE NOT NULL,
                            name VARCHAR(50) NOT NULL,
                            damage INT NOT NULL,
                            spelltype VARCHAR(50) NOT NULL,
                            monstertype VARCHAR(50)
                        )
                    ";
                    cmd.ExecuteNonQuery();

                    cmd.CommandText = @"
                        CREATE TABLE IF NOT EXISTS Deck (
                            Name VARCHAR(50) NOT NULL, 
                            CardID VARCHAR(50) NOT NULL,
                            FOREIGN KEY (Name) REFERENCES Person(Name),
                            FOREIGN KEY (CardID) REFERENCES Card(CardID),
                            PRIMARY KEY (Name, CardID)
                        )
                    ";
                    cmd.ExecuteNonQuery();

                    cmd.CommandText = @"
                        CREATE TABLE IF NOT EXISTS Stack (
                            Name VARCHAR(50) NOT NULL, 
                            CardID VARCHAR(50) NOT NULL,
                            FOREIGN KEY (Name) REFERENCES Person(Name),
                            FOREIGN KEY (CardID) REFERENCES Card(CardID),
                            PRIMARY KEY (Name, CardID)
                        )
                    ";
                    cmd.ExecuteNonQuery();

                    cmd.CommandText = @"
                        CREATE TABLE IF NOT EXISTS Packages (
                            Pa_Id SERIAL PRIMARY KEY,
                            Card1 VARCHAR(50) NOT NULL,
                            Card2 VARCHAR(50) NOT NULL,
                            Card3 VARCHAR(50) NOT NULL,
                            Card4 VARCHAR(50) NOT NULL,
                            Card5 VARCHAR(50) NOT NULL
                        )
                    ";
                    cmd.ExecuteNonQuery();


                }

            }

            BusinessLogic blogic = new BusinessLogic(connectionstring);
            blogic.Addcard("d7812bcd-1234-4f56-9abc-12de34f56abc", "Secret Goblin", 42, "normal", "Goblin");

        }
    }
}
