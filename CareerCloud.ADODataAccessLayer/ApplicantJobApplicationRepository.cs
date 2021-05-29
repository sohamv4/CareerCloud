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
    public class ApplicantJobApplicationRepository : IDataRepository<ApplicantJobApplicationPoco>
    {
        public string connStr = ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString;
        public void Add(params ApplicantJobApplicationPoco[] items)
        {
            //throw new NotImplementedException();
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                foreach (ApplicantJobApplicationPoco poco in items)
                {
                    cmd.CommandText = @"INSERT INTO[dbo].[Applicant_Job_Applications]
                                    ([Id]
                                    ,[Applicant]
                                    ,[Job]
                                    ,[Application_Date])
                                VALUES
                                        (@Id,@Applicant,@Job, @Applicant_Date)";
                    cmd.Parameters.AddWithValue("@Id", poco.Id);
                    cmd.Parameters.AddWithValue("@Applicant", poco.Applicant);
                    cmd.Parameters.AddWithValue("@Job", poco.Job);
                    cmd.Parameters.AddWithValue("@Applicant_Date", poco.ApplicationDate);

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

        public IList<ApplicantJobApplicationPoco> GetAll(params System.Linq.Expressions.Expression<Func<ApplicantJobApplicationPoco, object>>[] navigationProperties)
        {
            //throw new NotImplementedException();
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = @"SELECT * FROM  [dbo].[Applicant_Job_Applications]";
                conn.Open();
                SqlDataReader rdr = cmd.ExecuteReader();
                ApplicantJobApplicationPoco[] Temp = new ApplicantJobApplicationPoco[1000];
                int index = 0;
                while (rdr.Read())
                {
                    ApplicantJobApplicationPoco poco = new ApplicantJobApplicationPoco();
                    poco.Id = rdr.GetGuid(0);
                    poco.Applicant = rdr.GetGuid(1);
                    poco.Job = rdr.GetGuid(2);
                    poco.ApplicationDate = rdr.GetDateTime(3);
                    poco.TimeStamp = (byte[])rdr["Time_Stamp"];
                    Temp[index] = poco;
                    index++;
                }
                return Temp.Where(t => t != null).ToList();
            }


        }

        public IList<ApplicantJobApplicationPoco> GetList(Func<ApplicantJobApplicationPoco, bool> where, params System.Linq.Expressions.Expression<Func<ApplicantJobApplicationPoco, object>>[] navigationProperties)
        {
            //throw new NotImplementedException();
            return this.GetAll().Where(where).ToList();
        }

        public ApplicantJobApplicationPoco GetSingle(Func<ApplicantJobApplicationPoco, bool> where, params System.Linq.Expressions.Expression<Func<ApplicantJobApplicationPoco, object>>[] navigationProperties)
        {
            //throw new NotImplementedException();
            return this.GetAll().Where(where).FirstOrDefault();
        }

        public void Remove(params ApplicantJobApplicationPoco[] items)
        {
            //throw new NotImplementedException();
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                conn.Open();
                foreach (var item in items)
                {
                    cmd.CommandText = @"DELETE FROM Applicant_Job_Applications WHERE ID = @ID";
                    cmd.Parameters.AddWithValue("@ID", item.Id);
                    cmd.ExecuteNonQuery();

                }
            }
        }

        public void Update(params ApplicantJobApplicationPoco[] items)
        {
            //throw new NotImplementedException();
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                conn.Open();
                foreach (ApplicantJobApplicationPoco poco in items)
                {
                    cmd.CommandText = @"UPDATE Applicant_Job_Applications
                        SET [Id] = @Id,
                            [Applicant] =@Applicant,
                            [Job] =@Job,
                            [Application_Date] =@Application_Date
                            
                    WHERE Id = @Id";

                    cmd.Parameters.AddWithValue("@Id", poco.Id);
                    cmd.Parameters.AddWithValue("@Applicant", poco.Applicant);
                    cmd.Parameters.AddWithValue("@Job", poco.Job);
                    cmd.Parameters.AddWithValue("@Application_Date", poco.ApplicationDate);
                    
                    cmd.ExecuteNonQuery();

                }
            }
        }
    }
}
