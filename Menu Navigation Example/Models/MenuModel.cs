using Menu_Navigation_Example.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Menu_Navigation_Example.Models
{
    public class MenuModel
    {
        public List<MenuItemViewModel> MenuItems { get; set; } =
             new List<MenuItemViewModel>
             {
                new MenuItemViewModel { Header = "menu1" },
                new MenuItemViewModel { Header = "menu2",
                  SubMenuItems = new List<MenuItemViewModel>
                  {
                    new MenuItemViewModel { Header = "menu2_1" },
                    new MenuItemViewModel { Header = "menu2_2",
                      SubMenuItems = new List<MenuItemViewModel>
                      {
                        new MenuItemViewModel { Header = "menu2_2_1" },
                        new MenuItemViewModel { Header = "menu2_2_2" },
                        new MenuItemViewModel { Header = "menu2_2_3" }
                      }
                  },
                  new MenuItemViewModel { Header = "menu2_3" }
                }
              },
              new MenuItemViewModel { Header = "menu3" }
           };
    }
}
