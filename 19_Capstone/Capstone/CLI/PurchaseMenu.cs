using Capstone.Models;
using MenuFramework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Capstone.CLI
{
    public class PurchaseMenu : ConsoleMenu
    {
        public PurchaseMenu()
        {
            AddOption("Insert Money", FeedMoney);
            AddOption("Select Item", Select);
            AddOption("Finish Transaction", Finish);

            Configure(cfg =>
            {
                cfg.ItemForegroundColor = ConsoleColor.Cyan;
                cfg.Title = "Purchase Menu";
            });
        }

        protected override void OnAfterShow()
        {
            Console.WriteLine($"\n Current Money Provided: {VendingMachine.Balance:C}");
        }

        private MenuOptionResult FeedMoney()
        {
            decimal money = GetInteger("Please insert bills: ", 0);
            while (money < 0)
            {
                Console.WriteLine("Error: Negative currency does not exist");
                money = GetInteger("Please insert bills: ", 0);
            }

            VendingMachine.Accounting(money);

            return MenuOptionResult.DoNotWaitAfterMenuSelection;
        }
        private MenuOptionResult Select()
        {
            int spacing = 0;
            foreach (Item item in VendingMachine.Inventory)
            {
                if (spacing < item.Name.Length)
                {
                    spacing = 10 + item.Name.Length;
                }
            }

            foreach (Item item in VendingMachine.Inventory)
            { 
                string space = "";
                for (int i = 0; i < (spacing - item.Name.Length); i++)
                {
                    space += " ";
                }
                Console.WriteLine($"{item.SlotLocation}:\t{item.Name}{space}${item.Price}"); 
            }
            
            string selection = GetString("\nWhat would you like to purchase? (Please input product code)");
           
            foreach(Item item in VendingMachine.Inventory)
            {
                if (selection == item.SlotLocation && item.Quantity > 0)
                {
                    if (VendingMachine.Dispense(item))
                    {
                        Console.WriteLine(item.SoundEffect);
                        return MenuOptionResult.WaitAfterMenuSelection;
                    }
                    else
                    {
                        Console.WriteLine("Insufficient funds");
                        return MenuOptionResult.WaitAfterMenuSelection;
                    }
                }
                else if (item.Quantity == 0)
                {
                    Console.WriteLine("SOLD OUT!");
                    return MenuOptionResult.WaitAfterMenuSelection;
                }
            }

            Console.WriteLine("Error: Invalid Product Code");
            return MenuOptionResult.WaitAfterMenuSelection;
        }
        private MenuOptionResult Finish()
        {
            int[] result = VendingMachine.Change();
            Console.WriteLine($"Quarters: {result[0]}");
            Console.WriteLine($"Dimes: {result[1]}");
            Console.WriteLine($"Nickels: {result[2]}");
            Console.WriteLine($"Pennies: {result[3]}");

            //Console.WriteLine("balance = " + Program.vendingMachine.Balance); //for todd's testing purposes

            return MenuOptionResult.CloseMenuAfterSelection;
        }
    }
}
