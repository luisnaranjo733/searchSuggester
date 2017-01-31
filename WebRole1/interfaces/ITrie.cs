using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebRole1.interfaces
{
    interface ITrie
    {
        void AddTitle(string title);
        string[] SearchForPrefix(string query);
    }
}