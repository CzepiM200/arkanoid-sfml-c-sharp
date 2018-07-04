using SFML.Graphics;

namespace ArkanoidCS
{
    static class DataLoader
    {
        public static Texture block1 { get; private set; }
        public static Texture block2 { get; private set; }
        public static Texture block3 { get; private set; }
        public static Texture block4 { get; private set; }
        public static Font font1 { get; private set; }
        public static Texture racket1 { get; private set; }
        public static Texture racket2 { get; private set; }
        public static Texture racket3 { get; private set; }
        public static Texture racket4 { get; private set; }
        public static Texture racket5 { get; private set; }
        public static Texture racket6 { get; private set; }
        public static Texture bar1 { get; private set; }
        public static Texture bar2 { get; private set; }
        public static Texture bar3 { get; private set; }
        public static Texture bar4 { get; private set; }
        public static Texture bar5 { get; private set; }
        public static Texture bar6 { get; private set; }
        public static Texture blueBall { get; private set; }
        public static Texture silverStar { get; private set; }
        public static Texture goldStar { get; private set; }

        public static void LoadAllData()
        {
            LodaBlocks();
            LoadFonts();
            LoadRackets();
            LoadBalls();
            LoadMiniRackets();
            LoadUI();
        }

        public static bool LoadFonts()
        {
            font1 = new Font("Content/Fonts/Zelta-Six.otf");
            return (font1 != null);
        }

        public static bool LodaBlocks()
        {
            block1 = new Texture("Content/Graphics/Blocks/Block1.png");
            block2 = new Texture("Content/Graphics/Blocks/Block2.png");
            block3 = new Texture("Content/Graphics/Blocks/Block3.png");
            block4 = new Texture("Content/Graphics/Blocks/Block4.png");
            return (block1 != null && block2 != null && block3 != null && block4 != null);
        }

        public static bool LoadRackets()
        {
            racket1 = new Texture("Content/Graphics/Rackets/Racket1.png");
            racket2 = new Texture("Content/Graphics/Rackets/Racket2.png");
            racket3 = new Texture("Content/Graphics/Rackets/Racket3.png");
            racket4 = new Texture("Content/Graphics/Rackets/Racket4.png");
            racket5 = new Texture("Content/Graphics/Rackets/Racket5.png");
            racket6 = new Texture("Content/Graphics/Rackets/Racket6.png");
            return (racket1 == null && racket2 == null && racket3 == null 
                    && racket4 == null && racket5 == null &&
                    racket6 == null);
        }

        public static bool LoadMiniRackets()
        {
            bar1 = new Texture("Content/Graphics/Rackets/Bar1.png");
            bar2 = new Texture("Content/Graphics/Rackets/Bar2.png");
            bar3 = new Texture("Content/Graphics/Rackets/Bar3.png");
            bar4 = new Texture("Content/Graphics/Rackets/Bar4.png");
            bar5 = new Texture("Content/Graphics/Rackets/Bar5.png");
            bar6 = new Texture("Content/Graphics/Rackets/Bar6.png");
            return (bar1 == null && bar2 == null && bar3 == null
                    && bar4 == null && bar5 == null &&
                    bar6 == null);
        }

        public static bool LoadBalls()
        {
            blueBall = new Texture("Content/Graphics/Balls/Ball_Blue.png");
            return (blueBall == null);
        }

        public static bool LoadUI()
        {
            silverStar = new Texture("Content/Graphics/UI/StarSilver.png");
            goldStar = new Texture("Content/Graphics/UI/StarGold.png");
            return (silverStar != null && goldStar != null);
        }
    }
}