﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactoryPattern.AbstractFactory
{
    public class NavigationBar
    {
        public NavigationBar(IUIFactory factory) => factory.CreateButton(); 
    }
}
