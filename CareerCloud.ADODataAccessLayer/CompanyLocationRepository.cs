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
    public class CompanyLocationRepository : IDataRepository<CompanyLocationPoco>
    {
        public string connStr = ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString;
        public void Add(params CompanyLocationPoco[] items)
        {
            //throw new NotImplementedException();
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                foreach (CompanyLocationPoco poco in items)
                {
                    cmd.CommandText = @"INSERT INTO[dbo].[Company_Locations]
                                    ([Id]
                                    ,[Company]
                                    ,[Country_Code]
                                    ,[State_Province_Code]
                                    ,[Street_Address]
                                    ,[City_Town]
                                    ,[Zip_Postal_Code])
                                VALUES
                                        (@Id,@Company, @Country_Code,@State_Province_Code,@Street_Address,@City_Town, @Zip_Postal_Code)";
                    cmd.Parameters.AddWithValue("@Id", poco.Id);
                    cmd.Parameters.AddWithValue("@Company", poco.Company);
                    cmd.Parameters.AddWithValue("@Country_Code", poco.CountryCode);
                    cmd.Parameters.AddWithValue("@State_Province_Code", poco.Province);
                    cmd.Parameters.AddWithValue("@Street_Address", poco.Street);
                    cmd.Parameters.AddWithValue("@City_Town", poco.City);
                    cmd.Parameters.AddWithValue("@Zip_Postal_Code", poco.PostalCode);

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

        public IList<CompanyLocationPoco> GetAll(params System.Linq.Expressions.Expression<Func<CompanyLocationPoco, object>>[] navigationProperties)
        {
            //throw new NotImplementedException();
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = @"SELECT * FROM  [dbo].[Company_Locations]";
                conn.Open();
                SqlDataReader rdr = cmd.ExecuteReader();
                CompanyLocationPoco[] Temp = new CompanyLocationPoco[1000];
                int index = 0;
                while (rdr.Read())
                {
                    CompanyLocationPoco poco = new CompanyLocationPoco();
                    poco.Id = rdr.GetGuid(0);
                    poco.Company = rdr.GetGuid(1);
                    poco.CountryCode = rdr.GetString(2);
                    poco.Province = rdr.IsDBNull(3) ? null : rdr.GetString(3);
                    poco.Street = rdr.IsDBNull(4) ? null : rdr.GetString(4);
                    poco.City = rdr.IsDBNull(5) ? null : rdr.GetString(5);
                    poco.PostalCode = rdr.IsDBNull(6) ? null : rdr.GetString(6);
                    poco.TimeStamp = (byte[])rdr["Time_Stamp"];


                    Temp[index] = poco;
                    index++;
                }
                return Temp.Where(t => t != null).ToList();

            }
        }
        public IList<CompanyLocationPoco> GetList(Func<CompanyLocationPoco, bool> where, params System.Linq.Expressions.Expression<Func<CompanyLocationPoco, object>>[] navigationProperties)
        {
            //throw new NotImplementedException();
            return this.GetAll().Where(where).ToList();
        }

        public CompanyLocationPoco GetSingle(Func<CompanyLocationPoco, bool> where, params System.Linq.Expressions.Expression<Func<CompanyLocationPoco, object>>[] navigationProperties)
        {
            //throw new NotImplementedException();
            return this.GetAll().Where(where).FirstOrDefault();
        }

        public void Remove(params CompanyLocationPoco[] items)
        {
            //throw new NotImplementedException();
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                conn.Open();
                foreach (var item in items)
                {
                    cmd.CommandText = @"DELETE FROM Company_Locations WHERE ID = @ID";
                    cmd.Parameters.AddWithValue("@ID", item.Id);
                    cmd.ExecuteNonQuery();

                }
            }
        }

        public void Update(params CompanyLocationPoco[] items)
        {
            //throw new NotImplementedException();
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                conn.Open();
                foreach (CompanyLocationPoco poco in items)
                {
                    cmd.CommandText = @"UPDATE Company_Locations
                        SET [Id] = @Id,
                            [Company] =@Company,
                            [Country_Code] =@Country_Code,
                            [State_Province_Code] =@State_Province_Code,
                            [Street_Address] =@Street_Address,
                            [City_Town] =@City_Town,
                            [Zip_Postal_Code] =@Zip_Postal_Code
                                                                                   
                    WHERE Id = @Id";

                    cmd.Parameters.AddWithValue("@Id", poco.Id);
                    cmd.Parameters.AddWithValue("@Company", poco.Company);
                    cmd.Parameters.AddWithValue("@Country_Code", poco.CountryCode);
                    cmd.Parameters.AddWithValue("@State_Province_Code", poco.Province);
                    cmd.Parameters.AddWithValue("@Street_Address", poco.Street);
                    cmd.Parameters.AddWithValue("@City_Town", poco.City);
                    cmd.Parameters.AddWithValue("@Zip_Postal_Code", poco.PostalCode);

                    cmd.ExecuteNonQuery();

                }
            }
        }
    }
}
