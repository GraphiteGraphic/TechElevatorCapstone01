using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using Capstone.Models;

namespace CapstoneTests
{
    [TestClass]
    public class ItemTests
    {
        [TestMethod]
        public void Check_If_Item_Has_Correct_Properties()
        {
            //arrange
            Item item = new Item("A1", "Snackwells", .50M, "Candy");

            //act

            //assert
            Assert.AreEqual("A1", item.SlotLocation);
            Assert.AreEqual("Snackwells", item.Name);
            Assert.AreEqual(.50M, item.Price);
            Assert.AreEqual("Candy", item.Type);
            Assert.AreEqual(5, item.Quantity);
        }

        [TestMethod]
        public void Make_Sure_Vending_Machine_Is_Properly_Stocked()
        {
            //arrange

            //act
            VendingMachine.Load();

            //assert
            foreach(Item item in VendingMachine.Inventory)
            {
                Assert.AreEqual(5, item.Quantity);
            }
        }
    }
}
