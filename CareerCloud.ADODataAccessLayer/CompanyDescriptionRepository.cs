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
    public class CompanyDescriptionRepository : IDataRepository<CompanyDescriptionPoco>
    {
        public string connStr = ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString;
        public void Add(params CompanyDescriptionPoco[] items)
        {
            //throw new NotImplementedException();
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                foreach (CompanyDescriptionPoco poco in items)
                {
                    cmd.CommandText = @" INSERT INTO[dbo].[Company_Descriptions]
                                    ([Id]
                                     ,[Company]
                                    ,[LanguageID]
                                    ,[Company_Name]
                                    ,[Company_Description])
                                         VALUES
                                        (@Id,@Company, @LanguageID,@Company_Name,@Company_Description)";
                    cmd.Parameters.AddWithValue("@Id", poco.Id);
                    cmd.Parameters.AddWithValue("@Company", poco.Company);
                    cmd.Parameters.AddWithValue("@LanguageID", poco.LanguageId);
                    cmd.Parameters.AddWithValue("@Company_Name", poco.CompanyName);
                    cmd.Parameters.AddWithValue("@Company_Description", poco.CompanyDescription);
                   
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

        public IList<CompanyDescriptionPoco> GetAll(params System.Linq.Expressions.Expression<Func<CompanyDescriptionPoco, object>>[] navigationProperties)
        {
            //throw new NotImplementedException();
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = @"SELECT * FROM  [dbo].[Company_Descriptions]";
                conn.Open();
                SqlDataReader rdr = cmd.ExecuteReader();
                CompanyDescriptionPoco[] Temp = new CompanyDescriptionPoco[1000];
                int index = 0;
                while (rdr.Read())
                {
                    CompanyDescriptionPoco poco = new CompanyDescriptionPoco();
                    poco.Id = rdr.GetGuid(0);
                    poco.Company= rdr.GetGuid(1);
                    poco.LanguageId = rdr.GetString(2);
                    poco.CompanyName= rdr.GetString(3);
                    poco.CompanyDescription= rdr.GetString(4);
                    poco.TimeStamp = (byte[])rdr["Time_Stamp"];
                    Temp[index] = poco;
                    index++;
                }
                return Temp.Where(t => t != null).ToList();
            }

        }

        public IList<CompanyDescriptionPoco> GetList(Func<CompanyDescriptionPoco, bool> where, params System.Linq.Expressions.Expression<Func<CompanyDescriptionPoco, object>>[] navigationProperties)
        {
            //throw new NotImplementedException();
            return this.GetAll().Where(where).ToList();
        }

        public CompanyDescriptionPoco GetSingle(Func<CompanyDescriptionPoco, bool> where, params System.Linq.Expressions.Expression<Func<CompanyDescriptionPoco, object>>[] navigationProperties)
        {
            //throw new NotImplementedException();
            return this.GetAll().Where(where).FirstOrDefault();
        }

        public void Remove(params CompanyDescriptionPoco[] items)
        {
            //throw new NotImplementedException();
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                conn.Open();
                foreach (var item in items)
                {
                    cmd.CommandText = @"DELETE FROM Company_Descriptions WHERE ID = @ID";
                    cmd.Parameters.AddWithValue("@ID", item.Id);
                    cmd.ExecuteNonQuery();

                }
            }
        }

        public void Update(params CompanyDescriptionPoco[] items)
        {
            // throw new NotImplementedException();
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                conn.Open();
                foreach (CompanyDescriptionPoco poco in items)
                {
                    cmd.CommandText = @"UPDATE Company_Descriptions
                        SET [Id] = @Id,
                            [Company] =@Company,
                            [LanguageID] =@LanguageID,
                            [Company_Name] =@Company_Name,
                            [Company_Description] =@Company_Description
                                                        
                    WHERE Id = @Id";

                    cmd.Parameters.AddWithValue("@Id", poco.Id);
                    cmd.Parameters.AddWithValue("@Company", poco.Company);
                    cmd.Parameters.AddWithValue("@LanguageID", poco.LanguageId);
                    cmd.Parameters.AddWithValue("@Company_Name", poco.CompanyName);
                    cmd.Parameters.AddWithValue("@Company_Description", poco.CompanyDescription);

                    cmd.ExecuteNonQuery();

                }
            }
        }
    }
}
