using System;
using System.Collections.Generic;
//**Inventář - id, nazev, počet, cena/ks
    //**Zbraně - dmg, typ, potřebná síla/obratnost
    //**Jídlo - lečeni životy, léčení mana - !!!!!!!!!!!!!!!!!!!!! Udělat Listový metody ve třídě Food
//Na výpis udělat celou stránku, kde by se pohybovalo šipkama, pak třeba možnost, něco vyhodit, něco přibrat atd.. 
//Další možní potomci - Cenosti, Magie, Šperky, Zbroje.
namespace Inventar_Dedicnost
{
    internal class Program
    {
        public static List<Weapon> weaponList = new List<Weapon>();
        public static List<Food> foodList = new List<Food>();
        public static Dictionary<int, Action> pageOfInventory = new Dictionary<int, Action>();            //Vytvoření dictionary - jedna proměná int - druhá proměnná je v podstatě zástupce pro různé metody
        public static Dictionary<int, int> test = new Dictionary<int, int>();                             //Na asociaci onPage s daným listem - nelíbilo se mi všude muset odvozovat List, podle onPage
        public static int onPage = 1;
        public static int highlighted = 0;
        static void Main(string[] args)
        {
            weaponList.AddRange(new Weapon("initialize", 1, 1, 0, "close", 0).StartWeapons());
            foodList.AddRange(new Food("starting food", 1, 1, 0, 0, 0).StartFood());
            pageOfInventory.Add(1, InventoryWeapons);
            pageOfInventory.Add(2, InventoryFood);
            test.Add(1, weaponList.Count-1);
            test.Add(2, foodList.Count - 1);
            Console.CursorVisible = false;
            pageOfInventory[onPage]();
            while (true)
            {
                switch (Console.ReadKey(true).Key)                                              //Parametr - bool zakázat konzoli vypsávat zmáčknuté charaktery
                {
                    case ConsoleKey.RightArrow:
                        if (onPage < pageOfInventory.Count)
                        {
                            onPage++;
                            pageOfInventory[onPage]();
                            break;
                        }
                        onPage = 1;
                        pageOfInventory[onPage]();
                        break;
                    case ConsoleKey.LeftArrow:
                        if (onPage > 1)
                        {
                            onPage--;
                            pageOfInventory[onPage]();
                            break;
                        }
                        onPage = pageOfInventory.Count;
                        pageOfInventory[onPage]();
                        break;
                    case ConsoleKey.DownArrow:
                        if (highlighted < test[onPage]) 
                            highlighted++; 
                        else 
                            highlighted = 0;
                        pageOfInventory[onPage]();
                        break;
                    case ConsoleKey.UpArrow:
                        if (highlighted > 0)
                            highlighted--;
                        else
                            highlighted = test[onPage];
                        pageOfInventory[onPage]();
                        break;
                }
            }

        }

        public static void InventoryWeapons()
        {
            new Weapon("Interface", 1, 1, 0, "close", 0).WriteInterface();
            weaponList[highlighted].Highlight = true;
            weaponList.ForEach(w => w.WriteItem());
            weaponList[highlighted].Highlight = false;
        }

        public static void InventoryFood()
        {
            new Food("Interface", 1, 1, 0, 0, 0).WriteInterface();
            foodList[highlighted].Highlight = true;
            foodList.ForEach(wp => wp.WriteItem());
            foodList[highlighted].Highlight = false;
        }
    }
}
