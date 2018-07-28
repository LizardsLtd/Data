using System;

namespace Lizzards.Data.Types
{
    public sealed class ContactDetails : IEquatable<ContactDetails>
    {
        public ContactDetails(string name, string contactInfo)
        {
            this.Name = name;
            this.ContactInfo = contactInfo;
        }

        public string ContactInfo { get; }

        public string Name { get; }

        public override bool Equals(object obj)
            => Equals(obj as ContactDetails);

        public bool Equals(ContactDetails other)
            => this.GetHashCode() == other?.GetHashCode();

        public override int GetHashCode()
            => $"{Name} {ContactInfo}".GetHashCode();
    }
}