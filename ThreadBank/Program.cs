using System;
using System.Threading;
using System.Threading.Tasks;

namespace ThreadBank
{
    class Program
    {
        static void Main()
        {
            //RunOrdinary();
            //RunOrdinaryTask();
            //RunDeadlock();
            RunWaiting();
        }

        private static void RunOrdinary()
        {
            // The reason why we use IAccount interface is so we can easily change the type of account. We are using the interface type as a supertype
            IAccount acc = new AccountOrdinary("My Account", 100);
            Customer cust1 = new Customer(acc);
            Customer cust2 = new Customer(acc);
            Console.WriteLine(acc);
            Thread thread1 = new Thread(() => cust1.DoWithdraw(60));
            Thread thread2 = new Thread(() => cust2.DoWithdraw(50));
            thread1.Start();
            thread2.Start();
            thread1.Join();
            thread2.Join();
            Console.WriteLine(acc);
        }

        private static void RunOrdinaryTask()
        {
            IAccount acc = new AccountOrdinary("My account", 100);
            Customer cust1 = new Customer(acc);
            Customer cust2 = new Customer(acc);
            Console.WriteLine(acc);
            Task task1 = Task.Run(() => cust1.DoWithdraw(60));
            Task task2 = Task.Run(() => cust2.DoWithdraw(50));
            Task.WaitAll(task1, task2);
            Console.WriteLine(acc);
        }

        private static void RunDeadlock()
        {
            IAccount account1 = new AccountOrdinary("My account", 100);
            IAccount account2 = new AccountOrdinary("Your account", 40);
            Customer customer1 = new Customer(account1);
            Customer customer2 = new Customer(account2);

            Task task1 = Task.Run(() => customer1.DoTransfer(10, account2));
            Task task2 = Task.Run(() => customer2.DoTransfer(20, account1));
            Task.WaitAll(task1, task2);

            Console.WriteLine("Account1: " + account1);
            Console.WriteLine("Account2: " + account2);
        }

        private static void RunWaiting()
        {
            IAccount acc = new AccountWaiting("My account", 100);
            Customer cust1 = new Customer(acc);
            Customer cust2 = new Customer(acc);
            Console.WriteLine(acc);

            Task task1 = Task.Run(() => cust1.DoWithdraw(110));
            Task task2 = Task.Run(() => cust2.DoDeposit(15));
            Task.WaitAll(task1, task2);
            Console.WriteLine(acc);
        }
    }
}

