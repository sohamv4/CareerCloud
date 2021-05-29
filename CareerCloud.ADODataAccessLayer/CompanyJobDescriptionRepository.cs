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
    public class CompanyJobDescriptionRepository : IDataRepository<CompanyJobDescriptionPoco>
    {
        public string connStr = ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString;
        public void Add(params CompanyJobDescriptionPoco[] items)
        {
            //throw new NotImplementedException();
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                foreach (CompanyJobDescriptionPoco poco in items)
                {
                    cmd.CommandText = @" INSERT INTO[dbo].[Company_Jobs_Descriptions]
                                    ([Id]
                                     ,[Job]
                                     ,[Job_Name]
                                    ,[Job_Descriptions])
                                VALUES
                                        (@Id,@Job, @Job_Name,@Job_Descriptions)";
                    cmd.Parameters.AddWithValue("@Id", poco.Id);
                    cmd.Parameters.AddWithValue("@Job", poco.Job);
                    cmd.Parameters.AddWithValue("@Job_Name", poco.JobName);
                    cmd.Parameters.AddWithValue("@Job_Descriptions", poco.JobDescriptions);
                    
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

        public IList<CompanyJobDescriptionPoco> GetAll(params System.Linq.Expressions.Expression<Func<CompanyJobDescriptionPoco, object>>[] navigationProperties)
        {
            //throw new NotImplementedException();
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = @"SELECT * FROM  [dbo].[Company_Jobs_Descriptions]";
                conn.Open();
                SqlDataReader rdr = cmd.ExecuteReader();
                CompanyJobDescriptionPoco[] Temp = new CompanyJobDescriptionPoco[1100];
                int index = 0;
                while (rdr.Read())
                {
                    CompanyJobDescriptionPoco poco = new CompanyJobDescriptionPoco();
                    poco.Id = rdr.GetGuid(0);
                    poco.Job = rdr.GetGuid(1);
                    poco.JobName= rdr.IsDBNull(2) ? null : rdr.GetString(2);
                    poco.JobDescriptions = rdr.IsDBNull(3) ? null : rdr.GetString(3);
                    poco.TimeStamp = rdr.IsDBNull(4) ? null : (byte[])rdr["Time_Stamp"];
                    Temp[index] = poco;
                    index++;
                }
                return Temp.Where(t => t != null).ToList();
            }

        }

        public IList<CompanyJobDescriptionPoco> GetList(Func<CompanyJobDescriptionPoco, bool> where, params System.Linq.Expressions.Expression<Func<CompanyJobDescriptionPoco, object>>[] navigationProperties)
        {
            //throw new NotImplementedException();
            return this.GetAll().Where(where).ToList();
        }

        public CompanyJobDescriptionPoco GetSingle(Func<CompanyJobDescriptionPoco, bool> where, params System.Linq.Expressions.Expression<Func<CompanyJobDescriptionPoco, object>>[] navigationProperties)
        {
            //throw new NotImplementedException();
            return this.GetAll().Where(where).FirstOrDefault();
        }

        public void Remove(params CompanyJobDescriptionPoco[] items)
        {
            //throw new NotImplementedException();
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                conn.Open();
                foreach (var item in items)
                {
                    cmd.CommandText = @"DELETE FROM Company_Jobs_Descriptions WHERE ID = @ID";
                    cmd.Parameters.AddWithValue("@ID", item.Id);
                    cmd.ExecuteNonQuery();

                }
            }
        }

        public void Update(params CompanyJobDescriptionPoco[] items)
        {
            //throw new NotImplementedException();

            using (SqlConnection conn = new SqlConnection(connStr))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                conn.Open();
                foreach (CompanyJobDescriptionPoco poco in items)
                {
                    cmd.CommandText = @"UPDATE Company_Jobs_Descriptions
                        SET [Id] = @Id,
                            [Job] =@Job,
                            [Job_Name] =@Job_Name,
                            [Job_Descriptions] =@Job_Descriptions
                                                                                   
                    WHERE Id = @Id";

                    cmd.Parameters.AddWithValue("@Id", poco.Id);
                    cmd.Parameters.AddWithValue("@Job", poco.Job);
                    cmd.Parameters.AddWithValue("@Job_Name", poco.JobName);
                    cmd.Parameters.AddWithValue("@Job_Descriptions", poco.JobDescriptions);

                    cmd.ExecuteNonQuery();

                }
            }
        }
    }
}
