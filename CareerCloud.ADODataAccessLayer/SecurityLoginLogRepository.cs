using CareerCloud.DataAccessLayer;
using CareerCloud.Pocos;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Configuration;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace CareerCloud.ADODataAccessLayer
{
    public class SecurityLoginLogRepository : IDataRepository<SecurityLoginsLogPoco>
    {
        public string connStr = ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString;
        public void Add(params SecurityLoginsLogPoco[] items)
        {
            //throw new NotImplementedException();
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                foreach (SecurityLoginsLogPoco poco in items)
                {
                    cmd.CommandText = @"INSERT INTO [dbo].[Security_Logins_Log]
                                        ([Id]
                                        ,[Login]
                                        ,[Source_IP]
                                        ,[Logon_Date]
                                        ,[Is_Succesful])
                                VALUES
                                        (@Id,@Login, @Source_IP,@Logon_Date,@Is_Succesful)";
                    cmd.Parameters.AddWithValue("@Id", poco.Id);
                    cmd.Parameters.AddWithValue("@Login", poco.Login);
                    cmd.Parameters.AddWithValue("@Source_IP", poco.SourceIP);
                    cmd.Parameters.AddWithValue("@Logon_Date", poco.LogonDate);
                    cmd.Parameters.AddWithValue("@Is_Succesful", poco.IsSuccesful);
                    


                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();
                }
            }
        }

        public void CallStoredProc(string name, params Tuple<string, string>[] parameters)
        {
            throw new NotImplementedException();
        }

        public IList<SecurityLoginsLogPoco> GetAll(params System.Linq.Expressions.Expression<Func<SecurityLoginsLogPoco, object>>[] navigationProperties)
        {
            //throw new NotImplementedException();
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = @"SELECT * FROM  [dbo].[Security_Logins_Log]";
                conn.Open();
                SqlDataReader rdr = cmd.ExecuteReader();
                SecurityLoginsLogPoco[] Temp = new SecurityLoginsLogPoco[5000];
                int index = 0;
                while (rdr.Read())
                {
                    SecurityLoginsLogPoco poco = new SecurityLoginsLogPoco();
                    poco.Id = rdr.GetGuid(0);
                    poco.Login = rdr.GetGuid(1);
                    poco.SourceIP = rdr.GetString(2);
                    poco.LogonDate = rdr.GetDateTime(3);
                    poco.IsSuccesful = rdr.GetBoolean(4);

                    Temp[index] = poco;
                    index++;
                }
                return Temp.Where(t => t != null).ToList();

            }
        }
        public IList<SecurityLoginsLogPoco> GetList(Func<SecurityLoginsLogPoco, bool> where, params System.Linq.Expressions.Expression<Func<SecurityLoginsLogPoco, object>>[] navigationProperties)
        {
            //throw new NotImplementedException();
            return this.GetAll().Where(where).ToList();
        }

        public SecurityLoginsLogPoco GetSingle(Func<SecurityLoginsLogPoco, bool> where, params System.Linq.Expressions.Expression<Func<SecurityLoginsLogPoco, object>>[] navigationProperties)
        {
            //throw new NotImplementedException();
            return this.GetAll().Where(where).FirstOrDefault();
        }

        public void Remove(params SecurityLoginsLogPoco[] items)
        {
            //throw new NotImplementedException();
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                conn.Open();

                foreach (var item in items)
                {
                    cmd.CommandText = @"DELETE FROM Security_Logins_Log WHERE ID = @ID";
                    cmd.Parameters.AddWithValue("@ID", item.Id);
                    cmd.ExecuteNonQuery();

                }
            }
        }

        public void Update(params SecurityLoginsLogPoco[] items)
        {
            // throw new NotImplementedException();
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                conn.Open();
                foreach (SecurityLoginsLogPoco poco in items)
                {
                    cmd.CommandText = @"UPDATE Security_Logins_Log
                        SET [Id] = @Id,
                            [Login] =@Login,
                            [Source_IP] =@Source_IP,
                            [Logon_Date] =@Logon_Date,
                            [Is_Succesful] =@Is_Succesful
                                                                                                                                           
                    WHERE Id = @Id";

                    cmd.Parameters.AddWithValue("@Id", poco.Id);
                    cmd.Parameters.AddWithValue("@Login", poco.Login);
                    cmd.Parameters.AddWithValue("@Source_IP", poco.SourceIP);
                    cmd.Parameters.AddWithValue("@Logon_Date", poco.LogonDate);
                    cmd.Parameters.AddWithValue("@Is_Succesful", poco.IsSuccesful);


                    cmd.ExecuteNonQuery();

                }
            }
        }
    }
}
