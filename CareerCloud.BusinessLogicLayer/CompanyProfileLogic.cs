using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CareerCloud.Pocos;
using CareerCloud.DataAccessLayer;

namespace CareerCloud.BusinessLogicLayer
{
	public class CompanyProfileLogic : BaseLogic<CompanyProfilePoco>
	{
		public CompanyProfileLogic(IDataRepository<CompanyProfilePoco> repository) : base(repository)
		{
		}
		public override void Add(CompanyProfilePoco[] pocos)
		{
			Verify(pocos);
			base.Add(pocos);
		}
		public override void Update(CompanyProfilePoco[] pocos)
		{
			Verify(pocos);
			base.Update(pocos);
		}
		protected override void Verify(CompanyProfilePoco[] pocos)
		{
			List<ValidationException> exception = new List<ValidationException>();
			string[] requiredExtension = new string[] { ".ca", ".com", ".biz" };
			foreach (var poco in pocos)
			{
				if (!requiredExtension.Any(t => t == poco.CompanyWebsite.Substring(poco.CompanyWebsite.Length - t.Length)))
				{

					exception.Add(new ValidationException(600, $"Company Website {poco.Id} must be valid website end with the following extension-ca,.com,.biz"));

				}
				if (string.IsNullOrEmpty(poco.ContactPhone))
				{
					exception.Add(new ValidationException(601, $"{poco.ContactPhone} Must correspond to a valid phone number (e.g. 416-555-1234)"));
				}
				else
				{
					string[] phoneComponents = poco.ContactPhone.Split('-');
					if (phoneComponents.Length < 3)
					{
						exception.Add(new ValidationException(601, $"{poco.ContactPhone} Must correspond to a valid phone number (e.g. 416-555-1234)"));
					}
					else
					{
						if (phoneComponents[0].Length < 3)
						{
							exception.Add(new ValidationException(601, $"{poco.ContactPhone} Must correspond to a valid phone number (e.g. 416-555-1234)"));
						}
						if (phoneComponents[1].Length < 3)
						{
							exception.Add(new ValidationException(601, $"{poco.ContactPhone} Must correspond to a valid phone number (e.g. 416-555-1234)"));
						}
						if (phoneComponents[2].Length < 4)
						{
							exception.Add(new ValidationException(601, $"{poco.ContactPhone} Must correspond to a valid phone number (e.g. 416-555-1234)"));
						}
					}
				}


				if (exception.Count > 0)
				{
					throw new AggregateException(exception);
				}

			}
		}
	}
}

