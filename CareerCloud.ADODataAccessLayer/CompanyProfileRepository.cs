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
   public class CompanyProfileRepository : IDataRepository<CompanyProfilePoco>
    {
        public string connStr = ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString;
        public void Add(params CompanyProfilePoco[] items)
        {
            //throw new NotImplementedException();
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                foreach (CompanyProfilePoco poco in items)
                {
                    cmd.CommandText = @"INSERT INTO[dbo].[Company_Profiles]
                                        ([Id]
                                        ,[Registration_Date]
                                        ,[Company_Website]
                                        ,[Contact_Phone]
                                        ,[Contact_Name]
                                        ,[Company_Logo])
                                VALUES
                                        (@Id,@Registration_Date, @Company_Website,@Contact_Phone,@Contact_Name,@Company_Logo)";
                    cmd.Parameters.AddWithValue("@Id", poco.Id);
                    cmd.Parameters.AddWithValue("@Registration_Date", poco.RegistrationDate);
                    cmd.Parameters.AddWithValue("@Company_Website", poco.CompanyWebsite);
                    cmd.Parameters.AddWithValue("@Contact_Phone", poco.ContactPhone);
                    cmd.Parameters.AddWithValue("@Contact_Name", poco.ContactName);
                    cmd.Parameters.AddWithValue("@Company_Logo", poco.CompanyLogo);
                    

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

        public IList<CompanyProfilePoco> GetAll(params System.Linq.Expressions.Expression<Func<CompanyProfilePoco, object>>[] navigationProperties)
        {
            //throw new NotImplementedException();
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = @"SELECT * FROM  [dbo].[Company_Profiles]";
                conn.Open();
                SqlDataReader rdr = cmd.ExecuteReader();
                CompanyProfilePoco[] Temp = new CompanyProfilePoco[1000];
                int index = 0;
                while (rdr.Read())
                {
                    CompanyProfilePoco poco = new CompanyProfilePoco();
                    poco.Id = rdr.GetGuid(0);
                    poco.RegistrationDate = rdr.GetDateTime(1);
                    poco.CompanyWebsite = rdr.IsDBNull(2) ? null : rdr.GetString(2);
                    poco.ContactPhone = rdr.GetString(3);
                    poco.ContactName = rdr.IsDBNull(4) ? null : rdr.GetString(4);
                    poco.CompanyLogo = rdr.IsDBNull(5)?null:(byte[])rdr["Company_Logo"];
                    poco.TimeStamp = rdr.IsDBNull(6) ? null : (byte[])rdr["Time_Stamp"];



                    Temp[index] = poco;
                    index++;
                }
                return Temp.Where(t => t != null).ToList();

            }

        }

        public IList<CompanyProfilePoco> GetList(Func<CompanyProfilePoco, bool> where, params System.Linq.Expressions.Expression<Func<CompanyProfilePoco, object>>[] navigationProperties)
        {
            //throw new NotImplementedException();
            return this.GetAll().Where(where).ToList();
        }

        public CompanyProfilePoco GetSingle(Func<CompanyProfilePoco, bool> where, params System.Linq.Expressions.Expression<Func<CompanyProfilePoco, object>>[] navigationProperties)
        {
            //throw new NotImplementedException();
            return this.GetAll().Where(where).FirstOrDefault();
        }

        public void Remove(params CompanyProfilePoco[] items)
        {
            //throw new NotImplementedException();
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                conn.Open();
                foreach (var item in items)
                {
                    cmd.CommandText = @"DELETE FROM Company_Profiles WHERE ID = @ID";
                    cmd.Parameters.AddWithValue("@ID", item.Id);
                    cmd.ExecuteNonQuery();

                }
            }
        }

        public void Update(params CompanyProfilePoco[] items)
        {
            //throw new NotImplementedException();
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                conn.Open();
                foreach (CompanyProfilePoco poco in items)
                {
                    cmd.CommandText = @"UPDATE Company_Profiles
                        SET [Id] = @Id,
                            [Registration_Date] =@Registration_Date,
                            [Company_Website] =@Company_Website,
                            [Contact_Phone] =@Contact_Phone,
                            [Contact_Name] =@Contact_Name,
                            [Company_Logo] =@Company_Logo
                                                                                                               
                    WHERE Id = @Id";

                    cmd.Parameters.AddWithValue("@Id", poco.Id);
                    cmd.Parameters.AddWithValue("@Registration_Date", poco.RegistrationDate);
                    cmd.Parameters.AddWithValue("@Company_Website", poco.CompanyWebsite);
                    cmd.Parameters.AddWithValue("@Contact_Phone", poco.ContactPhone);
                    cmd.Parameters.AddWithValue("@Contact_Name", poco.ContactName);
                    cmd.Parameters.AddWithValue("@Company_Logo", poco.CompanyLogo);


                    cmd.ExecuteNonQuery();

                }
            }
        }
    }
}

