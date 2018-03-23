namespace Lizards.Data.Types
{
    public sealed class PersonalInformations
    {
        public PersonalInformations(string firstName, string lastName)
        {
            this.FirstName = firstName;
            this.LastName = lastName;
        }

        public string FirstName { get; set; }

        public string LastName { get; set; }
    }
}