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

            if (money > 0)
            {
                VendingMachine.StringLog("FEED MONEY:");
                VendingMachine.Accounting(money);
            }

            return MenuOptionResult.DoNotWaitAfterMenuSelection;
        }
        private MenuOptionResult Select()
        {
            //Visual Spacing
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

            //Secret Code to open hidden menu option
            if (selection == "%%")
            {
                HiddenOption();
            }

            Console.WriteLine(VendingMachine.CheckInventory(selection.ToUpper()));

            return MenuOptionResult.WaitAfterMenuSelection;
        }
        private MenuOptionResult Finish()
        {
            VendingMachine.StringLog("GIVE CHANGE:");
            int[] result = VendingMachine.Change();
            Console.WriteLine($"Quarters: {result[0]}");
            Console.WriteLine($"Dimes: {result[1]}");
            Console.WriteLine($"Nickels: {result[2]}");
            Console.WriteLine($"Pennies: {result[3]}");

            return MenuOptionResult.CloseMenuAfterSelection;
        }

        public void HiddenOption()
        {
            VendingMachine.Hidden = true;
        }

    }
}
