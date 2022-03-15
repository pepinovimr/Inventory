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
            HP = HP + f.AddHP;
            MP = MP + f.AddMP;
            ST = ST + f.AddST;
            if (HP > 100)
                HP = 100;
            if (MP > 100)
                MP = 100;
            if (ST > 100)
                ST = 100;
        }
    }
}
