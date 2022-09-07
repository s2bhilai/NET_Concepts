using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactoryPattern.SimpleFactory
{
    public class NavigationBar
    {
        public NavigationBar() => ButtonFactory.CreateButton();
    }
}
