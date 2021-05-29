using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CareerCloud.Pocos;
using CareerCloud.DataAccessLayer;

namespace CareerCloud.BusinessLogicLayer
{
    public class ApplicantEducationLogic : BaseLogic<ApplicantEducationPoco>
    {
        public ApplicantEducationLogic(IDataRepository<ApplicantEducationPoco> repository) : base(repository)
        {
        }
        public override void Add(ApplicantEducationPoco[] pocos)
        {
            Verify(pocos);
            base.Add(pocos);
        }
        public override void Update(ApplicantEducationPoco[] pocos)
        {
            Verify(pocos);
            base.Update(pocos);
        }
        protected override void Verify(ApplicantEducationPoco[] pocos)
        {
            List<ValidationException> exceptions = new List<ValidationException>();
            foreach(var poco in pocos)
            {
                if(string.IsNullOrEmpty(poco.Major))
                {
                    exceptions.Add(new ValidationException(107, $"Major for Applicant Education {poco.Id} is required"));
                }
                else if(poco.Major.Length<3)
                {
                    exceptions.Add(new ValidationException(107, $"Major for Applicant Education {poco.Id} must be greater than 3 char"));
                }
                if(poco.StartDate>DateTime.Now)
                {
                    exceptions.Add(new ValidationException(108, $"Start Date {poco.Id} can not be  greater than today "));
                }
                if(poco.CompletionDate<poco.StartDate)
                {
                    exceptions.Add(new ValidationException(109, $"Completion Date {poco.Id} can not be earlier than Start Date"));
                }

                if (exceptions.Count > 0)
                {
                    throw new AggregateException(exceptions);
                }

            }
          
        }
    }
}
