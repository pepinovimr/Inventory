using System;
using System.Collections.Generic;
using System.Threading;
//**Inventář - id, nazev, počet, cena/ks
    //**Zbraně - dmg, typ, potřebná síla/obratnost
    //**Jídlo - lečeni životy, léčení mana
//Na výpis udělat celou stránku, kde by se pohybovalo šipkama, pak třeba možnost, něco vyhodit, něco přibrat atd.. 
//Další možní potomci - Cenosti, Magie, Šperky, Zbroje.
namespace Inventar_Dedicnost
{
    internal class Program
    {
        public static List<Inventory> weaponList = new List<Inventory>();
        public static List<Inventory> foodList = new List<Inventory>();
        public static Dictionary<int, List<Inventory>> pageOfInventory = new Dictionary<int, List<Inventory>>()
        {
            { 0, weaponList },
            { 1, foodList }
        };
        public static int onPage = 0;
        public static int highlightedIndex = 0;
        static void Main(string[] args)
        {
            weaponList.AddRange(new Weapon("initialize", 1, 1, 0, "close", 0).StartingItems());
            foodList.AddRange(new Food("starting food", 1, 1, 0, 0, 0).StartingItems());

            Console.CursorVisible = false;
            Initialize(pageOfInventory[onPage]);

            while (true)
            {
                switch (Console.ReadKey(true).Key)                                              //Parametr - bool zakáže konzoli vypsávat zmáčknuté charaktery
                {
                    case ConsoleKey.RightArrow:
                        if (onPage < pageOfInventory.Count-1)
                        {
                            onPage++;
                            highlightedIndex = 0;
                            Initialize(pageOfInventory[onPage]);
                            break;
                        }
                        onPage = 0;
                        highlightedIndex = 0;
                        Initialize(pageOfInventory[onPage]);
                        break;

                    case ConsoleKey.LeftArrow:
                        if (onPage > 0)
                        {
                            onPage--;
                            highlightedIndex = 0;
                            Initialize(pageOfInventory[onPage]);
                            break;
                        }
                        onPage = pageOfInventory.Count-1;
                        highlightedIndex = 0; 
                        Initialize(pageOfInventory[onPage]);
                        break;

                    case ConsoleKey.DownArrow:
                        if (pageOfInventory[onPage].Count == 1)
                            break;
                        if (highlightedIndex < pageOfInventory[onPage].Count - 1) 
                            highlightedIndex++; 
                        else 
                            highlightedIndex = 0;
                        Initialize(pageOfInventory[onPage]);
                        break;

                    case ConsoleKey.UpArrow:
                        if (pageOfInventory[onPage].Count == 1)
                            break;
                        if (highlightedIndex > 0)
                            highlightedIndex--;
                        else
                            highlightedIndex = pageOfInventory[onPage].Count - 1 ;
                        Initialize(pageOfInventory[onPage]);
                        break;
                    case ConsoleKey.X:
                        if (pageOfInventory[onPage].Count == 0)
                            break;
                        RemoveItem(pageOfInventory[onPage]);
                        Initialize(pageOfInventory[onPage]);
                        break;
                    case ConsoleKey.E:
                        AddRandomItem();
                        Initialize(pageOfInventory[onPage]);
                        break;
                }
            }

        }

        public static void Initialize(List<Inventory> ListInv)
        {
            if (pageOfInventory[onPage].Count == 0)
                {
                    WriteHeaderOnPage();
                    Inventory.WriteFooter();
                    return;
                }
            ListInv[highlightedIndex].WriteHeader();
            ListInv.Sort((x, y) => x.Price.CompareTo(y.Price));
            ListInv[highlightedIndex].Highlight = true;
            ListInv.ForEach(wp => wp.WriteItem());
            ListInv[highlightedIndex].Highlight = false;
            Inventory.WriteFooter();
        }

        public static void RemoveItem(List<Inventory> rmItem)
        {
            if(pageOfInventory[onPage][highlightedIndex].Quantity == 1)
                pageOfInventory[onPage].RemoveAt(highlightedIndex);
            else
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write("Kolik kusů chcete vyhodit? :");
                Console.CursorVisible = true;
                string sn = Console.ReadLine();
                Console.CursorVisible = false;
                if (int.TryParse(sn, out int n))
                {
                    if (n < 1)
                    {
                        ErrorDialog("Číslo nesmí být menší než 1");
                        return;
                    }
                    if(pageOfInventory[onPage][highlightedIndex].Quantity == n)
                    {
                        pageOfInventory[onPage].RemoveAt(highlightedIndex);
                        return;
                    }

                    pageOfInventory[onPage][highlightedIndex].Quantity = pageOfInventory[onPage][highlightedIndex].Quantity - n;
                }
                else
                {
                    ErrorDialog("Nebylo zadáno číslo!");
                    return;

                }
            }

            if (pageOfInventory[onPage].Count > 0)
                highlightedIndex = -1;
            else
            {
                if(pageOfInventory[onPage].Count == 0)
                {
                    WriteHeaderOnPage();
                    return;
                }

            }
            if (highlightedIndex < 0)
            {
                rmItem.ForEach(wp => wp.WriteItem());
                highlightedIndex = 0;
            }
            else
            {
                rmItem[highlightedIndex].Highlight = true;
                rmItem.ForEach(wp => wp.WriteItem());
                rmItem[highlightedIndex].Highlight = false;
            }
            Inventory.WriteFooter();

        }

        public static void ErrorDialog(string s)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(s);
            Console.ForegroundColor = ConsoleColor.White;
            Thread.Sleep(1000);
            Initialize(pageOfInventory[onPage]);
        }

        //Tuty switche s casema pro každýho potomka jsou dost pain
        //*Příště u takového drobet většího projektu využívat něco ve stylu Factory Pattern
        //A jinak pořešit ty Listy
        public static void WriteHeaderOnPage()
        {
            switch (onPage)
            {
                case 0:
                    new Weapon("Header", 0, 0, 0, "close", 0).WriteHeader();
                    break;
                case 1:
                    new Food("Header", 0, 0, 0, 0, 0).WriteHeader();
                    break;
            }
        }

        public static void AddRandomItem()
        {
            switch (onPage)
            {
                case 0:
                    Weapon w = new Weapon("Header", 0, 0, 0, "close", 0).RandomItem();
                    FindAndAddElement(w);
                    break;
                case 1:
                    Food f = (new Food("Header", 0, 0, 0, 0, 0).RandomItem());
                    FindAndAddElement(f);
                    break;
            }
        }

        public static void FindAndAddElement(Inventory i)
        {
            i.WriteItem();
            List<Inventory> l = pageOfInventory[onPage].FindAll(x => x.Name.Contains(i.Name));
            if (l.Count != 0)
            {
                i.Quantity = l[0].Quantity + 1;
                pageOfInventory[onPage].Remove(l[0]);
            }
            pageOfInventory[onPage].Add(i);
        }

    }
}
