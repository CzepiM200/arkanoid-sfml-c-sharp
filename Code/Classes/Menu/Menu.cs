using SFML.Graphics;

namespace ArkanoidCS
{
    abstract class Menu
    {
        #region Public Fields
        public uint AmountOfPositions { get; protected set; }
        public uint SelectedOption { get; private set; } = 0;
        #endregion
        #region Protected Fields
        protected uint defalutFontSize = 25;
        protected Font font;
        protected Color firstColor = Color.White;
        protected Color secoundColor = Color.Blue;
        protected Text[] options = new Text[MaxAmountOfOptions];
        #endregion`
        #region Private Fields
        private const int MaxAmountOfOptions = 10;
        #endregion
        #region Public Methods
        public Menu(Font font)
        {
            this.font = font;
        }

        public Menu()
        {
            font = DataLoader.font1;
        }

        public Menu(Font font, Color firstColor, Color secoundColor) : this (font)
        {
            this.firstColor = firstColor;
            this.secoundColor = secoundColor;
        }

        public void Draw(ref RenderWindow window)
        {
            for (int i = 0; i < AmountOfPositions; i++)
            {
                window.Draw(options[i]);
            }
        }

        public void MoveUp()
        {
            if (SelectedOption == 0)
            {
                options[SelectedOption].Color = firstColor;
                SelectedOption = AmountOfPositions - 1;
                options[SelectedOption].Color = secoundColor;
            }
            else
            {
                options[SelectedOption].Color = firstColor;
                SelectedOption--;
                options[SelectedOption].Color = secoundColor;
            }
        }


        public void MoveDown()
        {
            if (SelectedOption == AmountOfPositions - 1)
            {
                options[SelectedOption].Color = firstColor;
                SelectedOption = 0;
                options[SelectedOption].Color = secoundColor;
            }
            else
            {
                options[SelectedOption].Color = firstColor;
                SelectedOption++;
                options[SelectedOption].Color =  secoundColor ;
            }
        }
#endregion
    }
}
