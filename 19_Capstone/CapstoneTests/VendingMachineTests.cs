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
        [TestMethod]
        public void Check_If_Dispense_Correctly_Updates_Balance()
        {
            //arrange
            VendingMachine.Accounting(5M);
            Item item = new Item("A1", "TestChocolate", .75M, "Candy");

            //act
            VendingMachine.Load();
            VendingMachine.Dispense(item);

            //assert
            Assert.AreEqual(4.25M, VendingMachine.Balance);
            VendingMachine.Change();

        }
        [TestMethod]
        public void Check_If_Dispense_Correctly_Updates_Quantity()
        {
            Item item = new Item("A1", "TestChocolate", .75M, "Candy");

            VendingMachine.Dispense(item);

            Assert.AreEqual(4, item.Quantity);
        }

        [TestMethod]
        public void Check_If_Machine_Returns_Correct_Change()
        {
            //arrange
            VendingMachine.Accounting(.99M);
            int[] expectedResult = new int[] { 3, 2, 0, 4 };


            //act
            int[] actualResult = VendingMachine.Change();

            //assert
            CollectionAssert.AreEquivalent(expectedResult, actualResult);
        }
    }
}
