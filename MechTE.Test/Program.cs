using System;
using System.Collections.Generic;
using System.Linq;
using MechTE_ContextMenu;

namespace MechTE.Test
{
    internal class Program
    {

        static void Main(string[] args)
        {
            ContextMenu menu = new ContextMenu();
            string rootPath = menu.getRootPath();
            Console.WriteLine(rootPath+"2");
            List<string> paths = menu.SelectedItemPaths.ToList();
            paths.Add(".exe");
            foreach (var r in paths)
            {
                Console.WriteLine(r);
            }
           
            Console.ReadKey();
        }
    }
}