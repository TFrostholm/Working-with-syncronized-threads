namespace ThreadBank
{
    public interface IAccount
    {
        string Id { get; }

        int Deposit(int amount);
        int Withdraw(int amount);

        int Transfer(int amount, IAccount toAccount);
        string ToString();
    }

}
