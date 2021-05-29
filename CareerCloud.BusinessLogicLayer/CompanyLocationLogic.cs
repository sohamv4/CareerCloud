using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CareerCloud.Pocos;
using CareerCloud.DataAccessLayer;
namespace CareerCloud.BusinessLogicLayer
{
    public class CompanyLocationLogic : BaseLogic<CompanyLocationPoco>
    {
        public CompanyLocationLogic(IDataRepository<CompanyLocationPoco> repository) : base(repository)
        {
        }
        public override void Add(CompanyLocationPoco[] pocos)
        {
            Verify(pocos);
            base.Add(pocos);	
        }
        public override void Update(CompanyLocationPoco[] pocos)
        {
            Verify(pocos);
            base.Update(pocos);
        }
        protected override void Verify(CompanyLocationPoco[] pocos)
        {
            List<ValidationException> exception = new List<ValidationException>();
            foreach(var poco in pocos)
            {
                if(string.IsNullOrEmpty(poco.CountryCode))
                {
                    exception.Add(new ValidationException(500, $"Country Code {poco.Id} can not be null"));
                }
                if(string.IsNullOrEmpty(poco.Province))
                {
                    exception.Add(new ValidationException(501, $"Province {poco.Id} can not be null"));
                }
                if(string.IsNullOrEmpty(poco.Street))
                {
                    exception.Add(new ValidationException(502, $"Street {poco.Id} can not be null"));
                }
                if (string.IsNullOrEmpty(poco.City))
                {
                    exception.Add(new ValidationException(503, $"City {poco.Id} can not be null"));
                }
                if (string.IsNullOrEmpty(poco.PostalCode))
                {
                    exception.Add(new ValidationException(504, $"Postal Code {poco.Id} can not be null"));
                }

				if (exception.Count > 0)
				{
					throw new AggregateException(exception);
				}

			}
        }
    }
}
