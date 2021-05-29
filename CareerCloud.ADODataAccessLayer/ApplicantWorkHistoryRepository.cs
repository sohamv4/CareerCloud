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
    public class ApplicantWorkHistoryRepository : IDataRepository<ApplicantWorkHistoryPoco>
    {
        public string connStr = ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString;
        public void Add(params ApplicantWorkHistoryPoco[] items)
        {
            //throw new NotImplementedException();
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                foreach (ApplicantWorkHistoryPoco poco in items)
                {
                    cmd.CommandText = @"INSERT INTO[dbo].[Applicant_Work_History]
                                        ([Id]
                                        ,[Applicant]
                                        ,[Company_Name]
                                        ,[Country_Code]
                                        ,[Location]
                                        ,[Job_Title]
                                        ,[Job_Description]
                                        ,[Start_Month]
                                        ,[Start_Year]
                                        ,[End_Month]
                                        ,[End_Year])
                                VALUES
                                        (@Id,@Applicant, @Company_Name,@Country_Code,@Location,@Job_Title,@Job_Description,@Start_Month,
                                        @Start_Year,@End_Month,@End_Year)";
                    cmd.Parameters.AddWithValue("@Id", poco.Id);
                    cmd.Parameters.AddWithValue("@Applicant", poco.Applicant);
                    cmd.Parameters.AddWithValue("@Company_Name", poco.CompanyName);
                    cmd.Parameters.AddWithValue("@Country_Code", poco.CountryCode);
                    cmd.Parameters.AddWithValue("@Location", poco.Location);
                    cmd.Parameters.AddWithValue("@Job_Title", poco.JobTitle);
                    cmd.Parameters.AddWithValue("@Job_Description", poco.JobDescription);
                    cmd.Parameters.AddWithValue("@Start_Month", poco.StartMonth);
                    cmd.Parameters.AddWithValue("@Start_Year", poco.StartYear);
                    cmd.Parameters.AddWithValue("@End_Month", poco.EndMonth);
                    cmd.Parameters.AddWithValue("@End_Year", poco.EndYear);

                    
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

        public IList<ApplicantWorkHistoryPoco> GetAll(params System.Linq.Expressions.Expression<Func<ApplicantWorkHistoryPoco, object>>[] navigationProperties)
        {
            //throw new NotImplementedException();
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = @"SELECT * FROM  [dbo].[Applicant_Work_History]";
                conn.Open();
                SqlDataReader rdr = cmd.ExecuteReader();
                ApplicantWorkHistoryPoco[] Temp = new ApplicantWorkHistoryPoco[1000];
                int index = 0;
                while (rdr.Read())
                {
                    ApplicantWorkHistoryPoco poco = new ApplicantWorkHistoryPoco();
                    poco.Id = rdr.GetGuid(0);
                    poco.Applicant = rdr.GetGuid(1);
                    poco.CompanyName = rdr.GetString(2);
                    poco.CountryCode= rdr.GetString(3);
                    poco.Location = rdr.GetString(4);
                    poco.JobTitle = rdr.GetString(5);
                    poco.JobDescription = rdr.GetString(6);
                    poco.StartMonth = rdr.GetInt16(7);
                    poco.StartYear = rdr.GetInt32(8);
                    poco.EndMonth = rdr.GetInt16(9);
                    poco.EndYear = rdr.GetInt32(10);
                    poco.TimeStamp = (byte[])rdr["Time_Stamp"];
                    Temp[index] = poco;
                    index++;
                }
                return Temp.Where(t => t != null).ToList();
            }

        }

        public IList<ApplicantWorkHistoryPoco> GetList(Func<ApplicantWorkHistoryPoco, bool> where, params System.Linq.Expressions.Expression<Func<ApplicantWorkHistoryPoco, object>>[] navigationProperties)
        {
            //throw new NotImplementedException();
            return this.GetAll().Where(where).ToList();
        }

        public ApplicantWorkHistoryPoco GetSingle(Func<ApplicantWorkHistoryPoco, bool> where, params System.Linq.Expressions.Expression<Func<ApplicantWorkHistoryPoco, object>>[] navigationProperties)
        {
            //throw new NotImplementedException();
            return this.GetAll().Where(where).FirstOrDefault();
        }

        public void Remove(params ApplicantWorkHistoryPoco[] items)
        {
            //throw new NotImplementedException();
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                conn.Open();
                foreach (var item in items)
                {
                    cmd.CommandText = @"DELETE FROM Applicant_Work_History WHERE ID = @ID";
                    cmd.Parameters.AddWithValue("@ID", item.Id);
                    cmd.ExecuteNonQuery();

                }
            }

        }

        public void Update(params ApplicantWorkHistoryPoco[] items)
        {
            // throw new NotImplementedException();

            using (SqlConnection conn = new SqlConnection(connStr))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                conn.Open();
                foreach (ApplicantWorkHistoryPoco poco in items)
                {
                    cmd.CommandText = @"UPDATE Applicant_Work_History
                        SET [Id] = @Id,
                            [Applicant] =@Applicant,
                            [Company_Name] =@Company_Name,
                            [Country_Code] =@Country_Code,
                            [Location] =@Location,
                            [Job_Title] =@Job_Title,
                            [Job_Description] =@Job_Description,
                            [Start_Month] =@Start_Month,
                            [Start_Year] =@Start_Year,
                            [End_Month] =@End_Month,
                            [End_Year] =@End_Year
                            
                    WHERE Id = @Id";

                    cmd.Parameters.AddWithValue("@Id", poco.Id);
                    cmd.Parameters.AddWithValue("@Applicant", poco.Applicant);
                    cmd.Parameters.AddWithValue("@Company_Name", poco.CompanyName);
                    cmd.Parameters.AddWithValue("@Country_Code", poco.CountryCode);
                    cmd.Parameters.AddWithValue("@Location", poco.Location);
                    cmd.Parameters.AddWithValue("@Job_Title", poco.JobTitle);
                    cmd.Parameters.AddWithValue("@Job_Description", poco.JobDescription);
                    cmd.Parameters.AddWithValue("@Start_Month", poco.StartMonth);
                    cmd.Parameters.AddWithValue("@Start_Year", poco.StartYear);
                    cmd.Parameters.AddWithValue("@End_Month", poco.EndMonth);
                    cmd.Parameters.AddWithValue("@End_Year", poco.EndYear);

                    cmd.ExecuteNonQuery();

                }
            }
        }
    }
}
