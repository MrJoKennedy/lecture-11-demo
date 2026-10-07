using System;
namespace CMS.BusinessLayer
{
    public class Customer
    {
        public Customer()
        {

        }
        public Customer(int customerId)
        {
            this.CustomerId = customerId;
        }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string EmailAddress { get; set; }
        public Address HomeAddress { get; set; }
        public Address WorkAddress { get; set; }
        public int CustomerId { get; private set; }
        public string FullName
        {
            get
            {
                string name = LastName;
                if (!string.IsNullOrWhiteSpace(FirstName))
                {
                    if (!string.IsNullOrWhiteSpace(name))
                    {
                        name += ", ";
                    }
                    name += FirstName;
                }
                return name;
            }
        }

        public bool Validate()
        {
            var valid = true;

            if (string.IsNullOrWhiteSpace(LastName)) valid = false;
            if (string.IsNullOrWhiteSpace(EmailAddress)) valid = false;

            return valid;
        }
    }
}
