﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebRole1.interfaces
{
    interface ITrie
    {
        void AddTitle(string title);
        List<string> SearchForPrefix(string title);
    }
}
