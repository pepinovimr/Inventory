using System;
using System.Collections.Generic;
//Jídlo - lečeni životy, léčení mana, Léčení stamina
namespace Inventar_Dedicnost
{
    internal class Food : Inventory
    {
        public int AddHP { get; set; }
        public int AddMP { get; set; }
        public int AddST { get; set; }
        public Food(string name, int count, int price, int addHP, int addMP, int addST) :
            base(name, count, price, false, false)
        {
            AddHP = addHP;
            AddST = addST;
            AddMP = addMP;
        }

        //Tuty metody příště asi dělat přes Interface
        public override void WriteHeader()
        {
            PageOfInv = "Jídlo";
            base.WriteHeader();
            Console.SetCursorPosition(posX[2], Console.CursorTop);
            Console.Write("Lečení života");
            Console.SetCursorPosition(posX[3], Console.CursorTop);
            Console.Write("Léčení výdrže");
            Console.SetCursorPosition(posX[4], Console.CursorTop);
            Console.Write("Léčení many");
        }

        public override void WriteItem()
        {
            base.WriteItem();
            Console.SetCursorPosition(posX[2], Console.CursorTop);
            Console.Write(AddHP);
            Console.SetCursorPosition(posX[3], Console.CursorTop);
            Console.Write(AddST);
            Console.SetCursorPosition(posX[4], Console.CursorTop);
            Console.Write(AddMP);
        }

        public List<Food> AllItems()
        {
            List<Food> allFd = new List<Food>
            {
            new Food("Borůvky", 1, 1, 10, 0, 20),
            new Food("Voda", 1, 5, 15, 5, 20),
            new Food("Med", 1, 25, 5, 20, 15),
            new Food("Léčivá rostlina", 1, 15, 25, 0, 10),
            new Food("Léčivá bylina", 1, 30, 50, 0, 10),
            new Food("Léčivý kořen", 1, 50, 75, 0, 10),
            new Food("Ohnivá kopřivka", 1, 20, 5, 25, 10),
            new Food("Ohnivé býlí", 1, 40, 5, 50, 10),
            new Food("Ohnivý kořen", 1, 80, 5, 75, 10),
            new Food("Polévka z divočáka", 1, 50, 65, 0, 60),
            new Food("Rum", 1, 20, -10, 25, 50),
            new Food("Lěčivý lektvar", 1, 100, 100, 0, 0),
            new Food("Lektvar many", 1, 150, 0, 100, 0),
            };
            return allFd;
        }

        public Food RandomItem()
        {
            return AllItems()[rnd.Next(AllItems().Count)];
        }
        public List<Inventory> StartingItems()
        {
            List<Inventory> start = new List<Inventory>();
            if (rnd.Next(10) == 5)
                start.Add(AllItems()[1]);
            else
                start.Add(AllItems()[0]);

            if (rnd.Next(10) == 3)
                start.Add(AllItems()[6]);
            else
                start.Add(AllItems()[2]);

            return start;
        }
    }
}
