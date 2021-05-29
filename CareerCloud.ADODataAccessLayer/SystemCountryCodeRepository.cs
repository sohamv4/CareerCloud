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
    public class SystemCountryCodeRepository : IDataRepository<SystemCountryCodePoco>
    {
        public string connStr = ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString;
        public void Add(params SystemCountryCodePoco[] items)
        {
             //throw new NotImplementedException();
            
            using (SqlConnection conn=new SqlConnection(connStr))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                conn.Open();
                foreach (SystemCountryCodePoco poco in items)
                {
                    cmd.CommandText =@"INSERT INTO [dbo].[System_Country_Codes]
                                        ([Code]
                                        ,[Name])
                                VALUES(@Code,@Name)";
                     cmd.Parameters.AddWithValue("@Code", poco.Code);
                     cmd.Parameters.AddWithValue("@Name", poco.Name);
                     
                    cmd.ExecuteNonQuery();
                    
                }
            }
        }
        public void CallStoredProc(string name, params Tuple<string, string>[] parameters)
        {
            throw new NotImplementedException();
        }

        public IList<SystemCountryCodePoco> GetAll(params System.Linq.Expressions.Expression<Func<SystemCountryCodePoco, object>>[] navigationProperties)
        {
            //throw new NotImplementedException();
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = @"SELECT * FROM  [dbo].[System_Country_Codes]";
                conn.Open();
                SqlDataReader rdr = cmd.ExecuteReader();
                SystemCountryCodePoco[] Temp = new SystemCountryCodePoco[1000];
                int index = 0;
                while (rdr.Read())
                {
                    SystemCountryCodePoco poco = new SystemCountryCodePoco();
                    poco.Code = rdr.GetString(0);
                    poco.Name = rdr.GetString(1);

                    Temp[index] = poco;
                    index++;
                }
                return Temp.Where(t => t != null).ToList();

            }


        }

        public IList<SystemCountryCodePoco> GetList(Func<SystemCountryCodePoco, bool> where, params System.Linq.Expressions.Expression<Func<SystemCountryCodePoco, object>>[] navigationProperties)
        {
            //throw new NotImplementedException();
            return this.GetAll().Where(where).ToList();
        }

        public SystemCountryCodePoco GetSingle(Func<SystemCountryCodePoco, bool> where, params System.Linq.Expressions.Expression<Func<SystemCountryCodePoco, object>>[] navigationProperties)
        {
            //throw new NotImplementedException();
            return this.GetAll().Where(where).FirstOrDefault();
         }

        public void Remove(params SystemCountryCodePoco[] items)
        {
            //throw new NotImplementedException();
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                conn.Open();
                foreach (var item in items)
                {
                    cmd.CommandText = @"DELETE FROM System_Country_Codes WHERE Code = @Code";
                    cmd.Parameters.AddWithValue("@Code", item.Code);
                    cmd.ExecuteNonQuery();

                }
            }
        }

        public void Update(params SystemCountryCodePoco[] items)
        {
            //throw new NotImplementedException();
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                conn.Open();
                foreach (SystemCountryCodePoco poco in items)
                {
                    cmd.CommandText = @"UPDATE System_Country_Codes
                        SET [Code] = @Code,
                            [Name] =@Name
                     WHERE Code = @Code";

                    cmd.Parameters.AddWithValue("@Code", poco.Code);
                    cmd.Parameters.AddWithValue("@Name", poco.Name);
                    
                    cmd.ExecuteNonQuery();

                }
            }
        }
    }
}
