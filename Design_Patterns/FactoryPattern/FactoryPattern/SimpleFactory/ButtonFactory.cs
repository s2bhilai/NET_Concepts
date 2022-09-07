using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactoryPattern.SimpleFactory
{
    public class ButtonFactory
    {
        public static Button CreateButton()
        {
            return new Button { Type = "Red Button" };
        }
    }
}
