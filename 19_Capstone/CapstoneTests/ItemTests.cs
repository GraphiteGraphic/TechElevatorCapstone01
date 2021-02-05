using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using Capstone.Models;

namespace CapstoneTests
{
    [TestClass]
    public class ItemTests
    { 
        [DataTestMethod]
        [DataRow("A1", "Snackwells", .50, "Candy")]
        [DataRow("D3", "Fritos", 1.20, "Chip")]
        [DataRow("B4", "Diet Water", 3.60, "Drink")]
        [DataRow("A3", "Twix", .80, "Candy")]
        public void Check_If_Item_Has_Correct_Properties(string slotLocation, string name, double price, string type)
        {
            //arrange
            Item item = new Item(slotLocation, name, (decimal)price, type);

            //act

            //assert
            Assert.AreEqual(slotLocation, item.SlotLocation);
            Assert.AreEqual(name, item.Name);
            Assert.AreEqual((decimal)price, item.Price);
            Assert.AreEqual(type, item.Type);
            Assert.AreEqual(5, item.Quantity);
        }
    }
}
