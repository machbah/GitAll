using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FactoryPattern
{
    public class ProductA:Ingrediants
    {
       public string Connection(string s1, string s2)
       {
           string output = "from ProductA  " + s1 + " and " + s2;
           return output;
       }
    }

    public class ProductB : Ingrediants
    {
        public string Connection(string s1, string s2)
        {
            string output = "from ProductB  " + s1 + " and " + s2;
            return output;
        }
    }
}