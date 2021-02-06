using Microsoft.VisualStudio.TestTools.UnitTesting;
using Capstone.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace CapstoneTests
{

    [TestClass]
    public class VendingMachineTests
    {
        [TestMethod]
        public void Make_Sure_Vending_Machine_Is_Properly_Stocked()
        {
            //arrange

            //act
            VendingMachine.Load();

            //assert
            foreach (Item item in VendingMachine.Inventory)
            {
                Assert.AreEqual(5, item.Quantity);
            }

        }

        [DataTestMethod]
        [DataRow(5, .75, 4.25)]
        [DataRow(100, 3, 97)]
        [DataRow(1, .01, .99)]
        public void Check_If_Dispense_Correctly_Updates_Balance(double startingBalance, double costOfItem, double finalBalance)
        {
            //arrange
            VendingMachine.Load();
            VendingMachine.Accounting((decimal)startingBalance);
            Item item = new Item("A1", "Potato Crisps", (decimal)costOfItem, "Chip");

            //act
            //VendingMachine.Load();
            VendingMachine.Dispense(item);

            //assert
            Assert.AreEqual((decimal)finalBalance, VendingMachine.Balance);
            VendingMachine.Change();

        }

        [DataTestMethod]
        [DataRow(1000, 3, 2)]
        [DataRow(1000, 1, 4)]
        [DataRow(1000, 0, 5)]
        [DataRow(1000, 5, 0)]
        [DataRow(1000, 7, -2)]
        [DataRow(0, 7, 5)]
        public void Check_If_Dispense_Correctly_Updates_Quantity(int money, int numberOfPurchases, int expectedQuantityLeft)
        {
            VendingMachine.Load();
            Item item = new Item("A1", "Potato Crisps", .75M, "Chip");
            VendingMachine.Accounting(money);

            for (int i = 0; i < numberOfPurchases; i++)
            {
                VendingMachine.Dispense(item);
            }

            Assert.AreEqual(expectedQuantityLeft, item.Quantity);
            VendingMachine.Change();
        }

        [DataTestMethod]
        [DataRow(.99, new int[] { 3, 2, 0, 4 })]
        [DataRow(1, new int[] { 4, 0, 0, 0 })]
        [DataRow(5.01, new int[] { 20, 0, 0, 1 })]
        [DataRow(.41, new int[] { 1, 1, 1, 1 })]
        [DataRow(0, new int[] { 0, 0, 0, 0 })]
        [DataRow(.71, new int[] { 2, 2, 0, 1 })]
        [DataRow(.30, new int[] { 1, 0, 1, 0 })]
        public void Check_If_Machine_Returns_Correct_Change(double balance, int[] expectedResult)
        {
            //arrange
            VendingMachine.Accounting((decimal)balance);


            //act
            int[] actualResult = VendingMachine.Change();

            //assert
            CollectionAssert.AreEquivalent(expectedResult, actualResult);
        }

        [DataTestMethod]
        [DataRow ("A1", "Insufficient Funds", 0, 1)]
        [DataRow ("Foo", "Error: Invalid Product Code", 0, 1)]
        [DataRow ("A1", "Crunch Crunch, Yum!", 10, 1)]
        [DataRow ("A1", "SOLD OUT", 20, 6)]
        public void Check_Inventory_Tests(string selection, string expectedmessage, double money, int numberOfTransactions)
        {
            //arrange
            VendingMachine.Load();
            VendingMachine.Accounting((decimal)money);
            string output = "";

            //act
            for (int i = 0; i < numberOfTransactions; i++)
            {
                output = VendingMachine.CheckInventory(selection);
            }

            //assert
            Assert.AreEqual(expectedmessage, output);

            //reset Class
            VendingMachine.Accounting(VendingMachine.Balance * -1);
        }
    }
}
