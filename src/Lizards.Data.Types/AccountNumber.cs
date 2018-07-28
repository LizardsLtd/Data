namespace Lizzards.Data.Types
{
    public sealed class AccountNumber
    {
        public AccountNumber(string accountNumber)
        {
            this.Number = accountNumber;
        }

        public string Number { get; }

        public static implicit operator AccountNumber(string accountNumber)
            => new AccountNumber(accountNumber);

        public override string ToString() => this.Number;
    }
}