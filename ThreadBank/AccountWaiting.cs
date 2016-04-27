using System;
using System.Threading;

namespace ThreadBank
{
    /// <summary>
    /// Simple bank account.
    /// Class invariant: balance >= 0.
    /// If you try to withdraw more than the balance you will have to wait for incoming money.
    /// </summary>
    public class AccountWaiting : IAccount
    {
        private int _balance;
        public string Id { get; private set; }
        private readonly Object _lockObject = new Object();


        public AccountWaiting(String id, int balance)
        {
            Id = id;
            _balance = balance;
        }



        public int Deposit(int amount)
        {
            if (amount < 0)
            {
                throw new ArgumentOutOfRangeException("amount", amount, "amount must be non-negative");
            }
            Monitor.Enter(_lockObject);
            try
            {
                _balance = _balance + amount;
                Thread.Sleep(1000);
                //
                Monitor.PulseAll(_lockObject);
                return _balance;
            }
            finally
            {
                Monitor.Exit(_lockObject);
            }
        }

        public int Withdraw(int amount)
        {
            if (amount < 0)
            {
                throw new ArgumentOutOfRangeException("amount", amount, "amount must be non-negative");
            }
            Monitor.Enter(_lockObject);
            try
            {
                while (_balance < amount)
                {
                    Console.WriteLine("waiting for money: has {0}, wants: {1}", _balance, amount);
                    // by using Monitor.Wait we are actually releasing the lock so another thread can use it
                    Monitor.Wait(_lockObject);
                }
                // _balance = _balance - amount;
                int bal = _balance;
                int newBalance = bal - amount;
                //Thread.Sleep(1000);
                _balance = newBalance;
                return _balance;
            }
            finally
            {
                Monitor.Exit(_lockObject);
            }
        }

        public int Transfer(int amount, IAccount toAccount)
        {
            throw new NotImplementedException();
        }

        public override string ToString()
        {
            return string.Format("Account id: {0}, balance: {1}", Id, _balance);
        }
    }
}
