using System.Collections.Generic;

namespace Lizzards.Data.Types
{
    public sealed class PersonalContactDetails
    {
        private readonly List<ContactDetails> contactDetails;

        public PersonalContactDetails(string firstName, string lastName)
            : this(
                  firstName
                  , lastName
                  , new List<ContactDetails>())
        {
        }

        private PersonalContactDetails(
            string firstName
            , string lastName
            , List<ContactDetails> contactDetails)
        {
            this.FirstName = firstName;
            this.LastName = lastName;
            this.contactDetails = contactDetails;
        }

        public string FirstName { get; }

        public string LastName { get; }

        public IReadOnlyCollection<ContactDetails> ContactDetails => contactDetails.AsReadOnly();

        public PersonalContactDetails AddContactDetails(ContactDetails newContact)
        {
            var extendedContactDetails = new List<ContactDetails>(this.contactDetails);

            extendedContactDetails.Add(newContact);

            return new PersonalContactDetails(
                  this.FirstName
                  , this.LastName
                  , extendedContactDetails);
        }
    }
}