using SFML.Graphics;
using SFML.System;

namespace ArkanoidCS
{
    class OptionsMenu : Menu
    {
        public OptionsMenu()
        {
            font = DataLoader.font1;
            SetApperance();
        }

        public OptionsMenu(Font font) : base(font)
        {
            SetApperance();
        }

        public OptionsMenu(Font font, Color firstColor, Color secoundColor) : base(font, firstColor, secoundColor)
        {
            SetApperance();
        }

        void Initialize()
        {
            for (int i = 0; i < AmountOfPositions; i++)
            {
                options[i] = new Text(ChoseString(i), font, defalutFontSize + 50);
            }
        }

        void SetApperance()
        {
            AmountOfPositions = 5;
            Initialize();
            SetFontStyle();
        }

        string ChoseString(int i)
        {
            switch (i)
            {
                case 3: return "Set default";
                case 2: return "Developer mode";
                case 1: return "Save data";
                case 0: return "Load Data";
                case 4: return "Back to menu";
                default: return "ERROR";
            }
        }

        void SetFontStyle()
        {
            for (int i = 0; i < AmountOfPositions; i++)
            {
                if (i != 0) options[i].Color = firstColor;
                else options[i].Color = secoundColor;
                options[i].Position = new Vector2f(50.0f, 15.0f + i * (GameProperties.GetWinY * 0.18f));
            }
        }
    }
}
