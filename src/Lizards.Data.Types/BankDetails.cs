namespace Lizzards.Data.Types
{
    public sealed class BankDetails
    {
        public BankDetails(string accountNumber)
            : this(new AccountNumber(accountNumber)) { }

        public BankDetails(AccountNumber accountNumber)
        {
            this.AccountNumber = accountNumber;
        }

        public BankDetails()
        {
        }

        public AccountNumber AccountNumber { get; }
    }
}