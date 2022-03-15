using System;
using System.Collections.Generic;
using System.Threading;
//**Inventář - id, nazev, počet, cena/ks
    //**Zbraně - dmg, typ, potřebná síla/obratnost
    //**Jídlo - lečeni životy, léčení mana
//Na výpis udělat celou stránku, kde by se pohybovalo šipkama, pak třeba možnost, něco vyhodit, něco přibrat atd.. 
//Další možní potomci - Cenosti, Magie, Šperky, Zbroje.

//Celkově jsem po napsání většiny programu zjistil, že je dost špatně navržený, spousta věcí by mělo mít vlastní classy, pro jednotlivé druhy předmětů příště udělat interface atd...
//Všechno to vychází z toho, že jsem si přidával věci co udělat během psaní kódu.
//Už to nestíhám předělat, tak aby se mi to líbilo.
//Každopádně na ukázku dědičnosti (a toho jak nemá být navržený program) to asi bohatě stačí.
namespace Inventar_Dedicnost
{
    internal class Program
    {
        public static List<Inventory> weaponList = new List<Inventory>();
        public static List<Inventory> foodList = new List<Inventory>();
        public static Dictionary<int, List<Inventory>> pageOfInventory = new Dictionary<int, List<Inventory>>()         //Slovník s číslem stránky a Kolekcí pro danou stránku
        {
            { 0, weaponList },
            { 1, foodList }
        };
        public static PlayerStats player = new PlayerStats();
        public static int onPage = 0;               //Ukazuje na jaké stránce inventáře jsme
        public static int highlightedIndex = 0;     //Ukazuje index označeného itemu
        static void Main(string[] args)
        {
            weaponList.AddRange(new Weapon("starting weapons", 1, 1, 0, "close", 0).StartingItems());                         //Přidá do kolekce startovací věci
            foodList.AddRange(new Food("starting food", 1, 1, 0, 0, 0).StartingItems());

            Console.CursorVisible = false;
            WriteInventory(pageOfInventory[onPage]);

            while (true)                                                                        //'menu' pro funkcionalitu
            {
                switch (Console.ReadKey(true).Key)                                              //Parametr - bool zakáže konzoli vypsávat zmáčknuté charaktery
                {
                    case ConsoleKey.RightArrow:
                        if (onPage < pageOfInventory.Count-1)
                        {
                            onPage++;
                            highlightedIndex = 0;
                            WriteInventory(pageOfInventory[onPage]);
                            break;
                        }
                        onPage = 0;
                        highlightedIndex = 0;
                        WriteInventory(pageOfInventory[onPage]);
                        break;

                    case ConsoleKey.LeftArrow:
                        if (onPage > 0)
                        {
                            onPage--;
                            highlightedIndex = 0;
                            WriteInventory(pageOfInventory[onPage]);
                            break;
                        }
                        onPage = pageOfInventory.Count-1;
                        highlightedIndex = 0; 
                        WriteInventory(pageOfInventory[onPage]);
                        break;

                    case ConsoleKey.DownArrow:
                        if (pageOfInventory[onPage].Count == 1)
                            break;
                        if (highlightedIndex < pageOfInventory[onPage].Count - 1) 
                            highlightedIndex++; 
                        else 
                            highlightedIndex = 0;
                        WriteInventory(pageOfInventory[onPage]);
                        break;

                    case ConsoleKey.UpArrow:
                        if (pageOfInventory[onPage].Count == 1)
                            break;
                        if (highlightedIndex > 0)
                            highlightedIndex--;
                        else
                            highlightedIndex = pageOfInventory[onPage].Count - 1 ;
                        WriteInventory(pageOfInventory[onPage]);
                        break;
                    case ConsoleKey.X:
                        if (pageOfInventory[onPage].Count == 0)
                            break;
                        RemoveItem(pageOfInventory[onPage]);
                        WriteInventory(pageOfInventory[onPage]);
                        break;
                    case ConsoleKey.E:
                        AddRandomItem();
                        WriteInventory(pageOfInventory[onPage]);
                        break;
                    case ConsoleKey.Q:
                        Use();
                        WriteInventory(pageOfInventory[onPage]);
                        break;
                }
            }

        }

        public static void WriteInventory(List<Inventory> ListInv)                              //Vypisuje vše na obrazovce
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

        public static void RemoveItem(List<Inventory> rmItem)                                   //Odstraňuje označený item - příště by mohlo být ve vlastní třídě
        {

            if(pageOfInventory[onPage][highlightedIndex].Quantity == 1)                         //Pokud je quantity u označeného itemu jedna, rovnou jej odstraní
                pageOfInventory[onPage].RemoveAt(highlightedIndex);
            else
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write("Kolik kusů chcete vyhodit? :");
                Console.CursorVisible = true;
                string sn = Console.ReadLine();                                                     //Pro možnost vyhození určitého počtu itemů
                Console.CursorVisible = false;
                if (int.TryParse(sn, out int n))                                                    //Ošetření vyjimek - nezadnáno číslo
                {
                    if (n < 1)                                                                      //Ošetření vyjimek - číslo není kladné
                    {
                        ErrorDialog("Číslo nesmí být menší než 1");
                        return;
                    }
                    if(pageOfInventory[onPage][highlightedIndex].Quantity == n)  //vyhodí item pryč, pokud je zadané číslo stejné, jako quantity
                    {
                        pageOfInventory[onPage].RemoveAt(highlightedIndex);
                        return;
                    }
                    if(pageOfInventory[onPage][highlightedIndex].Quantity <= n)
                    {
                        pageOfInventory[onPage].RemoveAt(highlightedIndex);
                        highlightedIndex--;
                        return;
                    }

                    pageOfInventory[onPage][highlightedIndex].Quantity = pageOfInventory[onPage][highlightedIndex].Quantity - n;            //pouze sníží quantity o daný počet
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
                if(pageOfInventory[onPage].Count == 0)          //Ošetření, aby se program nesnanžil vypisovat Itemy, když žádné v kolekci nejsou
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
        public static void Use()
        {
            if (pageOfInventory[onPage].Count <= 0)
                return;
            switch (onPage)
            {
                case 0:
                    pageOfInventory[onPage].ForEach(x => { if (x.Equiped == true) x.Equiped = false;});                //Zjišťuje jestli je v Listu už jeden Equipnutý Item, pokud ano, tak ho odznačí, aby nemohli být equipnuty 2 a více
                    pageOfInventory[onPage][highlightedIndex].Equiped = true;
                    Weapon w = (Weapon) pageOfInventory[onPage][highlightedIndex];
                    PlayerStats.PlayerDmg = w.Dmg;
                    break;
                case 1:
                    Food f = (Food) pageOfInventory[onPage][highlightedIndex];
                    PlayerStats.EatFood(f);
                    pageOfInventory[onPage][highlightedIndex].Quantity--;
                    if (pageOfInventory[onPage][highlightedIndex].Quantity <= 0)
                    {
                        pageOfInventory[onPage].RemoveAt(highlightedIndex);
                        highlightedIndex--;
                    }
                    break;
            }
        }

        public static void ErrorDialog(string s)                 //Metoda, která vypisuje chybovou hlášku
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(s);
            Console.ForegroundColor = ConsoleColor.White;
            Thread.Sleep(1000);
            WriteInventory(pageOfInventory[onPage]);
        }
        public static void WriteHeaderOnPage()                    //Vypíše Header podle dané stránky - příště by asi měla být jínde
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

        public static void AddRandomItem()                          //Přidá náhodný Item do kolekce, podle dané stránky - asi špatně navržený, příště by mělo být jinde 
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

        public static void FindAndAddElement(Inventory i)         //Pokusí se najít element v kolekci, pokuď tam není - přidá ho, pokuď tam je, zvětší Quantity o 1
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
