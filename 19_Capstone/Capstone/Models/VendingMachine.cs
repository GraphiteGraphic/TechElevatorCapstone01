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

        public static void Dispense(Item item)
        {
            if (Accounting(item.Price * -1))
            {
                item.Quantity -= 1;
            }
        }

        public static string inPath = "..\\..\\..\\..\\vendingmachine.csv";
        public static string outPath = "..\\..\\..\\..\\Log.txt";

        public static void Load()
        {
            inventory.Clear();
            using (StreamReader reader = new StreamReader(inPath))
            {
                while (!reader.EndOfStream)
                {
                    string[] result = reader.ReadLine().Split("|");
                    inventory.Add(new Item(result[0], result[1], decimal.Parse(result[2]), result[3]));
                }
            }
        }

        public static string CheckInventory(string selection)
        {
            foreach (Item item in Inventory)
            {
                if (selection != item.SlotLocation)
                {
                    continue;
                }

                if (item.Quantity > 0)
                {
                    if (balance > item.Price)
                    {
                        StringLog($"{item.Name} {item.SlotLocation}");
                        Dispense(item);
                        return item.SoundEffect;
                    }

                    return "Insufficient Funds";
                }

                return "SOLD OUT";
            }

            return "Error: Invalid Product Code";
        }

        public static bool Accounting(decimal money)
        {
            if (balance + money >= 0)
            {
                CashLog(balance);
                balance += money;
                CashLog(balance);
                return true;
            }

            return false;
        }

        public static int[] Change()
        {
            decimal[] change = new decimal[] { 0.25M, 0.10M, 0.05M, 0.01M };
            int[] amountsOfEachCoin = new int[] { 0, 0, 0, 0 };

            CashLog(balance);
            for (int i = 0; i < change.Length; i++)
            {
                while (balance >= change[i])
                {
                    balance -= change[i];
                    amountsOfEachCoin[i]++;
                }
            }
            CashLog(balance);

            return amountsOfEachCoin;
        }

        public static void StringLog(string action)
        {
            using (StreamWriter writer = new StreamWriter(outPath, true))
            {
                writer.Write($"{DateTime.Now} {action}");
            }
        }


        public static int indexer = 0;
        public static void CashLog(decimal balance)
        {
            using (StreamWriter writer = new StreamWriter(outPath, true))
            {
                indexer++;
                writer.Write($" {balance:C}");

                if (indexer > 1)
                {
                    writer.WriteLine();
                    indexer = 0;
                }
            }
        }

        /// <summary>
        /// Attempt at making a "Hidden" Menu Option
        /// </summary>
        public static bool Hidden = false;
    }
}
