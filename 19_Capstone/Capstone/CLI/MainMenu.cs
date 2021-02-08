using MenuFramework;
using System;
using Capstone.Models;
using System.Collections.Generic;
using System.Text;

namespace Capstone.CLI
{
    public class MainMenu : ConsoleMenu
    {
        PurchaseMenu purchaseMenu = new PurchaseMenu();

        /*******************************************************************************
         * Private data:
         * Usually, a menu has to hold a reference to some type of "business objects",
         * on which all of the actions requested by the user are performed. A common 
         * technique would be to declare those private fields here, and then pass them
         * in through the constructor of the menu.
         * ****************************************************************************/

        // NOTE: This constructor could be changed to accept arguments needed by the menu
        public MainMenu()
        {
            // Add Sample menu options
            AddOption("Display Menu Items", DisplayMenuItems);
            AddOption("Purchase", Purchase);
            AddOption("Quit", Close);



            Configure(cfg =>
            {
                cfg.ItemForegroundColor = ConsoleColor.Cyan;
                cfg.Title = "Vendo-matic 800";
            });

        }

        protected override void RebuildMenuOptions()
        {
            if (VendingMachine.Hidden)
            {
                AddOption("**SALES REPORT**", SalesReport);

                Configure(cfg =>
                {
                    cfg.SelectedItemForegroundColor = ConsoleColor.Red;
                    cfg.ItemForegroundColor = ConsoleColor.Cyan;
                    cfg.Title = "SECRET MENU ACTIVATED";
                });
            }
        }

        private MenuOptionResult DisplayMenuItems()
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
                if (item.Quantity == 0)
                {
                    Console.WriteLine($"{item.SlotLocation}: {item.Name} {space}SOLD OUT");
                }
                else
                {
                    Console.WriteLine($"{item.SlotLocation}: {item.Name} {space}${item.Price} \t({item.Quantity} available) ");
                }
            }
            return MenuOptionResult.WaitAfterMenuSelection;
        }

        private MenuOptionResult Purchase()
        {
            purchaseMenu.Show();

            return MenuOptionResult.WaitAfterMenuSelection;
        }

        private MenuOptionResult SalesReport()
        {
            VendingMachine.SalesReport();
            Console.WriteLine($"SalesReport{VendingMachine.dateTime}.txt has been generated");
            return MenuOptionResult.WaitAfterMenuSelection;
        }

    }
}
