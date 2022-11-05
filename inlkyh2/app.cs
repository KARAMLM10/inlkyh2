using inlkyh2.models;
using System;
using System.Collections.Generic;
using System.IO.Enumeration;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace inlkyh2
{
    public class app
    {
        

        public void Run()
        {
           
            var allprodukter = new List<kvitto>();
            //if (File.Exists("products.txt"))
            allprodukter = ReadproductsFromFile();
            //allprodukter = ReadPriseFromFile();
            while (true) 
            { 
              Console.WriteLine("1.Ny Kund");
              Console.WriteLine("0.Avsluta");
                //Console.WriteLine("pay");
                var num = Console.ReadLine();
                if(num == "0")
                    break;
                if (num == "1")
                {
                    ProduktRegistration(allprodukter);
                }
                //if (num == "pay") 
                //{
                //    Console.WriteLine($"kvitto" );



                //}
            }

        }

        private void ProduktRegistration(List<kvitto> allprodukter)
        {
            kvitto produkt1;
            while (true)
            {
                Console.WriteLine("kommandon");
                Console.WriteLine("produkt id"); // produkt id som man matar in frö exmp 300
                var ID1 = Console.ReadLine();
                produkt1 = FindproductsFromID(allprodukter, ID1);
                if (produkt1 == null)
                    Console.WriteLine("ogiltig ID");
                else break;
            }
            kvitto produkt2;
            while (true)
            {
                Console.WriteLine("produkt id"); // produkt id som man matar in frö exmp 300
                var ID2 = Console.ReadLine();
                produkt2 = FindproductsFromID(allprodukter, ID2);
                if (produkt2 == null)
                    Console.WriteLine("ogiltig ID");
                else break;
            }

            Console.WriteLine($"antal {produkt1.Name} ");
            var antal1 = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine($"antal {produkt2.Name}");
            var antal2 = Convert.ToInt32(Console.ReadLine());
            

            //var prise= Convert.ToDecimal(Console.ReadLine()); - tillföra affarsvärde - mogenhet-
            while (true)
            {
                Console.WriteLine("vill du forsätta handla ja/nej");
                string svar = Console.ReadLine();
                if (svar == "ja")
                {
                    allprodukter = ReadproductsFromFile();
                     ProduktRegistration(allprodukter);
                }
                else if (svar == "nej") break;
                
            }
            
            var filename = DateTime.Now.ToString("yyyy-MM-dd-hh-mm-ss") + ".txt";
            var kvi = ($"kvitto {DateTime.Now.ToString("yyyy-MM-dd-hh-mm-ss")}");

            var file = (produkt1.Name) + ".txt";
            
            var kvi2 = ($"kvitto {DateTime.Now.ToString("yyyy-MM-dd-hh-mm-ss")}");
            var line = $"{produkt1.Name }  {antal1} * {produkt1.Prise} = {produkt1.Prise * antal1} ";
            var line2 = $"{produkt2.Name}  {antal2} * {produkt2.Prise} = {produkt2.Prise * antal2}";
            var kvitto = ($"Total = {produkt1.Prise * antal1 + produkt2.Prise * antal2}");

            var str = DateTime.Now.ToString("yyy-MM-dd \n");

            var output = new StringBuilder(str.Length * antal1).Insert(0, str, antal1).ToString();

            var file2 = (produkt2.Name) + ".txt";
            var str2 = DateTime.Now.ToString("yyy-MM-dd \n");

            var output2 = new StringBuilder(str2.Length * antal2).Insert(0, str2, antal2).ToString();
            //File.AppendAllText(file2, output2);


            File.AppendAllText(filename, kvi + Environment.NewLine);
            File.AppendAllText(filename, line + Environment.NewLine);
            File.AppendAllText(filename, line2 + Environment.NewLine);
            File.AppendAllText(filename, kvitto + Environment.NewLine);
            //File.AppendAllText(filename,file + Environment.NewLine );
           // File.AppendAllText(file2, file2 + Environment.NewLine);
            File.AppendAllText(file, output + Environment.NewLine);
            File.AppendAllText(file2, output2 + Environment.NewLine);

        }

        private kvitto FindproductsFromID(List<kvitto> allprodukter, string? produkt1)
        {
            foreach(var produkter in allprodukter)
            {
                if (produkter.ID.ToString() == produkt1.ToLower()) return produkter;
            }

            return null; 
        }

        private List<kvitto> ReadproductsFromFile()
        {
            var result = new List<kvitto>();
            foreach (var line in File.ReadLines("products.txt"))
            {
                var parts=line.Split(';');
                //var produkt = new kvitto();
                //produkt.Name = parts[1];
                //produkt.ID = Convert.ToInt32(parts[0]);
                //produkt.Prise = Convert.ToDecimal(parts[3]);
                //produkt.Typ=parts[2];
                //result.Add(produkt);
                var produkt = new kvitto
                {
                    Name = parts[1],
                    ID = Convert.ToInt32( parts[0]),
                    Prise = Convert.ToDecimal(parts[3]),
                    Typ = parts[2],
                };
                result.Add(produkt);
            }
                return result;
        }
    }
}
