using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace CareerCloud.Pocos
{
    [Table("Company_Jobs")]
    public class CompanyJobPoco : IPoco
      {
        [Key]
        public Guid Id { get; set; }
        public Guid Company { get; set; }

        [Column("Profile_Created")]
        public DateTime ProfileCreated { get; set; }

        [Column("Is_Inactive")]
        public Boolean IsInactive { get; set; }

        [Column("Is_Company_Hidden")]
        public Boolean IsCompanyHidden { get; set; }

        [Column("Time_Stamp")]
		[Timestamp]
		public byte[] TimeStamp { get; set; }
		public virtual ICollection<ApplicantJobApplicationPoco> ApplicationJobApplication { get; set; }
		public virtual ICollection<CompanyJobDescriptionPoco> CompanyJobDescription { get; set; }
		public virtual ICollection<CompanyJobEducationPoco> CompanyJobEducation { get; set; }
		public virtual ICollection<CompanyJobSkillPoco> CompanyJobSkill { get; set; }
		public virtual CompanyProfilePoco CompanyProfile { get; set; }

    }
}
