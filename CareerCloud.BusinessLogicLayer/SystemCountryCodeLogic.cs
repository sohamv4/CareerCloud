using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CareerCloud.Pocos;
using CareerCloud.ADODataAccessLayer;
using CareerCloud.DataAccessLayer;

namespace CareerCloud.BusinessLogicLayer
{
    public class SystemCountryCodeLogic :SystemCountryCodePoco
    {
        public SystemCountryCodeLogic(IDataRepository<SystemCountryCodePoco> repository)
        {
        }
        public  void Add(SystemCountryCodePoco[] pocos)
        {
            Verify(pocos);
          
        }
        public  void Update(SystemCountryCodePoco[] pocos)
        {
            Verify(pocos);
            
        }
        protected  void Verify(SystemCountryCodePoco[] pocos)
        {
            List<ValidationException> exception = new List<ValidationException>();
            foreach(var poco in pocos)
            {
                if (string.IsNullOrEmpty(poco.Code))
                {
                    exception.Add(new ValidationException(900, $"code for System country code {poco.Code} cannot be null"));
                }
                if (string.IsNullOrEmpty(poco.Name))
                {
                    exception.Add(new ValidationException(901, $"code for System country code{poco.Name} cannot be null"));
                }
                if (exception.Count > 0)
                {
                    throw new AggregateException(exception);
                }

            }
        }
    }
}
