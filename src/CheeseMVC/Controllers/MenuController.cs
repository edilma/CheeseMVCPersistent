using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CheeseMVC.Data;
using CheeseMVC.Models;
using CheeseMVC.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace CheeseMVC.Controllers
{
    public class MenuController : Controller
    {
        private CheeseDbContext context;

        public MenuController(CheeseDbContext dbContext)
        {
            context = dbContext;
        }
        public IActionResult Index()
        {
            IList<CheeseMenu> allMenus = context.CheeseMenus.ToList();
            return View(allMenus);
        }

        public IActionResult Add()
        {
            AddMenuViewModel addMenuViewModel = new AddMenuViewModel();
            return View(addMenuViewModel);
        }
        [HttpPost]
        public IActionResult Add(AddMenuViewModel addMenuViewModel)
        {
            if (ModelState.IsValid)
            {
                Menu newMenu = new Menu()
                    {
                    Name = addMenuViewModel.Name
                    };
                context.Menus.Add(newMenu);
                context.SaveChanges();
                return Redirect("/Menu/ViewMenu/" + newMenu.ID);
            }
            return View();
        }

        public IActionResult ViewMenu(int id)
        {
            Menu menuToDisplay = context.Menus.Single(m => m.ID == id);
            List<CheeseMenu> items = context
                .CheeseMenus
                .Include(item =>item.Cheese)
                .Where(cm => cm.MenuID == id)
                .ToList();
            ViewMenuViewModel viewMenuViewModel = new ViewMenuViewModel(items)
            {
                Menu = menuToDisplay,
                Items = items
            };

            return View(viewMenuViewModel);
        }

        public IActionResult AddItem(int id)
        {
            Menu getMenu = context.Menus.Single(cm => cm.ID == id);
            List<SelectListItem> theCheeses = context.Cheeses
                .Select(x=> 
                new SelectListItem() 
                    { 
                    Value = x.ID.ToString(),
                    Text = x.Name
                    })
                .ToList();

            AddMenuItemViewModel addMenuItemViewModel = new AddMenuItemViewModel()
            {
                Menu = getMenu,
                Cheeses = theCheeses

            };
            return View(addMenuItemViewModel); 
        }

        [HttpPost]
        public IActionResult AddItem(AddMenuItemViewModel addMenuItemViewModel)
        {
            
            if (ModelState.IsValid)
            {
                int cheeseID = addMenuItemViewModel.cheeseID;
                int menuID = addMenuItemViewModel.MenuID;


                IList<CheeseMenu> existingItems = context.CheeseMenus
                    .Where(cm => cm.CheeseID == cheeseID)
                    .Where(cm => cm.MenuID == menuID).ToList();
                if (existingItems.Count==0 )
                {
                    CheeseMenu addedMenuItem = new CheeseMenu()
                    {
                        MenuID = menuID,
                        CheeseID = cheeseID

                    };
                    context.CheeseMenus.Add(addedMenuItem);
                    context.SaveChanges();
                    
                }
                return Redirect("/Menu/ViewMenu/" + menuID);


            }
            return View();
        }
    }
}