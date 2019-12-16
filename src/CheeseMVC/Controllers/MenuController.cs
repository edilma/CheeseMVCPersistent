using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CheeseMVC.Data;
using CheeseMVC.Models;
using CheeseMVC.ViewModels;
using Microsoft.AspNetCore.Mvc;

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
            IList<CheeseMenu> myMenus = context.CheeseMenu.ToList();
            return View(myMenus);
        }

        public IActionResult Add()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Add(AddMenuViewModel addMenuViewModel)
        {
            if (ModelState.IsValid)
            {
                Menu newMenu = new Menu
                    ()
                    {
                    Name = addMenuViewModel.Name
               };
            context.Menu.Add(newMenu);
            context.SaveChanges();
                return Redirect("/Menu/ViewMenu" + newMenu.ID);
            }
            return View();
        }

        public IActionResult ViewMenu(int id)
        {
            Menu newMenu = context.Menu.Single(m => m.ID == id);
            List<CheeseMenu> items = context
                .CheeseMenus
                .Include(item =>item.Cheese)
                .Where(cm => cm.MenuID == id)
                .ToList();
            ViewMenuViewModel viewMenuViewModel = new ViewMenuViewModel()
            {
                Menu = newMenu,
                Items = items
            };

            return View(viewMenuViewModel);
        }
    }
}