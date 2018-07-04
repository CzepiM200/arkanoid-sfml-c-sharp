using SFML.Graphics;
using SFML.System;
using System.Collections.Generic;

namespace ArkanoidCS
{
    class ShopMenu : Menu
    {
        private List<Sprite> racketSprites = new List<Sprite>();
        private Sprite silverSprite = new Sprite(DataLoader.silverStar);
        private Sprite goldSprite = new Sprite(DataLoader.goldStar);
        private Text coinsText;
        private UserData userData;

        public ShopMenu(UserData data)
        {
            userData = data;
            SetApperance();
        }

        void Initialize()
        {
            for (int i = 0; i < AmountOfPositions; i++)
            {
                options[i] = new Text(ChoseString(i), font, defalutFontSize - 10 + 50);
            }
            coinsText = new Text("Coins: " + userData.coins,font, 15);
        }

        void SetApperance()
        {
            AmountOfPositions = 5;
            Initialize();
            SetFontStyle();
            SetSprites();
        }

        string ChoseString(int i)
        {
            switch (i)
            {
                case 0: return "FAST RACKET";
                case 1: return "LONG RACKET";
                case 2: return "STONG RACKET"; 
                case 3: return "LUCKY RACKET";
                case 4: return "MASTER RACKET";
                default: return "ERROR";
            }
        }

        void SetFontStyle()
        {
            for (int i = 0; i < AmountOfPositions; i++)
            {
                if (i != 0) options[i].Color = firstColor;
                else options[i].Color = secoundColor;
                options[i].Position = new Vector2f(70.0f, 15.0f + i * (GameProperties.GetWinY * 0.18f));
            }
            coinsText.Color = Color.Yellow;
            coinsText.Position = new Vector2f(70.0f, GameProperties.GetWinY - 20);
        }

        void SetSprites()
        {
            LoadSprites();
            SetSpritesPositions();
        }

        void LoadSprites()
        {
            racketSprites.Add(new Sprite(DataLoader.bar3));
            racketSprites.Add(new Sprite(DataLoader.bar6));
            racketSprites.Add(new Sprite(DataLoader.bar2));
            racketSprites.Add(new Sprite(DataLoader.bar4));
            racketSprites.Add(new Sprite(DataLoader.bar5));
        }

        void SetSpritesPositions()
        {
            int i = 0;
            foreach (Sprite sprite in racketSprites)
            {
                sprite.Position = new Vector2f(GameProperties.GetWinX - 150 , 40.0f + i * (GameProperties.GetWinY * 0.18f));
                i++;
            }
        }

        public void DrawSprites(RenderWindow window)
        {
            for (int i = 0; i < AmountOfPositions; i++)
            {
                if (userData.equipment[i])
                {
                    goldSprite.Position = new Vector2f(20.0f, 15.0f + i * (GameProperties.GetWinY * 0.18f));
                    window.Draw(goldSprite);
                }
                else
                {
                    silverSprite.Position = new Vector2f(20.0f, 15.0f + i * (GameProperties.GetWinY * 0.18f));
                    window.Draw(silverSprite);
                } 
            }
                foreach (Sprite sprite in racketSprites)
            {
                window.Draw(sprite);
            }
        }

        public void DrawCoinsText(RenderWindow window)
        {
            coinsText.DisplayedString = "Coins: " + userData.coins;
            window.Draw(coinsText);
        }

        public void BuyRacket(UserData data, Rackets racket) //TODO
        {
            data.AddToEquiment(racket);
        }

    }
}
