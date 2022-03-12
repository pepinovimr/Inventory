using System;
using System.Collections.Generic;
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
        public static int highlighted = 0;
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
                            highlighted = 0;
                            Initialize(pageOfInventory[onPage]);
                            break;
                        }
                        onPage = 0;
                        highlighted = 0;
                        Initialize(pageOfInventory[onPage]);
                        break;

                    case ConsoleKey.LeftArrow:
                        if (onPage > 0)
                        {
                            onPage--;
                            highlighted = 0;
                            Initialize(pageOfInventory[onPage]);
                            break;
                        }
                        onPage = pageOfInventory.Count-1;
                        highlighted = 0; 
                        Initialize(pageOfInventory[onPage]);
                        break;

                    case ConsoleKey.DownArrow:
                        if (pageOfInventory[onPage].Count == 1)
                            break;
                        if (highlighted < pageOfInventory[onPage].Count - 1) 
                            highlighted++; 
                        else 
                            highlighted = 0;
                        Initialize(pageOfInventory[onPage]);
                        break;

                    case ConsoleKey.UpArrow:
                        if (pageOfInventory[onPage].Count == 1)
                            break;
                        if (highlighted > 0)
                            highlighted--;
                        else
                            highlighted = pageOfInventory[onPage].Count - 1 ;
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
            ListInv[highlighted].WriteHeader();
            ListInv.Sort((x, y) => x.Price.CompareTo(y.Price));
            ListInv[highlighted].Highlight = true;
            ListInv.ForEach(wp => wp.WriteItem());
            ListInv[highlighted].Highlight = false;
            Inventory.WriteFooter();
        }

        public static void RemoveItem(List<Inventory> rmItem)
        {
            if(pageOfInventory[onPage][highlighted].Quantity == 1)
                pageOfInventory[onPage].RemoveAt(highlighted);
            else
            {
                pageOfInventory[onPage][highlighted].Quantity--;
            }
            if (pageOfInventory[onPage].Count > 0)
                highlighted = -1;
            else
            {
                highlighted = 0;
                if(pageOfInventory[onPage].Count == 0)
                {
                    WriteHeaderOnPage();
                    return;
                }

            }
            if (highlighted < 0)
            {
                rmItem.ForEach(wp => wp.WriteItem());
                highlighted = 0;
            }
            else
            {
                rmItem[highlighted].Highlight = true;
                rmItem.ForEach(wp => wp.WriteItem());
                rmItem[highlighted].Highlight = false;
            }
            Inventory.WriteFooter();

        }
        //Tuty switche mimo Main jsou dost pain
        //*Příště u drobet většího projektu využívat Factory Pattern
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
