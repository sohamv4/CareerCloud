using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CareerCloud.Pocos;
using CareerCloud.DataAccessLayer;
namespace CareerCloud.BusinessLogicLayer
{
    public class SecurityRoleLogic : BaseLogic<SecurityRolePoco>
    {
        public SecurityRoleLogic(IDataRepository<SecurityRolePoco> repository) : base(repository)
        {
        }
        public override void Add(SecurityRolePoco[] pocos)
        {
            Verify(pocos);
            base.Add(pocos);
        }
        public override void Update(SecurityRolePoco[] pocos)
        {
            Verify(pocos);
            base.Update(pocos);
        }
        protected override void Verify(SecurityRolePoco[] pocos)
        {
            List<ValidationException> exception = new List<ValidationException>();
            foreach (var poco in pocos) 
            {

                if (string.IsNullOrEmpty(poco.Role))
                {
                    exception.Add(new ValidationException(800, $"Role for Security role {poco.Id} cannot be null"));
                }
                if (exception.Count > 0)
                {
                    throw new AggregateException(exception);
                }
            }
        }
    }
}
