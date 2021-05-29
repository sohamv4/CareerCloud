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
   
    public class ApplicantEducationRepository : IDataRepository<ApplicantEducationPoco>
    {
        public string connStr = ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString;
        public void Add(params ApplicantEducationPoco[] items)
        {
            //throw new NotImplementedException();
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                foreach (ApplicantEducationPoco poco in items)
                {
                    cmd.CommandText = @"INSERT INTO[dbo].[Applicant_Educations]
                                        ([Id]
                                        ,[Applicant]
                                        ,[Major]
                                        ,[Certificate_Diploma]
                                        ,[Start_Date]
                                        ,[Completion_Date]
                                        ,[Completion_Percent])
                                VALUES
                                        (@Id,@Applicant,@Major, @Certificate_Diploma,@Start_Date,@Completion_Date,@Completion_Percent)";
                    cmd.Parameters.AddWithValue("@Id", poco.Id);
                    cmd.Parameters.AddWithValue("@Applicant", poco.Applicant);
                    cmd.Parameters.AddWithValue("@Major", poco.Major);
                    cmd.Parameters.AddWithValue("@Certificate_Diploma", poco.CertificateDiploma);
                    cmd.Parameters.AddWithValue("@Start_Date", poco.StartDate);
                    cmd.Parameters.AddWithValue("@Completion_Date", poco.CompletionDate);
                    cmd.Parameters.AddWithValue("@Completion_Percent", poco.CompletionPercent);
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

        public IList<ApplicantEducationPoco> GetAll(params System.Linq.Expressions.Expression<Func<ApplicantEducationPoco, object>>[] navigationProperties)
        {
            //throw new NotImplementedException();
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = @"SELECT * FROM  [dbo].[Applicant_Educations]";
                conn.Open();
                SqlDataReader rdr = cmd.ExecuteReader();
                ApplicantEducationPoco[] Temp = new ApplicantEducationPoco[1000];
                int index = 0;
                    while (rdr.Read())
                    {
                    ApplicantEducationPoco poco = new ApplicantEducationPoco();
                    poco.Id = rdr.GetGuid(0);
                    poco.Applicant = rdr.GetGuid(1);
                    poco.Major = rdr.GetString(2);
                    poco.CertificateDiploma =rdr.IsDBNull(3) ? null : rdr.GetString(3);
                    poco.StartDate = rdr.IsDBNull(4) ? null : (DateTime?)rdr.GetDateTime(4);
                    poco.CompletionDate = rdr.IsDBNull(5) ? null : (DateTime?)rdr.GetDateTime(5);
                    poco.CompletionPercent = rdr.IsDBNull(6) ? null : (Byte?)rdr.GetByte(6);
                    poco.TimeStamp = (byte[]) rdr["Time_Stamp"];
                    Temp[index] = poco;
                    index ++;
                    }
                return Temp.Where(t => t != null).ToList();
            }
            
        }

        public IList<ApplicantEducationPoco> GetList(Func<ApplicantEducationPoco, bool> where, params System.Linq.Expressions.Expression<Func<ApplicantEducationPoco, object>>[] navigationProperties)
        {
            //throw new NotImplementedException();
            return this.GetAll().Where(where).ToList();
        }

        public ApplicantEducationPoco GetSingle(Func<ApplicantEducationPoco, bool> where, params System.Linq.Expressions.Expression<Func<ApplicantEducationPoco, object>>[] navigationProperties)
        {
            //throw new NotImplementedException();

            return this.GetAll().Where(where).FirstOrDefault();

        }

        public void Remove(params ApplicantEducationPoco[] items)
        {
            //throw new NotImplementedException();
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                conn.Open();
                foreach (var item in items)
                {
                    cmd.CommandText = @"DELETE FROM Applicant_Educations WHERE ID = @ID";
                    cmd.Parameters.AddWithValue("@ID", item.Id);
                    cmd.ExecuteNonQuery();          

                }
            }
        }

        public void Update(params ApplicantEducationPoco[] items)
        {
            //throw new NotImplementedException();
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                conn.Open();
                foreach (ApplicantEducationPoco poco in items)
                {
                    cmd.CommandText = @"UPDATE Applicant_Educations
                        SET [Id] = @Id,
                            [Applicant] =@Applicant,
                            [Major] =@Major,
                            [Certificate_Diploma] =@Certificate_Diploma,
                            [Start_Date] =@Start_Date,
                            [Completion_Date] =@Completion_Date,
                            [Completion_Percent] =@Completion_Percent
                    WHERE Id = @Id";

                    cmd.Parameters.AddWithValue("@Id", poco.Id);
                    cmd.Parameters.AddWithValue("@Applicant", poco.Applicant);
                    cmd.Parameters.AddWithValue("@Major", poco.Major);
                    cmd.Parameters.AddWithValue("@Certificate_Diploma", poco.CertificateDiploma);
                    cmd.Parameters.AddWithValue("@Start_Date", poco.StartDate);
                    cmd.Parameters.AddWithValue("@Completion_Date", poco.CompletionDate);
                    cmd.Parameters.AddWithValue("@Completion_Percent", poco.CompletionPercent);
                    
                    cmd.ExecuteNonQuery();
                    
                }
            }

        }
    }
}
