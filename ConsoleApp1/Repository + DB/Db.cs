using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
                            coins INT
                        )
                    ";
                    cmd.ExecuteNonQuery();

                    cmd.CommandText = @"
                        CREATE TABLE IF NOT EXISTS Card (
                            C_Id SERIAL PRIMARY KEY, 
                            name VARCHAR(50) NOT NULL,
                            damage INT NOT NULL,
                            spelltype VARCHAR(50) NOT NULL,
                            monstertype VARCHAR(50)
                        )
                    ";
                    cmd.ExecuteNonQuery();

                    cmd.CommandText = @"
                        CREATE TABLE IF NOT EXISTS Deck (
                            P_Id INT NOT NULL, 
                            C_Id INT NOT NULL,
                            FOREIGN KEY (P_Id) REFERENCES Person(P_Id),
                            FOREIGN KEY (C_Id) REFERENCES Card(C_Id),
                            PRIMARY KEY (P_Id, C_Id)
                        )
                    ";
                    cmd.ExecuteNonQuery();

                    cmd.CommandText = @"
                        CREATE TABLE IF NOT EXISTS Stack (
                            P_Id INT NOT NULL, 
                            C_Id INT NOT NULL,
                            FOREIGN KEY (P_Id) REFERENCES Person(P_Id),
                            FOREIGN KEY (C_Id) REFERENCES Card(C_Id),
                            PRIMARY KEY (P_Id, C_Id)
                        )
                    ";
                    cmd.ExecuteNonQuery();
                }
            }

        }
    }
}
