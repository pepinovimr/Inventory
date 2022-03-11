using System;
using System.Collections.Generic;
//Jídlo - lečeni životy, léčení mana
namespace Inventar_Dedicnost
{
    internal class Food:Inventory
    {
        public int AddHP { get; set; }
        public int AddMP { get; set; }
        public int AddST { get; set; }
        public Food(string name, int count, int price, int addHP, int addMP, int addST) :
            base(name, count, price, false)
        {
            this.AddHP = addHP;
            this.AddST = addST;
            this.AddMP = addMP;
        }

        public override void WriteInterface()
        {
            PageOfInv = "Jídlo";
            base.WriteInterface();
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
    }
}
