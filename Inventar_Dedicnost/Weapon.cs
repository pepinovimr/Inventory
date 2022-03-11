using System;
using System.Collections.Generic;

namespace Inventar_Dedicnost
{
    //Zbraně - dmg, typ, potřebná síla/obratnost
    internal class Weapon : Inventory
    {
        protected byte Dmg;
        protected string Type;
        protected int AttributeNeeded;
        public Weapon(string name, int count, int price, byte dmg, string type, int attributeNeeded) :
            base(name, count, price, false)
        {
            this.Dmg = dmg;
            this.Type = type;
            this.AttributeNeeded = attributeNeeded;

        }
        public override void WriteHeader()
        {
            PageOfInv = "Zbraně";
            base.WriteHeader();
            Console.SetCursorPosition(posX[2], Console.CursorTop);
            Console.Write("Typ");
            Console.SetCursorPosition(posX[3], Console.CursorTop);
            Console.Write("Poškození");
            Console.SetCursorPosition(posX[4], Console.CursorTop);
            Console.Write("Potřebný atribut");
        }
        public override void WriteItem()
        {
            base.WriteItem();
            Console.SetCursorPosition(posX[2], Console.CursorTop);
            if (Type == "close")
                Console.Write("Zbraň na blízko");
            else if (Type == "range")
                Console.Write("Zbraň na dálku");
            Console.SetCursorPosition(posX[3], Console.CursorTop);
            Console.Write(Dmg);
            Console.SetCursorPosition(posX[4], Console.CursorTop);
            Console.Write(AttributeNeeded);
            if (Type == "close")
                Console.Write(" síla");
            else if (Type == "range")
                Console.Write(" obratnost");
        }
        public List<Weapon> AllWeapon()
        {
            List<Weapon> allWp = new List<Weapon>
            {
                new Weapon("Vycházková hůl", 1, 50, 10, "close", 10),
                new Weapon("Široký meč", 1, 150, 25, "close", 20),
                new Weapon("Zdobený meč", 1, 500, 35, "close", 20),
                new Weapon("Dlouhý meč", 1, 300, 40, "close", 35),
                new Weapon("Pirátská šavle", 1, 500, 55, "close", 45),
                new Weapon("Meč padlého hrdiny", 1, 1000, 80, "close", 65),
                new Weapon("Malý Luk", 1, 45, 15, "range", 10),
                new Weapon("Lovecký Luk", 1, 90, 30, "range", 20),
                new Weapon("Kuše", 1, 400, 40, "range", 25),
                new Weapon("Bukový Luk", 1, 185, 45, "range", 35),
                new Weapon("Dlouhý Luk", 1, 350, 65, "range", 45),
                new Weapon("Luk padlého hrdiny", 1, 800, 90, "range", 65)
            };
            return allWp;
        }

        public Weapon RandomWeapon()
        {
            return AllWeapon()[rnd.Next(AllWeapon().Count)];
        }

        public override List<Inventory> StartingItems()
        {
            List<Inventory> start = new List<Inventory>();
            if (rnd.Next(10) == 5)
                start.Add(AllWeapon()[1]);
            else
                start.Add(AllWeapon()[0]);

            if (rnd.Next(10) == 3)
                start.Add(AllWeapon()[7]);
            else
                start.Add(AllWeapon()[6]);

            return start;
        }
    }
}
