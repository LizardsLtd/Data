using System;
using System.Text.RegularExpressions;

namespace Lizzards.Data.Types
{
    public sealed class Email
    {
        private const string parsingExpression = @"^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$";

        public Email(string address)
        {
            this.Address = address;

            if (!this.IsValid())
            {
                throw new ArgumentException("Email is not valid");
            }
        }

        public string Address { get; }

        public static implicit operator Email(string emailAddres)
            => new Email(emailAddres);

        public static implicit operator string(Email email)
            => email?.Address;

        public override int GetHashCode() => this.Address.GetHashCode();

        public override string ToString() => this.Address;

        private bool IsValid() => Regex.IsMatch(this.Address, parsingExpression);
    }
}