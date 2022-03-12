using System;
using System.Collections.Generic;
namespace Inventar_Dedicnost
{
    //Inventář - nazev, počet, cena/ks
    internal class Inventory
    {
        public string Name { set; get; }
        public int Quantity { set; get; }
        public int Price { set; get; }
        protected string PageOfInv { set; get; }
        public bool Highlight { get; set; } = false;
        protected int[] posX = { 20, 32, 44, 65, 81 };
        public static Random rnd = new Random();

        public virtual void WriteHeader()                                                       //Napíše hlavičku inventáře
        {
            Console.Clear();
            Console.BackgroundColor = ConsoleColor.White;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.WriteLine("                                                     INVENTÁŘ                                                          ");
            Console.ForegroundColor = ConsoleColor.White;
            Console.BackgroundColor = ConsoleColor.Black;
            Console.SetCursorPosition(Console.WindowWidth / 3, Console.CursorTop);
            Console.WriteLine(PageOfInv);
            Console.Write("Název");
            Console.SetCursorPosition(posX[0], Console.CursorTop);
            Console.Write("Počet");
            Console.SetCursorPosition(posX[1], Console.CursorTop);
            Console.Write("Cena");
        }

        public static void WriteFooter()                                                          //Napíše patičku inventáře
        {
            Console.SetCursorPosition(0, Console.CursorTop + 2);
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Použít - Q               Vyhodit - X             Najít náhodný předmět - E");
            Console.ForegroundColor = ConsoleColor.White;
        }

        public virtual void WriteItem()                                                            //Vypíše item na konzoli
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine();
            if (Highlight)
                Console.ForegroundColor = ConsoleColor.Green;
            Console.Write(Name);
            Console.SetCursorPosition(posX[0], Console.CursorTop);
            Console.Write(Quantity);
            Console.SetCursorPosition(posX[1], Console.CursorTop);
            Console.Write(Price);
        }

        public Inventory(string name, int count, int price, bool highlight)
        {
            this.Name = name;
            this.Quantity = count;
            this.Price = price;
            this.Highlight = highlight;
        }
    }
}
