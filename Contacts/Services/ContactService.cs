using Contacts.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contacts.DAL.Services
{
    public class ContactService
    {
        private SqlConnection oConn;

        public ContactService(SqlConnection oConn)
        {
            this.oConn = oConn;
        }

        public int AddContact(Contact contact)
        {
            SqlTransaction t = null;
            try
            {
                oConn.Open();
                t = oConn.BeginTransaction();
                SqlCommand cmd = oConn.CreateCommand();
                cmd.Transaction = t;
                cmd.CommandText = "INSERT INTO Contacts(Nom, Prenom, TEL, EMAIL) OUTPUT inserted.Id VALUES (@p1,@p2,@p3,@p4)";
                cmd.Parameters.AddWithValue("p1", contact.Nom);
                cmd.Parameters.AddWithValue("p2", contact.Prenom);
                cmd.Parameters.AddWithValue("p3", contact.Tel);
                cmd.Parameters.AddWithValue("p4", contact.Email);
                int id = (int)cmd.ExecuteScalar();
                t.Commit();
                return id;
            }
            catch (Exception)
            {
                t.Rollback();
                throw;
            }
            finally
            {
                oConn.Close();
            }

        }
        public bool Delete(int id)
        {
            SqlTransaction t = null;
            try
            {
                oConn.Open();
                t = oConn.BeginTransaction();
                SqlCommand cmd = oConn.CreateCommand();
                cmd.Transaction = t;
                cmd.CommandText = "DELETE FROM Contacts WHERE ID = @p1";
                cmd.Parameters.AddWithValue("p1", id);
                t.Commit();
                return cmd.ExecuteNonQuery() != 0;
            }
            catch (Exception ex)
            {
                t?.Rollback();
                throw;
            }
            finally
            {
                if (oConn.State != System.Data.ConnectionState.Closed)
                {
                    try
                    {
                        oConn.Close();
                    }
                    catch (Exception ex)
                    {

                        throw;
                    }
                }
            }
        }
        public bool Update(Contact contact)
        {
            SqlTransaction t = null;
            try
            {
                oConn.Open();
                t = oConn.BeginTransaction();
                SqlCommand cmd = oConn.CreateCommand();
                cmd.Transaction = t;
                cmd.CommandText = "UPDATE Contacts SET Nom = @p1, Prenom = @p2, TEL = @p3, EMAIL = @p4 WHERE ID = @p5";
                cmd.Parameters.AddWithValue("p1", contact.Nom);
                cmd.Parameters.AddWithValue("p2", contact.Prenom);
                cmd.Parameters.AddWithValue("p3", contact.Tel);
                cmd.Parameters.AddWithValue("p4", contact.Email);
                cmd.Parameters.AddWithValue("p5", contact.Id);
                t.Commit();
                return cmd.ExecuteNonQuery() != 0;
            }
            catch (Exception ex)
            {
                t?.Rollback();
                throw;
            }
            finally
            {
                if (oConn.State != System.Data.ConnectionState.Closed)
                {
                    try
                    {
                        oConn.Close();
                    }
                    catch (Exception ex)
                    {

                        throw;
                    }
                }
            }
        }
        public List<Contact> GetContact()
        {
            SqlTransaction t = null;
            try
            {
                oConn.Open();
                t = oConn.BeginTransaction();
                SqlCommand cmd = oConn.CreateCommand();
                cmd.Transaction = t;
                cmd.CommandText = "SELECT * FROM Contacts";

                SqlDataReader reader = cmd.ExecuteReader();
                List<Contact> result = new List<Contact>();
                while (reader.Read())
                {
                    result.Add(new Contact
                    {
                        Id = (int)reader["ID"],
                        Nom = (string)reader["Nom"],
                        Prenom = (string)reader["Prenom"],
                        Tel = (string)reader["Tel"],
                        Email = (string)reader["Email"],
                    });
                }
                reader.Close();
                t.Commit();
                return result;
            }
            catch (Exception ex)
            {
                t?.Rollback();
                throw;
            }
            finally
            {
                if (oConn.State != System.Data.ConnectionState.Closed)
                {
                    try
                    {
                        oConn.Close();
                    }
                    catch (Exception ex)
                    {

                        throw;
                    }
                }
            }
        }

    }


}
