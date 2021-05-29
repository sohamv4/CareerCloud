﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CareerCloud.Pocos;
using CareerCloud.DataAccessLayer;

namespace CareerCloud.BusinessLogicLayer
{
    public class ApplicantSkillLogic : BaseLogic<ApplicantSkillPoco>
    {
        public ApplicantSkillLogic(IDataRepository<ApplicantSkillPoco> repository) : base(repository)
        {
        }
        public override void Add(ApplicantSkillPoco[] pocos)
        {
            Verify(pocos);
            base.Add(pocos);
        }
        public override void Update(ApplicantSkillPoco[] pocos)
        {
            Verify(pocos);
            base.Update(pocos);
        }
        protected override void Verify(ApplicantSkillPoco[] pocos)
        {
            List<ValidationException> exceptions = new List<ValidationException>();
            foreach(var poco in pocos)
            {
                
                if(poco.StartMonth>12)
                {
                    exceptions.Add(new ValidationException(101, $"Start Month {poco.Id} can not be greater than 12"));
                }
                if(poco.EndMonth>12)
                {
                    exceptions.Add(new ValidationException(102, $"End Month {poco.Id} can not be greater than 12"));
                }
                if(poco.StartYear<1900)
                {
                    exceptions.Add(new ValidationException(103, $"Start year {poco.Id} can not be less than 1900 "));
                }
                if(poco.EndYear<poco.StartYear)
                {
                    exceptions.Add(new ValidationException(104, $"End year {poco.Id} can not be less than start year"));
                        }

                if (exceptions.Count > 0)
                {
                    throw new AggregateException(exceptions);
                }
            }
        }
    }
}
