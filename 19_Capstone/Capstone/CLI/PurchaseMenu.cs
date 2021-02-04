using MenuFramework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Capstone.CLI
{
    class PurchaseMenu : ConsoleMenu
    {
        public PurchaseMenu()
        {
            //feedmoney
            //select
            //finish
            AddOption("Insert Money", FeedMoney);
            AddOption("Select Item", Select);
            AddOption("Finish Transaction", Finish);

            Configure(cfg =>
            {
                cfg.ItemForegroundColor = ConsoleColor.Cyan;
                cfg.Title = "Purchase Menu";
            });
        }

        private MenuOptionResult FeedMoney()
        {
            return MenuOptionResult.WaitAfterMenuSelection;
        }
        private MenuOptionResult Select()
        {
            return MenuOptionResult.WaitAfterMenuSelection;
        }
        private MenuOptionResult Finish()
        {
            return MenuOptionResult.WaitAfterMenuSelection;
        }
    }
}
