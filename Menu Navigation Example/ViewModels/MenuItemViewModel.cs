using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Menu_Navigation_Example.ViewModels
{
    public class MenuItemViewModel
    {
        public string Header { get; set; }

        public List<MenuItemViewModel> SubMenuItems { get; set; }
    }
}
