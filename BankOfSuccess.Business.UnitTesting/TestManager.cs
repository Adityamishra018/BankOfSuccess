using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using BankOfSuccess.Business;
using BankOfSuccess.Data.Entities;
using System.Linq;

namespace BankOfSuccess.Business.UnitTesting
{
    [TestClass]
    public class TestManager
    {
        IAccountManager mgr = null;

        [TestInitialize]
        public void Init()
        {
            mgr = AccountManager.GetInstance;
        }
        [TestMethod]
        public void OpenSavingAccount_Success()
        {
            var sa = mgr.OpenSavingsAccount("Aditya", 1234, "M", "1133131", new DateTime(1999, 10, 19), 500);
            Assert.IsNotNull(sa);
            Assert.IsInstanceOfType(sa, typeof(SavingsAccount));
        }

        [TestMethod]
        public void OpenSavingAccount_Fail()
        {
            var sa = mgr.OpenSavingsAccount("Aditya", 1234, "M", "1133131", new DateTime(2010, 10, 19), 500);
            Assert.IsNull(sa);
        }
        [TestMethod]
        public void OpenCurrentAccount_Success()
        {
            var ca = mgr.OpenCurrentAccount("Aditya",1234,"cognizant","sasas","21DW");
            Assert.IsNotNull(ca);
            Assert.IsInstanceOfType(ca, typeof(CurrentAccount));
        }

        [TestMethod]
        public void OpenCurrentAccount_Fail()
        {
            var ca = mgr.OpenCurrentAccount("Aditya",1234,"sasa","asadad",null,500);
            Assert.IsNull(ca);
        }

        [TestMethod,ExpectedException(typeof(AccountException))]
        public void CloseAccount_Active_Fail()
        {
            var sa = mgr.OpenSavingsAccount("Aditya", 1234, "M", "1133131", new DateTime(1999, 10, 19), 500);
            sa.IsActive = false;
            mgr.CloseAccount(sa);
        }

        [TestMethod, ExpectedException(typeof(AccountException))]
        public void CloseAccount_Balance_Fail()
        {
            var sa = mgr.OpenSavingsAccount("Aditya", 1234, "M", "1133131", new DateTime(1999, 10, 19), 500);
            mgr.CloseAccount(sa);
        }

        [TestMethod]
        public void CloseAccount_Success()
        {
            var sa = mgr.OpenSavingsAccount("Aditya", 1234, "M", "1133131", new DateTime(1999, 10, 19), 0);
            mgr.CloseAccount(sa);
        }

        [TestMethod]
        public void Withdarw_Success()
        {
            var sa = mgr.OpenSavingsAccount("Aditya", 1234, "M", "1133131", new DateTime(1999, 10, 19),500);
            Assert.IsTrue(mgr.Withdraw(sa, 200, 1234));
        }

        [TestMethod,ExpectedException(typeof(TransactionFailedException))]
        public void Withdarw_Balance_Fail()
        {
            var sa = mgr.OpenSavingsAccount("Aditya", 1234, "M", "1133131", new DateTime(1999, 10, 19), 500);
            mgr.Withdraw(sa, 700, 1234);
        }

        [TestMethod, ExpectedException(typeof(TransactionFailedException))]
        public void Withdarw_Pin_Fail()
        {
            var sa = mgr.OpenSavingsAccount("Aditya", 1234, "M", "1133131", new DateTime(1999, 10, 19), 500);
            mgr.Withdraw(sa, 200, 1342);
        }

        [TestMethod, ExpectedException(typeof(AccountException))]
        public void Withdraw_Active_Fail()
        {
            var sa = mgr.OpenSavingsAccount("Aditya", 1234, "M", "1133131", new DateTime(1999, 10, 19), 500);
            mgr.CloseAccount(sa);
            mgr.Withdraw(sa, 200, 1234);
        }

        [TestMethod, ExpectedException(typeof(AccountException))]
        public void Deposit_Active_Fail()
        {
            var sa = mgr.OpenSavingsAccount("Aditya", 1234, "M", "1133131", new DateTime(1999, 10, 19), 500);
            mgr.CloseAccount(sa);
            mgr.Deposit(sa, 200);
        }

        [TestMethod]
        public void Deposit_Success()
        {
            var sa = mgr.OpenSavingsAccount("Aditya", 1234, "M", "1133131", new DateTime(1999, 10, 19), 500);
            Assert.IsTrue(mgr.Deposit(sa, 200));
        }

        [TestMethod]
        public void Transfer_Success()
        {
            var sa = mgr.OpenSavingsAccount("Aditya", 1234, "M", "1133131", new DateTime(1999, 10, 19), 500);
            var ca = mgr.OpenCurrentAccount("Aditya", 1234, "cognizant", "sasas", "21DW");
            Assert.IsTrue(mgr.Transfer(sa, ca, 300, 1234, TransferMode.NEFT));
            Assert.IsTrue(ca.Bal == 800);
        }

        [TestMethod,ExpectedException(typeof(AccountException))]
        public void Transfer_Balance_Fail()
        {
            var sa = mgr.OpenSavingsAccount("Aditya", 1234, "M", "1133131", new DateTime(1999, 10, 19), 500);
            var ca = mgr.OpenCurrentAccount("Aditya", 1234, "cognizant", "sasas", "21DW");
            mgr.Transfer(sa, ca, 800, 1234, TransferMode.NEFT);
        }

        [TestMethod, ExpectedException(typeof(TransactionFailedException))]
        public void Transfer_Pin_Fail()
        {
            var sa = mgr.OpenSavingsAccount("Aditya", 1234, "M", "1133131", new DateTime(1999, 10, 19), 500);
            var ca = mgr.OpenCurrentAccount("Aditya", 1234, "cognizant", "sasas", "21DW");
            mgr.Transfer(sa, ca, 300, 1242, TransferMode.IMPS);
        }

        [TestMethod, ExpectedException(typeof(AccountException))]
        public void Transfer_Active_Fail()
        {
            var sa = mgr.OpenSavingsAccount("Aditya", 1234, "M", "1133131", new DateTime(1999, 10, 19), 500);
            var ca = mgr.OpenCurrentAccount("Aditya", 1234, "cognizant", "sasas", "21DW");
            mgr.CloseAccount(sa);
            mgr.Transfer(sa, ca, 800, 1242, TransferMode.IMPS);
        }

        [TestMethod, ExpectedException(typeof(TransactionFailedException))]
        public void Transfer_Limit_Fail()
        {
            var sa = mgr.OpenSavingsAccount("Aditya", 1234, "M", "1133131", new DateTime(1999, 10, 19), 50000);
            var ca = mgr.OpenCurrentAccount("Aditya", 1234, "cognizant", "sasas", "21DW");
            mgr.Transfer(sa, ca, 20000, 1234, TransferMode.IMPS);
            mgr.Transfer(sa, ca, 5000, 1234, TransferMode.IMPS);
            mgr.Transfer(sa, ca, 1000, 1234, TransferMode.IMPS); //fails here
        }

        [TestCleanup]
        public void Cleanup()
        {
            mgr = null;
        }
    }

    [TestClass]
    public class TestLogger
    {
        ILogManager _logger = null;
        [TestInitialize]
        public void Init()
        {
            _logger = new LogManager();
        }
        [TestMethod]
        public void CreateLog_Success()
        {
            _logger.CreateLog(123);
            var li = _logger.GetLogs(123,TransactionType.TRANSFER);
            Assert.IsTrue(li.Count == 0);
        }

        [TestMethod]
        public void UpdateLog_Success()
        {
            _logger.CreateLog(123);
            _logger.UpdateLog(123,300,TransactionType.DEPOSIT);
            var log = _logger.GetLogs(123,TransactionType.DEPOSIT);
            Assert.IsTrue(300 == log.Sum(t => t.Amt));
        }

        [TestMethod]
        public void GetLog_Success()
        {
            _logger.CreateLog(123);
            var log = _logger.GetLogs(123,TransactionType.DEPOSIT);
            Assert.IsNotNull(log);
        }

        [TestCleanup]
        public void Cleanup()
        {
            _logger = null;
        }
    }
}
