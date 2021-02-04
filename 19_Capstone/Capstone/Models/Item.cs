using System;
using System.Collections.Generic;
using System.Text;

namespace Capstone.Models
{
    class Item
    {
        public string Name { get; }
        public string Type { get; }
        public string SlotLocation { get; }
        public int Quantity { get; set; }
        public decimal Price { get; }
        public string SoundEffect
        {
            get
            {
                return (Type.ToLower() == "chip") ? "Crunch Crunch, Yum!" :
                    (Type.ToLower() == "drink") ? "Glug Glug, Yum!" :
                    (Type.ToLower() == "gum") ? "Chew Chew, Yum!" :
                    "Munch Munch, Yum!" ;
            }
        }

        public Item(string name, string type, string slotlocation, decimal price)
        {
            this.Name = name;
            this.Type = type;
            this.SlotLocation = slotlocation;
            this.Price = price;
            Quantity = 5;
        }
    }
}
