﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace EmployeeManagement.Tests.TestData
{
    public class StronglyTypedEmployeeServiceTestData: TheoryData<int,bool>
    {
        public StronglyTypedEmployeeServiceTestData()
        {
            Add(100, true);
            Add(200, false);
        }
    }
}
