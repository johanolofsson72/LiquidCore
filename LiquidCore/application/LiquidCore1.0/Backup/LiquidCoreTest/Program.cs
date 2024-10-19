using System;
using System.Collections.Generic;
using System.Text;

namespace Arne
{
    class Program
    {

        static void Main(string[] args)
        {

             
            
            // Uppdatera Site
            LiquidCore.Site site = new LiquidCore.Site();
            site.Title = "TestSite1";
            site.Save();


            //// Hämta Site
            //LiquidCore.Site oSite = new LiquidCore.Site(1008);
            //Console.WriteLine(oSite.Id);
            //Console.WriteLine(oSite.Name);
            //Console.WriteLine(oSite.UpdatedDate);
            //Console.WriteLine(oSite.CreatedDate);
            //oSite.Dispose();


            //Console.WriteLine("create sites");
            //Console.ReadKey();
            //Console.WriteLine("create sites start");
            //for (Int32 i = 0; i < 100; i++)
            //{
            //    LiquidCore.Site s = new LiquidCore.Site();
            //    s.Name = DateTime.Now.Ticks.ToString();
            //    s.Save();
            //}
            //Console.WriteLine("create sites stop");

            Console.WriteLine("");
            Console.WriteLine("");

            Console.WriteLine("hämtar");
            LiquidCore.Sites ss = new LiquidCore.Sites();
            
            Console.WriteLine("visar");
            foreach (LiquidCore.Site s in ss)
                Console.WriteLine(s.Title);


            //ss[0].Name = "kvar";
            //ss.Save();
            
            ////ss.Add(new Site(2)); 
            //ss.Delete();


            Console.ReadKey();
        }
    }
}

