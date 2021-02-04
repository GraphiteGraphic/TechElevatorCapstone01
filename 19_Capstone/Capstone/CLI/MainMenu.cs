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
               //cfg.MenuSelectionMode = MenuSelectionMode.KeyString; // KeyString: User types a key, Arrow: User selects with arrow
               //cfg.KeyStringTextSeparator = ": ";
               cfg.Title = "Main Menu";
           });
        }
        private MenuOptionResult DisplayMenuItems()
        {
            foreach (Item item in Program.vendingMachine.Inventory)
            {
                if (item.Quantity == 0)
                {
                    Console.WriteLine($"{item.SlotLocation}: {item.Name}, SOLD OUT");
                }
                else
                {
                    Console.WriteLine($"{item.SlotLocation}: {item.Name}, ${item.Price}, ({item.Quantity} available) ");
                }
            }
            return MenuOptionResult.WaitAfterMenuSelection;
        }

        private MenuOptionResult Purchase()
        {
            purchaseMenu.Show();
            return MenuOptionResult.WaitAfterMenuSelection;
        }
    }
}
