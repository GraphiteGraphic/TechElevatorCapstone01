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
            Console.WriteLine($"\n Current Money Provided: {Program.vendingMachine.Balance:C}");
        }

        private MenuOptionResult FeedMoney()
        {
            decimal money = GetInteger("Please insert bills: ", 0);
            while (money < 0)
            {
                Console.WriteLine("Error: Negative currency does not exist");
                money = GetInteger("Please insert bills: ", 0);
            }

            Program.vendingMachine.Accounting(money);

            return MenuOptionResult.DoNotWaitAfterMenuSelection;
        }
        private MenuOptionResult Select()
        {


            return MenuOptionResult.WaitAfterMenuSelection;
        }
        private MenuOptionResult Finish()
        {
            
            return MenuOptionResult.CloseMenuAfterSelection;
        }
    }
}
