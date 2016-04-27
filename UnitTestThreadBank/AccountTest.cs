using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ThreadBank;

namespace UnitTestThreadBank
{
    [TestClass]
    public class AccountTest
    {
        [TestMethod]
        public void TestItAll()
        {
            IAccount account = new AccountOrdinary("My Account", 100);
            int balance = account.Withdraw(10);
            Assert.AreEqual(90, balance);
            balance = account.Deposit(20);
            Assert.AreEqual(110, balance);

            Assert.AreEqual("Account id: My Account, balance: 110", account.ToString());

            try
            {
                account.Deposit(-1);
                Assert.Fail();
            }
            catch (ArgumentOutOfRangeException) { }
            try
            {
                account.Withdraw(-1);
                Assert.Fail();
            }
            catch (ArgumentOutOfRangeException ex)
            {
                Assert.AreEqual("amount", ex.ParamName);
                Assert.AreEqual(-1, ex.ActualValue); 
            }
        }
    }
}
