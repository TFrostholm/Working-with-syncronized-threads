using System;

namespace ThreadBank
{
    public class Customer
    {

        private readonly IAccount _account ;

        public Customer(IAccount account)
        {
            _account = account;
        }

        public void DoWithdraw(int amount)
        {
            _account.Withdraw(amount);
            Console.WriteLine("Customer withdrew {0}", amount);
        }

        public void DoDeposit(int amount)
        {
            _account.Deposit(amount);
            Console.WriteLine("Customer deposited {0}", amount);
        }

        public void DoTransfer(int amount, IAccount toAccount)
        {
            _account.Transfer(amount, toAccount);
            Console.WriteLine("Customer transferred {0} to {1}", amount, toAccount);
        }

        public override string ToString()
        {
            return string.Format("Customer balance: {0}", _account);
        }
    }
}
