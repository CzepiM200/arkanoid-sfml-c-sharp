using SFML.Graphics;
using SFML.System;
using System.Collections.Generic;

namespace ArkanoidCS
{
    class ChoseRacketManu : Menu
    {
        #region Private Fields
        private List<Sprite> racketSprites = new List<Sprite>();
        private Sprite silverSprite = new Sprite(DataLoader.silverStar);
        private Sprite goldSprite = new Sprite(DataLoader.goldStar);
        #endregion
        #region Constructor
        public ChoseRacketManu()
        {
            SetApperance();
        }
        #endregion
        #region Public Methods
        public void DrawSprites(RenderWindow window, UserData data)
        {
            for (int i = 0; i < AmountOfPositions - 1; i++)
            {
                if (data.equipment[i])
                {
                    goldSprite.Position = new Vector2f(20.0f, 15.0f + i * (GameProperties.GetWinY * 0.16f));
                    window.Draw(goldSprite);
                }
                else
                {
                    silverSprite.Position = new Vector2f(20.0f, 15.0f + i * (GameProperties.GetWinY * 0.16f));
                    window.Draw(silverSprite);
                }
            }
            foreach (Sprite sprite in racketSprites)
            {
                window.Draw(sprite);
            }
        }
        #endregion
        #region Private Methods
        private void Initialize()
        {
            for (int i = 0; i < AmountOfPositions; i++)
            {
                options[i] = new Text(ChoseString(i), font, defalutFontSize - 10 + 50);
            }
        }

        private void SetApperance()
        {
            AmountOfPositions = 6;
            Initialize();
            SetFontStyle();
            SetSprites();
        }

        private string ChoseString(int i)
        {
            switch (i)
            {
                case 0: return "FAST RACKET";
                case 1: return "LONG RACKET";
                case 2: return "STONG RACKET";
                case 3: return "LUCKY RACKET";
                case 4: return "MASTER RACKET";
                case 5: return "BASIC RACKET";
                default: return "ERROR";
            }
        }

        private void SetFontStyle()
        {
            for (int i = 0; i < AmountOfPositions; i++)
            {
                if (i != 0) options[i].Color = firstColor;
                else options[i].Color = secoundColor;
                options[i].Position = new Vector2f(70.0f, 15.0f + i * (GameProperties.GetWinY * 0.16f));
            }
        }

        private void SetSprites()
        {
            LoadSprites();
            SetSpritesPositions();
        }

        private void LoadSprites()
        {
            racketSprites.Add(new Sprite(DataLoader.bar3));
            racketSprites.Add(new Sprite(DataLoader.bar6));
            racketSprites.Add(new Sprite(DataLoader.bar2));
            racketSprites.Add(new Sprite(DataLoader.bar4));
            racketSprites.Add(new Sprite(DataLoader.bar5));
            racketSprites.Add(new Sprite(DataLoader.bar1));
        }

        private void SetSpritesPositions()
        {
            int i = 0;
            foreach (Sprite sprite in racketSprites)
            {
                sprite.Position = new Vector2f(GameProperties.GetWinX - 150, 40.0f + i * (GameProperties.GetWinY * 0.16f));
                i++;
            }
        }
#endregion
    }
}
