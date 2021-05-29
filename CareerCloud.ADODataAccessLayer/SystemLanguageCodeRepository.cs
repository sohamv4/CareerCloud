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
   public class SystemLanguageCodeRepository : IDataRepository<SystemLanguageCodePoco>
    {
        public string connStr = ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString;
        public void Add(params SystemLanguageCodePoco[] items)
        {
            //throw new NotImplementedException();
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                foreach (SystemLanguageCodePoco poco in items)
                {
                    cmd.CommandText = @"INSERT INTO [dbo].[System_Language_Codes]
                                    ([LanguageID]
                                    ,[Name]
                                    ,[Native_Name])
                                VALUES
                                        (@LanguageID,@Name,@Native_Name)";
                    cmd.Parameters.AddWithValue("@LanguageID", poco.LanguageID);
                    cmd.Parameters.AddWithValue("@Name", poco.Name);
                    cmd.Parameters.AddWithValue("@Native_Name", poco.NativeName);

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

        public IList<SystemLanguageCodePoco> GetAll(params System.Linq.Expressions.Expression<Func<SystemLanguageCodePoco, object>>[] navigationProperties)
        {
            //throw new NotImplementedException();
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = @"SELECT * FROM  [dbo].[System_Language_Codes]";
                conn.Open();
                SqlDataReader rdr = cmd.ExecuteReader();
                SystemLanguageCodePoco[] Temp = new SystemLanguageCodePoco[1000];
                int index = 0;
                while (rdr.Read())
                {
                    SystemLanguageCodePoco poco = new SystemLanguageCodePoco();
                    poco.LanguageID = rdr.GetString(0);
                    poco.Name = rdr.GetString(1);
                    poco.NativeName = rdr.GetString(2);
                    Temp[index] = poco;
                    index++;
                }
                return Temp.Where(t => t != null).ToList();

            }


        }

        public IList<SystemLanguageCodePoco> GetList(Func<SystemLanguageCodePoco, bool> where, params System.Linq.Expressions.Expression<Func<SystemLanguageCodePoco, object>>[] navigationProperties)
        {
            //throw new NotImplementedException();
            return this.GetAll().Where(where).ToList();
        }

        public SystemLanguageCodePoco GetSingle(Func<SystemLanguageCodePoco, bool> where, params System.Linq.Expressions.Expression<Func<SystemLanguageCodePoco, object>>[] navigationProperties)
        {
            //throw new NotImplementedException();
            return this.GetAll().Where(where).FirstOrDefault();
        }

        public void Remove(params SystemLanguageCodePoco[] items)
        {
            //throw new NotImplementedException();
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                conn.Open();
                foreach (var item in items)
                {
                    cmd.CommandText = @"DELETE FROM System_Language_Codes WHERE LanguageID = @LanguageID";
                    cmd.Parameters.AddWithValue("@LanguageID", item.LanguageID);
                    cmd.ExecuteNonQuery();

                }
            }
        }

        public void Update(params SystemLanguageCodePoco[] items)
        {
            //throw new NotImplementedException();
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                conn.Open();
                foreach (SystemLanguageCodePoco poco in items)
                {
                    cmd.CommandText = @"UPDATE System_Language_Codes
                        SET [LanguageID] = @LanguageID,
                            [Name] =@Name,
                            [Native_Name] =@Native_Name
                     WHERE LanguageID = @LanguageID";

                    cmd.Parameters.AddWithValue("@LanguageID", poco.LanguageID);
                    cmd.Parameters.AddWithValue("@Name", poco.Name);
                    cmd.Parameters.AddWithValue("@Native_Name", poco.NativeName);

                    cmd.ExecuteNonQuery();

                }
            }
        }
    }
}
