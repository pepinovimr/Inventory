using System;

namespace Inventar_Dedicnost
{
    internal class PlayerStats
    {
        public static int Dex { get; set; }
        public static int Str { get; set; }
        public static int HP { get; set; }
        public static int MP { get; set; }
        public static int ST { get; set; }
        public static int PlayerDmg { get; set; }
        public static Random rnd = new Random();

        public PlayerStats()
        {
            Dex = 10;
            Str = 10;
            HP = 20;
            MP = 10;
            ST = 35;
            PlayerDmg = 0;
        }

        public static void EatFood(Food f)
        {
            HP += f.AddHP;
            MP += f.AddMP;
            ST += f.AddST;
            if (HP > 100)
                HP = 100;
            if (MP > 100)
                MP = 100;
            if (ST > 100)
                ST = 100;
        }

        public static void AddRandomAttributes()
        {
            Dex += rnd.Next(1, 6);
            Str += rnd.Next(1, 6);

            if (Dex > 100)
                Dex = 100;
            if (Str > 100)
                Str = 100;
        }
    }
}
