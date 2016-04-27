using System;
using System.Threading;

namespace ThreadBank
{

    public class AccountOrdinary : IAccount
    {
        private int _balance;
        public string Id { get; private set; }

        private readonly Object _lockObject = new Object();


        public AccountOrdinary(String id, int balance)
        {
            _balance = balance;
            Id = id;
        }

        public int Deposit(int amount)
        {
            if (amount < 0)
            {
                throw new ArgumentOutOfRangeException("amount", amount, "amount must be non-negative");
            }
            lock (_lockObject)
            {
                _balance = _balance + amount;
                return _balance;
            }
        }

        // 
        public int Withdraw(int amount)
        {
            if (amount < 0)
            {
                throw new ArgumentOutOfRangeException("amount", amount, "amount must be non-negative");
            }
            lock (_lockObject)
            {
                // _balance = _balance - amount;
                int bal = _balance;
                int newBalance = bal - amount;
                Thread.Sleep(1000);
                _balance = newBalance;
                return _balance;
            }
        }

        public int Transfer(int amount, IAccount toAccount)
        {
            if (amount < 0)
            {
                throw new ArgumentOutOfRangeException("amount", amount, "amount must be non-negative");
            }
            lock (this)
            {
                Console.WriteLine("Account " + this + " locked");
                Thread.Sleep(100);
                lock (toAccount)
                {
                    _balance = _balance - amount;
                    toAccount.Deposit(amount);
                    return _balance;
                }
            }
        }

        public override string ToString()
        {
            return string.Format("Account id: {0}, balance: {1}", Id, _balance);
        }
    }
}
