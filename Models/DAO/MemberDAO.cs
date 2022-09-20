﻿using MySql.Data.MySqlClient;
using Readit.Models.Entities;
using System;

namespace Readit.Models.DAO
{
    public class MemberDAO
    {
        private readonly IConfiguration configuration;
        public DbContext dbContext { get; set; }

        public MemberDAO(IConfiguration _configuration) 
        {
            this.configuration = _configuration;
            this.dbContext = new DbContext(configuration);
        }

        public IList<Member> GetMembers()
        {
            IList<Member > members = new List<Member>();
            MySqlConnection connection = new MySqlConnection(dbContext.connectionString);
            try
            {
                connection.Open();
                MySqlCommand command = new MySqlCommand("SELECT * FROM Member", connection);
                MySqlDataReader dataReader = command.ExecuteReader();
                while (dataReader.Read())
                {
                    members.Add(new Member()
                    {
                        ID = dataReader.GetInt32("ID"),
                        Username = dataReader.GetString("Username"),
                        Email = dataReader.GetString("Email"),
                        Password = dataReader.GetString("Password")
                    }
                    );
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                connection.Close();
            }
            return members;
        }

        public Member GetMemberByID(int id)
        {
            Member found;
            MySqlConnection connection = new MySqlConnection(dbContext.connectionString);
            try
            {
                connection.Open();
                MySqlCommand command = new MySqlCommand("SELECT * FROM Member WHERE ID=@id", connection);
                command.Parameters.Add(new MySqlParameter("@ID", id));
                MySqlDataReader dataReader = command.ExecuteReader();
                found = new Member()
                {
                    ID = dataReader.GetInt32("ID"),
                    Username = dataReader.GetString("Username"),
                    Email = dataReader.GetString("Email"),
                    Password = dataReader.GetString("Password")
                };
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                connection.Close();
            }
            return found;
        }

        public Member GetMemberByEmail(string email)
        {
            Member found;
            MySqlConnection connection = new MySqlConnection(dbContext.connectionString);
            try
            {
                connection.Open();
                MySqlCommand command = new MySqlCommand("SELECT * FROM Member WHERE Email=@email", connection);
                command.Parameters.Add(new MySqlParameter("@email", email));
                MySqlDataReader dataReader = command.ExecuteReader();
                found = new Member()
                {
                    ID = dataReader.GetInt32("ID"),
                    Username = dataReader.GetString("Username"),
                    Email = dataReader.GetString("Email"),
                    Password = dataReader.GetString("Password")
                };
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                connection.Close();
            }
            return found;
        }

        public Member GetMemberByUsername(string username)
        {
            Member found;
            MySqlConnection connection = new MySqlConnection(dbContext.connectionString);
            try
            {
                connection.Open();
                MySqlCommand command = new MySqlCommand("SELECT * FROM Member WHERE Username=@username", connection);
                command.Parameters.Add(new MySqlParameter("@username", username));
                MySqlDataReader dataReader = command.ExecuteReader();
                found = new Member()
                {
                    ID = dataReader.GetInt32("ID"),
                    Username = dataReader.GetString("Username"),
                    Email = dataReader.GetString("Email"),
                    Password = dataReader.GetString("Password")
                };
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                connection.Close();
            }
            return found;
        }

        public void MemberEmailOrUsername(string parameter, string entry)
        {
            MySqlConnection connection = new MySqlConnection(dbContext.connectionString);
            try
            {
                connection.Open();
                MySqlCommand command = new MySqlCommand("SELECT * FROM Member WHERE Username=@username", connection);
                command.Parameters.Add(new MySqlParameter("@username", username));
                MySqlDataReader dataReader = command.ExecuteReader();
                found = new Member()
                {
                    ID = dataReader.GetInt32("ID"),
                    Username = dataReader.GetString("Username"),
                    Email = dataReader.GetString("Email"),
                    Password = dataReader.GetString("Password")
                };
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                connection.Close();
            }
        }
    }
}
