using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleApp1.Models;
using Npgsql;

namespace ConsoleApp1.Repository
{
    public class UserRepository//nicht getestet
    {
        private readonly string connectionstring;

        public UserRepository(string connectionstring)
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

    }
}
