using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactoryPattern.AbstractFactory
{
    public class Apple : IUIFactory
    {
        public Button CreateButton()
        {
            return new Button { Type = "IOS Button" };
        }
    }
}
