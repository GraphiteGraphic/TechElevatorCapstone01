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
            VendingMachine.Accounting((decimal)startingBalance);
            Item item = new Item("A1", "TestChocolate", (decimal)costOfItem, "Candy");

            //act
            //VendingMachine.Load();
            VendingMachine.Dispense(item);

            //assert
            Assert.AreEqual((decimal)finalBalance, VendingMachine.Balance);
            VendingMachine.Change();

        }

        [DataTestMethod]
        [DataRow(3, 2)]
        [DataRow(1, 4)]
        [DataRow(0, 5)]
        [DataRow(5, 0)]
        [DataRow(7, -2)]
        public void Check_If_Dispense_Correctly_Updates_Quantity(int numberOfPurchases, int expectedQuantityLeft)
        {
            Item item = new Item("A1", "TestChocolate", .75M, "Candy");

            VendingMachine.Accounting(10000M);
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
    }
}
