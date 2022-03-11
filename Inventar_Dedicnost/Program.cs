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
            foodList.Add(new Food("Borůvky", 2, 1, 10, 0, 20));
            foodList.Add(new Food("Voda", 1, 5, 15, 5, 20));
            foodList.Add(new Food("Neco", 1, 5, 15, 5, 20));
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
                        //Console.WriteLine(test[onPage]);
                        //Console.ReadKey(true);
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

            /*new Food("Interface", 1, 1, 0, 0, 0).WriteInterface();
            foodList.Add(new Food("Borůvky", 15, 1, 10, 0, 20));
            foodList.Add(new Food("Voda", 10, 5, 15, 5, 20));
            foodList.Add(new Food("Med", 2, 25, 5, 20, 15));
            foodList.Add(new Food("Léčivá rostlina", 9, 15, 25, 0, 10));
            foodList.Add(new Food("Léčivá bylina", 4, 30, 50, 0, 10));
            foodList.Add(new Food("Léčivý kořen", 2, 50, 75, 0, 10));
            foodList.Add(new Food("Ohnivá kopřivka", 7, 20, 5, 25, 10));
            foodList.Add(new Food("Ohnivé býlí", 3, 40, 5, 50, 10));
            foodList.Add(new Food("Ohnivý kořen", 1, 80, 5, 75, 10));
            foodList.Add(new Food("Polévka z divočáka", 3, 50, 65, 0, 60));
            foodList.Add(new Food("Rum", 4, 20, -10, 25, 50));
            foodList.Add(new Food("Lěčivý lektvar", 4, 100, 100, 0, 0));
            foodList.Add(new Food("Lektvar many", 2, 150, 0, 100, 0));
            foodList.ForEach(wp => wp.WriteItem());*/
        }

        public static void InventoryWeapons()
        {
            new Weapon("initialize", 1, 1, 0, "close", 0).WriteInterface();
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
