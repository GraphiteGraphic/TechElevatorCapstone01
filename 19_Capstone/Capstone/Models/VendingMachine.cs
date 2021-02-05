using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Capstone.Models
{
    public static class VendingMachine
    {
        private static List<Item> inventory = new List<Item> { };
        public static Item[] Inventory { get { return inventory.ToArray(); } }
        
        private static decimal balance = 0;
        public static decimal Balance { get { return balance; } }

        public static bool Dispense(Item item)
        {
            item.Quantity -= 1;
            return Accounting(item.Price * -1);
        }

        public static string inPath = "..\\..\\..\\..\\vendingmachine.csv";
        public static void Load()
        {
            using (StreamReader reader = new StreamReader(inPath))
            {
                while (!reader.EndOfStream)
                {
                    string[] result = reader.ReadLine().Split("|");
                    inventory.Add(new Item(result[0], result[1], decimal.Parse(result[2]), result[3]));
                }
            }
        }

        public static bool Accounting(decimal money)
        {
            if (balance + money >= 0)
            {
                balance += money;
                return true;
            }

            return false;
        }

        public static int[] Change()
        {
            decimal[] change = new decimal[] { 0.25M, 0.10M, 0.05M, 0.01M };
            int[] amountsOfEachCoin = new int[] { 0, 0, 0, 0 };

            for (int i = 0; i < change.Length; i++)
            {
                while (balance >= change[i])
                {
                    balance -= change[i];
                    amountsOfEachCoin[i]++;
                }
            }
            return amountsOfEachCoin;
        }
    }
}
