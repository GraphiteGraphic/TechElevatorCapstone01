using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Capstone.Models
{
    public static class VendingMachine
    {
        private static Dictionary<string, int> salesLog = new Dictionary<string, int> { };
        private static List<Item> inventory = new List<Item> { };
        public static Item[] Inventory { get { return inventory.ToArray(); } }

        private static decimal balance = 0;
        public static decimal Balance { get { return balance; } }

        public static void Dispense(Item item)
        {
            if (Accounting(item.Price * -1))
            {
                item.Quantity -= 1;
                salesLog[item.Name]++;
            }
        }

        public static string invPath = "..\\..\\..\\..\\vendingmachine.csv";
        public static string auditPath = "..\\..\\..\\..\\Log.txt";
        public static string reportPath = "..\\..\\..\\..\\";

        public static void Load()
        {
            inventory.Clear();
            using (StreamReader reader = new StreamReader(invPath))
            {
                while (!reader.EndOfStream)
                {
                    string[] result = reader.ReadLine().Split("|");
                    inventory.Add(new Item(result[0], result[1], decimal.Parse(result[2]), result[3]));
                }
            }

            salesLog.Clear();
            string newReport = "";
            string[] files = Directory.GetFiles(reportPath);
            foreach (string file in files)
            {
                if (file.Contains("SalesReport"))
                {
                    newReport = file;
                }
            }
            if (File.Exists(newReport))
            {
                using (StreamReader reader = new StreamReader(reportPath))
                {
                    while (!reader.EndOfStream)
                    {
                        string foo = reader.ReadLine();
                        if (foo.Contains("|"))
                        {
                            string[] result = foo.Split("|");
                            salesLog.Add(result[0], int.Parse(result[1]));
                        }
                    }
                }
            }
            else
            {
                using (StreamReader reader = new StreamReader(invPath))
                {
                    while (!reader.EndOfStream)
                    {
                        string[] result = reader.ReadLine().Split("|");
                        salesLog.Add(result[1], 0);
                    }
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
                    if (balance >= item.Price)
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
            using (StreamWriter writer = new StreamWriter(auditPath, true))
            {
                writer.Write($"{DateTime.Now} {action}");
            }
        }


        public static int indexer = 0;
        public static void CashLog(decimal balance)
        {
            using (StreamWriter writer = new StreamWriter(auditPath, true))
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

        public static string dateTime;
        public static void SalesReport()
        {
            dateTime = $"{DateTime.Now}";
            dateTime = dateTime.Replace("/", "_").Replace(":",".").Replace(" ","_");
            using (StreamWriter writer = new StreamWriter($"{reportPath}SalesReport{dateTime}.txt"))
            {
                decimal total = 0;
                foreach (KeyValuePair<string,int> kvp in salesLog)
                {
                    foreach (Item item in inventory)
                    {
                        if (kvp.Key == item.Name)
                        {
                            total += kvp.Value * item.Price;
                        }
                    }
                    writer.WriteLine($"{kvp.Key}|{kvp.Value}");
                }

                writer.WriteLine($"\nTOTAL SALES: {total:C}");
            }
        }


        /// <summary>
        /// Attempt at making a "Hidden" Menu Option
        /// </summary>
        public static bool Hidden = false;
    }
}
