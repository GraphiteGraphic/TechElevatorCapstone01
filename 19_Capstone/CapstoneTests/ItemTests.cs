using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using Capstone.Models;

namespace CapstoneTests
{
    [TestClass]
    public class ItemTests
    { 
        [DataTestMethod]
        [DataRow("A1", "Snackwells", .50, "Candy", "Munch Munch, Yum!")]
        [DataRow("D3", "Fritos", 1.20, "Chip", "Crunch Crunch, Yum!")]
        [DataRow("B4", "Diet Water", 3.60, "Drink", "Glug Glug, Yum!")]
        [DataRow("A3", "Twix", .80, "Candy", "Munch Munch, Yum!")]
        [DataRow("D4", "ExciteMint", .20, "Gum", "Chew Chew, Yum!")]
        public void Check_If_Item_Has_Correct_Properties(string slotLocation, string name, double price, string type, string soundEffect)
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
            Assert.AreEqual(soundEffect, item.SoundEffect);
        }
    }
}
