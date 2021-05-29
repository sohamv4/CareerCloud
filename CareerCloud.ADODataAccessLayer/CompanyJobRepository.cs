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
    public class CompanyJobRepository : IDataRepository<CompanyJobPoco>
    {
        public string connStr = ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString;
        public void Add(params CompanyJobPoco[] items)
        {
            //throw new NotImplementedException();

            using (SqlConnection conn = new SqlConnection(connStr))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                foreach (CompanyJobPoco poco in items)
                {
                    cmd.CommandText = @" INSERT INTO[dbo].[Company_Jobs]
                                    ([Id]
                                    ,[Company]
                                    ,[Profile_Created]
                                    ,[Is_Inactive]
                                    ,[Is_Company_Hidden])
                                VALUES
                                        (@Id,@Company, @Profile_Created,@Is_Inactive,@Is_Company_Hidden)";
                    cmd.Parameters.AddWithValue("@Id", poco.Id);
                    cmd.Parameters.AddWithValue("@Company", poco.Company);
                    cmd.Parameters.AddWithValue("@Profile_Created", poco.ProfileCreated);
                    cmd.Parameters.AddWithValue("@Is_Inactive", poco.IsInactive);
                    cmd.Parameters.AddWithValue("@Is_Company_Hidden", poco.IsCompanyHidden);
                    
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

        public IList<CompanyJobPoco> GetAll(params System.Linq.Expressions.Expression<Func<CompanyJobPoco, object>>[] navigationProperties)
        {
            //throw new NotImplementedException();
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = @"SELECT * FROM  [dbo].[Company_Jobs]";
                conn.Open();
                SqlDataReader rdr = cmd.ExecuteReader();
                CompanyJobPoco[] Temp = new CompanyJobPoco[1100];
                int index = 0;
                while (rdr.Read())
                {
                    CompanyJobPoco poco = new CompanyJobPoco();
                    poco.Id = rdr.GetGuid(0);
                    poco.Company = rdr.GetGuid(1);
                    poco.ProfileCreated = rdr.GetDateTime(2);
                    poco.IsInactive = rdr.GetBoolean(3);
                    poco.IsCompanyHidden = rdr.GetBoolean(4);
                    poco.TimeStamp = (byte[])rdr["Time_Stamp"];
                    Temp[index] = poco;
                    index++;
                }
                return Temp.Where(t => t != null).ToList();
            }

        }

        public IList<CompanyJobPoco> GetList(Func<CompanyJobPoco, bool> where, params System.Linq.Expressions.Expression<Func<CompanyJobPoco, object>>[] navigationProperties)
        {
            //throw new NotImplementedException();
            return this.GetAll().Where(where).ToList();
        }

        public CompanyJobPoco GetSingle(Func<CompanyJobPoco, bool> where, params System.Linq.Expressions.Expression<Func<CompanyJobPoco, object>>[] navigationProperties)
        {
            //throw new NotImplementedException();
            return this.GetAll().Where(where).FirstOrDefault();
        }

        public void Remove(params CompanyJobPoco[] items)
        {
            //throw new NotImplementedException();
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                conn.Open();
                foreach (var item in items)
                {
                    cmd.CommandText = @"DELETE FROM Company_Jobs WHERE ID = @ID";
                    cmd.Parameters.AddWithValue("@ID", item.Id);
                    cmd.ExecuteNonQuery();

                }
            }
        }

        public void Update(params CompanyJobPoco[] items)
        {
            //throw new NotImplementedException();
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                conn.Open();
                foreach (CompanyJobPoco poco in items)
                {
                    cmd.CommandText = @"UPDATE Company_Jobs
                        SET [Id] = @Id,
                            [Company] =@Company,
                            [Profile_Created] =@Profile_Created,
                            [Is_Inactive] =@Is_Inactive,
                            [Is_Company_Hidden] =@Is_Company_Hidden
                                                                                   
                    WHERE Id = @Id";
                    cmd.Parameters.AddWithValue("@Id", poco.Id);
                    cmd.Parameters.AddWithValue("@Company", poco.Company);
                    cmd.Parameters.AddWithValue("@Profile_Created", poco.ProfileCreated);
                    cmd.Parameters.AddWithValue("@Is_Inactive", poco.IsInactive);
                    cmd.Parameters.AddWithValue("@Is_Company_Hidden", poco.IsCompanyHidden);

                    cmd.ExecuteNonQuery();

                }
            }
        }
    }
}
