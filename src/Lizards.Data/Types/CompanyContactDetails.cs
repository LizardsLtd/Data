using System.Collections.Generic;

namespace Lizards.Data.Types
{
    public class CompanyContactDetails
    {
        private readonly List<ContactDetails> contactDetails;

        public CompanyContactDetails(string companyName, string firstName, string lastName)
            : this(
                companyName
                , firstName
                , lastName
                , new List<ContactDetails>())
        {
        }

        private CompanyContactDetails(
            string companyName
            , string firstName
            , string lastName
            , List<ContactDetails> contactDetails)
        {
            this.CompanyName = companyName;
            this.FirstName = firstName;
            this.LastName = lastName;
            this.contactDetails = contactDetails;
        }

        public string CompanyName { get; }

        public string FirstName { get; }

        public string LastName { get; }

        public IReadOnlyCollection<ContactDetails> ContactDetails => contactDetails.AsReadOnly();

        public CompanyContactDetails AddContactDetails(ContactDetails newContact)
        {
            var extendedContactDetails = new List<ContactDetails>(this.contactDetails);

            extendedContactDetails.Add(newContact);

            return new CompanyContactDetails(
                this.CompanyName
                , this.FirstName
                , this.LastName
                , extendedContactDetails);
        }
    }
}