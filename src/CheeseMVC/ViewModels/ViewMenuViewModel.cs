using CheeseMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CheeseMVC.ViewModels
{
    public class ViewMenuViewModel
    {
        public Menu Menu { get; set; }
        public IList<CheeseMenu> Items { get; set; }
        
        public ViewMenuViewModel (List<CheeseMenu> items)
        {
            foreach (CheeseMenu item in items)
            {
                Items.Add(new item
                {
                    Menu = item.MenuID.ToString(),
                    Cheese = item.CheeseID.ToString()
                });
            }
        }
    }
}
