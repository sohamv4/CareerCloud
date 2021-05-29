using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Data;
using System.Configuration;
using CareerCloud.Pocos;

namespace CareerCloud.EntityFrameworkDataAccess
{
	public class CareerCloudContext : DbContext
	{
		public CareerCloudContext() : base(ConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString)
		{
			this.Database.Log = s => System.Diagnostics.Debug.WriteLine(s);
		}
		protected override void OnModelCreating(DbModelBuilder modelBuilder)
		{
			//modelBuilder.Entity<ApplicantEducationPoco>()
			//	.Property(e => e.TimeStamp)
			//	.IsRowVersion();
			//modelBuilder.Entity<CompanyProfilePoco>()
			//	.HasMany(e => e.CompanyLocation)
			//	.WithRequired(e => e.CompanyProfile)
			//	.HasForeignKey(e => e.Company);

			modelBuilder.Entity<ApplicantProfilePoco>()
				.HasMany(e => e.ApplicantEducation)
				.WithRequired(e => e.ApplicantProfile)
				.HasForeignKey(e => e.Applicant)
				.WillCascadeOnDelete(false);
			modelBuilder.Entity<ApplicantProfilePoco>()
				.HasMany(e => e.ApplicantJobApplication)
				.WithRequired(e => e.ApplicantProfile)
				.HasForeignKey(e => e.Applicant)
				.WillCascadeOnDelete(false);

			modelBuilder.Entity<ApplicantProfilePoco>()
				.HasMany(e => e.ApplicantResume)
				.WithRequired(e => e.ApplicantProfile)
				.HasForeignKey(e => e.Applicant)
				.WillCascadeOnDelete(false);

			modelBuilder.Entity<ApplicantProfilePoco>()
				.HasMany(e => e.ApplicantSkill)
				.WithRequired(e => e.ApplicationProfile)
				.HasForeignKey(e => e.Applicant)
				.WillCascadeOnDelete(false);

			modelBuilder.Entity<ApplicantProfilePoco>()
				.HasMany(e => e.ApplicantWorkHistory)
				.WithRequired(e => e.ApplicantProfile)
				.HasForeignKey(e => e.Applicant)
				.WillCascadeOnDelete(false);

			modelBuilder.Entity<CompanyJobPoco>()
				.HasMany(e => e.ApplicationJobApplication)
				.WithRequired(e => e.CompanyJob)
				.HasForeignKey(e => e.Job)
				.WillCascadeOnDelete(false);

			modelBuilder.Entity<CompanyJobPoco>()
				.HasMany(e => e.CompanyJobDescription)
				.WithRequired(e => e.CompanyJob)
				.HasForeignKey(e => e.Job)
				.WillCascadeOnDelete(false);

			modelBuilder.Entity<CompanyJobPoco>()
				.HasMany(e => e.CompanyJobEducation)
				.WithRequired(e => e.CompanyJob)
				.HasForeignKey(e => e.Job)
				.WillCascadeOnDelete(false);

			modelBuilder.Entity<CompanyJobPoco>()
				.HasMany(e => e.CompanyJobSkill)
				.WithRequired(e => e.CompanyJob)
				.HasForeignKey(e => e.Job)
				.WillCascadeOnDelete(false);

			modelBuilder.Entity<CompanyProfilePoco>()
				.HasMany(e => e.CompanyDescription)
				.WithRequired(e => e.CompanyProfile)
				.HasForeignKey(e => e.Company)
				.WillCascadeOnDelete(false);

			modelBuilder.Entity<CompanyProfilePoco>()
				.HasMany(e => e.CompanyJob)
				.WithRequired(e => e.CompanyProfile)
				.HasForeignKey(e => e.Company)
				.WillCascadeOnDelete(false);

			modelBuilder.Entity<CompanyProfilePoco>()
				.HasMany(e => e.CompanyLocation)
				.WithRequired(e => e.CompanyProfile)
				.HasForeignKey(e => e.Company)
				.WillCascadeOnDelete(false);

			modelBuilder.Entity<SecurityLoginPoco>()
				.HasMany(e => e.ApplicantProfile)
				.WithRequired(e => e.SecurityLogin)
				.HasForeignKey(e => e.Login)
				.WillCascadeOnDelete(false);

			modelBuilder.Entity<SecurityLoginPoco>()
				.HasMany(e => e.SecurityLoginsRole)
				.WithRequired(e => e.SecurityLogin)
				.HasForeignKey(e => e.Login)
				.WillCascadeOnDelete(false);

			modelBuilder.Entity<SecurityLoginPoco>()
				.HasMany(e => e.SecurityLoginsLog)
				.WithRequired(e => e.SecurityLogin)
				.HasForeignKey(e => e.Login)
				.WillCascadeOnDelete(false);

			modelBuilder.Entity<SecurityRolePoco>()
				.HasMany(e => e.SecurityLoginsRole)
				.WithRequired(e => e.SecurityRole)
				.HasForeignKey(e => e.Role)
				.WillCascadeOnDelete(false);
			

			modelBuilder.Entity<SystemCountryCodePoco>()
				.HasMany(e => e.ApplicantWorkHistory)
				.WithRequired(e => e.SystemCountryCode)
				.HasForeignKey(e => e.CountryCode)
				.WillCascadeOnDelete(false);

			modelBuilder.Entity<SystemCountryCodePoco>()
				.HasMany(e => e.ApplicantProfile)
				.WithOptional(e => e.SystemCountryCode)
				.HasForeignKey(e => e.Country);

			modelBuilder.Entity<SystemLanguageCodePoco>()
				.HasMany(e => e.CompanyDescription)
				.WithRequired(e => e.SystemLanguageCode)
				.WillCascadeOnDelete(false);

			
			base.OnModelCreating(modelBuilder);
		}

		public DbSet<ApplicantEducationPoco> ApplicantEducation { get; set; }
		public DbSet<ApplicantJobApplicationPoco> ApplicantJobApplication { get; set; }
		public DbSet<ApplicantProfilePoco> ApplicantProfile { get; set; }
		public DbSet<ApplicantResumePoco> ApplicantResume { get; set; }
		public DbSet<ApplicantSkillPoco> ApplicantSkill { get; set; }
		public DbSet<ApplicantWorkHistoryPoco> ApplicantWorkHistory { get; set; }
		public DbSet<CompanyDescriptionPoco> CompanyDescripition { get; set; }
		public DbSet<CompanyJobDescriptionPoco> CompanyJobDescription { get; set; }
		public DbSet<CompanyJobEducationPoco> CompanyJobEducation { get; set; }
		public DbSet<CompanyJobPoco> CompnayJob { get; set; }
		public DbSet<CompanyJobSkillPoco> CompanyJobSkill { get; set; }
		public DbSet<CompanyLocationPoco> CompanyLocation { get; set; }
		public DbSet<CompanyProfilePoco> CompanyProfile { get; set; }
		public DbSet<SecurityLoginPoco> SecurityLogin { get; set; }
		public DbSet<SecurityLoginsLogPoco> SecurityLoginLog { get; set; }
		public DbSet<SecurityLoginsRolePoco> SecurityLoginRole { get; set; }
		public DbSet<SecurityRolePoco> SecurityRole { get; set; }
		public DbSet<SystemCountryCodePoco> SysstemCountryCode { get; set; }
		public DbSet<SystemLanguageCodePoco> SystemLanguageCode { get; set; }
	
	
	}
	
	}
	
	

