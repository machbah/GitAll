using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FactoryPattern
{
    public class Factory
    {
       
        public static Ingrediants GetProduct(string choice)
        {
            Ingrediants createdProduct = null;

            if(choice.Equals("1"))
            {
                createdProduct = new ProductA();              
            }
            else if(choice.Equals("2"))
            {
                createdProduct = new ProductB();
            }
            return createdProduct;
        }
    }
}