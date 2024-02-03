using System;
using System.Collections.Specialized;
using System.Reflection;

namespace MechTE.Test
{
    internal class Program
    {
        static void Main(string[] args)
        {
           var myCol = new NameValueCollection();
           myCol.Add("red", "rojo");
           myCol.Add("green", "verde");
           myCol.Add("blue", "azul");
           myCol.Add("red", "rouge");
           Console.WriteLine(myCol["red"]);
           // rojo,rouge
            Console.ReadKey();
        }
    }

}