﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Capstone.Models
{
    public class VendingMachine
    {
        private List<Item> inventory = new List<Item> { };
        public Item[] Inventory { get { return inventory.ToArray(); } }
        
        private decimal balance = 0;
        public decimal Balance { get { return balance; } }

        public void Dispense()
        {

        }

        public string inPath = "..\\..\\..\\..\\vendingmachine.csv";
        public void Load()
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

        public bool Accounting(decimal money)
        {
            if (balance + money > 0)
            {
                balance += money;
                return true;
            }

            return false;
        }

        public int[] Change()
        {
            decimal[] change = new decimal[] { 0.25M, 0.10M, 0.05M, 0.01M };
            int[] amountsOfEachCoin = new int[] { 0, 0, 0, 0 };

            for (int i = 0; i < change.Length - 1; i++)
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
