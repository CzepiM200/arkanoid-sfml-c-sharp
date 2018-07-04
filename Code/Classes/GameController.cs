using SFML.Graphics;
using SFML.System;
using SFML.Window;
using System;
using System.Collections.Generic;


namespace ArkanoidCS
{
    class GameController
    {
        #region PrivateFields
        private static RenderWindow window = new RenderWindow(new VideoMode (GameProperties.GetWinX, GameProperties.GetWinY), "ArkanoidCS", Styles.Default);
        private GameStatus gameStatus = GameStatus.MENU;
        private readonly Font defaultFont = new Font("Content/Fonts/Zelta-Six.otf");
        private Menu menu;
        private Racket racket;
        private bool controllerLoop { get; set; } = true;
        private UserData data = new UserData();
        #endregion
        public GameController()
        {
            window.SetFramerateLimit(60);
            window.Closed += (sender, arg) => window.Close();
            window.KeyPressed += KeyPressedEvent;
            DataLoader.LoadAllData();
        }
        #region ControllMethods
        public void StartGame()
        {
            while (window.IsOpen && controllerLoop)
            {
                CheckGameStatus();
            }
        }

        private void CheckGameStatus()
        {   
            while (window.IsOpen)
            {
                controllerLoop = true;
                switch (gameStatus)
                {
                    case GameStatus.MENU:
                        OpenMenu();
                        break;
                    case GameStatus.SHOP:
                        OpenShop();
                        break;
                    case GameStatus.OPTIONS:
                        OpenOptions();
                        break;
                    case GameStatus.GAME:
                        OpenGame();
                        break;
                    case GameStatus.STATS:
                        OpenStats();
                        break;
                    case GameStatus.EXIT:
                        data.SaveDataToFile();
                        data.SaveStatsDataToFile();
                        window.Close();
                        break;
                }
            }
        }
#endregion
        #region MainMenuOptions
        private void OpenMenu()
        {
            menu = new MainMenu(defaultFont);
            while (window.IsOpen && controllerLoop)
            {
                window.Clear();
                menu?.Draw(ref window);
                window.DispatchEvents();
                window.Display();
            }
        }

        private void OpenShop()
        {
            menu = new ShopMenu(data);
            ShopMenu shop = (ShopMenu)menu;
            while (window.IsOpen && controllerLoop)
            {
                window.Clear();
                menu.Draw(ref window);
                shop.DrawSprites(window);
                shop.DrawCoinsText(window);
                window.DispatchEvents();
                window.Display();
            }
        }

        private void OpenOptions()
        {
            menu = new OptionsMenu();
            while (window.IsOpen && controllerLoop)
            {
                window.Clear();
                menu.Draw(ref window);
                window.DispatchEvents();
                window.Display();
            }
        }

        private void OpenStats()
        {
            Text[] statsTexts = new Text[GameProperties.AmountOfStats];
            for (int i = 0; i < GameProperties.AmountOfStats; i++)
            {
                statsTexts[i] = new Text("[" + (i + 1) + "] " + data.stats[i].ToString(), DataLoader.font1, 30);
                statsTexts[i].Position = new Vector2f(70f, 70 + 40 * i);
                statsTexts[i].Color = Color.White;
            }
            while (window.IsOpen && controllerLoop)
            {
                window.Clear();
                for (int i = (int)GameProperties.AmountOfStats - 1; i >= 0; i--) window.Draw(statsTexts[i]);
                window.DispatchEvents();
                window.Display();
            }
        }

        #region Game

        void OpenGame()
        {
            ChoseRacker();
            if (gameStatus == GameStatus.CHOSERACKET)
            {
                gameStatus = GameStatus.GAME;
                Ball ball = new Ball(GameProperties.GetWinX / 2, GameProperties.GetWinY / 2);
                if (racket is StrongRacket || racket is MasterRacket) ball.AddSpeed(2);
                Console.WriteLine("Speed: " + ball.ballVelocity);
                List<Block> blocks = new List<Block>();

                int maxPoints = 0;
                int temp;

                GenerateLevel(blocks, ref maxPoints);
                Console.WriteLine("Ilosc blokow: " + maxPoints);

                while (window.IsOpen && controllerLoop)
                {
                    temp = 0;
                    UpdateGameFrame(ball, blocks);
                    window.DispatchEvents();
                    window.Clear();
                    temp = DrawGameFrame(blocks, temp, ball);
                    if (CheckLoseStatus(ball, maxPoints, temp)) break;
                    if (CheckWinStatus(temp, maxPoints)) break;
                }
            }
            else gameStatus = GameStatus.MENU;
        }

        private void UpdateGameFrame(Ball ball, List<Block> blocks)
        {
            BallAndRacketCollisions(ball);
            foreach (var block in blocks)
            {
                if (block.rectangleShape.GetGlobalBounds().Intersects(ball.circle.GetGlobalBounds()))
                {
                    if (block.status) BallAndBlockCollisions(ball, block);
                    break;
                }
            }

            ball.Update();
            racket.Update();
        }

        private int DrawGameFrame(List<Block> blocks, int temp, Ball ball)
        {
            foreach (var block in blocks)
            {
                if (block.status)
                {
                    block.DrawBlock(window);
                    temp++;
                }
            }

            ball.DrawBall(window);
            racket.DrawRacket(window);
            window.Display();
            return temp;
        }

        private bool CheckWinStatus(int temp, int maxPoints)
        {
            if (temp == 0)
            {
                gameStatus = GameStatus.GAMESUMATION;
                if (racket is LuckyRacket || racket is MasterRacket)
                    OpenGameSumation(GameSolution.WIN, maxPoints - temp, 2 * (maxPoints - temp));
                else OpenGameSumation(GameSolution.WIN, (maxPoints - temp), (maxPoints - temp) / 2);
                return true;
            }

            return false;
        }

        private bool CheckLoseStatus(Ball ball, int maxPoints, int temp)
        {
            if (ball.GetSidePosition('B') >= racket.GetSidePosition('B'))
            {
                gameStatus = GameStatus.GAMESUMATION;
                if (racket is LuckyRacket || racket is MasterRacket)
                    OpenGameSumation(GameSolution.LOSE, maxPoints - temp, maxPoints - temp);
                else OpenGameSumation(GameSolution.LOSE, (maxPoints - temp), (maxPoints - temp) / 4);
                return true;
            }

            return false;
        }

        private void ChoseRacker()
        {
            gameStatus = GameStatus.CHOSERACKET;
            menu = new ChoseRacketManu();
            ChoseRacketManu shop = (ChoseRacketManu)menu;
            while (window.IsOpen && controllerLoop)
            {
                window.Clear();
                menu.Draw(ref window);
                shop.DrawSprites(window, data);
                window.DispatchEvents();
                window.Display();
            }
            controllerLoop = true;
        }

        private static void GenerateLevel(List<Block> blocks, ref int maxPoints)
        {
            Random rand = new Random();
            for (uint i = GameProperties.FrameSizeInGame + GameProperties.BlockWidth / 2;
                i <= GameProperties.GetWinX - (GameProperties.BlockWidth / 2);
                i += GameProperties.BlockWidth)
            {
                for (uint j = (GameProperties.FrameSizeInGame + GameProperties.BlockHeight / 2), k = 0;
                    k < GameProperties.AmountOfBlockInoY;
                    j += GameProperties.BlockHeight, k++)
                {
                    blocks.Add(new Block(i, j, (Blocks) (rand.Next(0, 4))));
                    maxPoints++;
                }
            }
        }

        void BallAndRacketCollisions(Ball ball)
        {
            if(racket != null && ball != null)
            if (racket.rectangleShape.GetGlobalBounds().Intersects(ball.circle.GetGlobalBounds()))
            {          
                double lenghtOfelocityVecotr =
                    Math.Sqrt(Math.Pow(ball.ballVelocityVector.X, 2) + Math.Pow(ball.ballVelocityVector.Y, 2));
                double newXVelocity = CheckHitAngle(ball);
                double newYVelocity = Math.Sqrt(Math.Abs(Math.Pow(lenghtOfelocityVecotr, 2) - Math.Pow(newXVelocity, 2)));
                ball.ballVelocityVector = new Vector2f((float)newXVelocity, (float)newYVelocity * -1);
            }
        }

        void BallAndBlockCollisions(Ball ball, Block block)
        {
                block.DestroyBlock();
                float leftDifference = (Math.Abs(ball.GetSidePosition('R') - block.GetSidePosition('L')));
                float rigthDifference = (Math.Abs(ball.GetSidePosition('L') - block.GetSidePosition('R')));
                float bottomDifference = (Math.Abs(ball.GetSidePosition('T') - block.GetSidePosition('B')));
                float topDifference = (Math.Abs(ball.GetSidePosition('B') - block.GetSidePosition('T')));

                if (leftDifference > rigthDifference)
                {
                    if (bottomDifference > topDifference)
                    {
                        if (rigthDifference > topDifference) ball.RevertVelocityVector(false, true);
                        else if (rigthDifference < topDifference) ball.RevertVelocityVector(true, false);
                        else if (rigthDifference == topDifference) ball.RevertVelocityVector(true, true);
                    }
                    else if (bottomDifference < topDifference)
                    {
                        if (rigthDifference > bottomDifference) ball.RevertVelocityVector(false, true);
                        else if (rigthDifference < bottomDifference) ball.RevertVelocityVector(true, false);
                        else if (rigthDifference == bottomDifference) ball.RevertVelocityVector(true, true);
                    }
                }
                else
                {
                    if (bottomDifference > topDifference)
                    {
                        if (leftDifference > topDifference) ball.RevertVelocityVector(false, true);
                        else if (leftDifference < topDifference) ball.RevertVelocityVector(true, false);
                        else if (leftDifference == topDifference) ball.RevertVelocityVector(true, true);
                    }
                    else if (bottomDifference < topDifference)
                    {
                        if (leftDifference > bottomDifference) ball.RevertVelocityVector(false, true);
                        else if (leftDifference < bottomDifference) ball.RevertVelocityVector(true, false);
                        else if (leftDifference == bottomDifference) ball.RevertVelocityVector(true, true);
                    }
                }
        }

        double CheckHitAngle(Ball ball)
        {
            bool sign = false;
            double temp = ball.circle.Position.X - racket.rectangleShape.Position.X;
            //Console.WriteLine("Ret: " + temp);
            if (temp < 0){sign = true; temp *= -1;}
            double tempSqr = Math.Sqrt(temp);
            if (sign) return ((racket.lenghtX * 0.6) * -1 *tempSqr) / racket.lenghtX;
            return ((racket.lenghtX * 0.6) * tempSqr) / racket.lenghtX;

        }

        #endregion
#endregion
        #region GameSumation

        void OpenGameSumation(GameSolution gameSolution, int extraPoints, int extraCoins)
        {
            Text[] texts = new Text[4];
            Text firstText = new Text();
            UpdateData(extraPoints, extraCoins);

            if (gameSolution == GameSolution.WIN)
            {
                firstText.Color = Color.Green;
                firstText.DisplayedString ="YOU WIN";
            }
            else 
            {
                firstText.Color = Color.Red;
                firstText.DisplayedString = "GAME OVER";
            }
            firstText.Font = DataLoader.font1;
            firstText.CharacterSize = 70;
            firstText.Position = new Vector2f(GameProperties.GetWinX/6, GameProperties.GetWinY/15);
            for (int i = 0; i < 4; i++) texts[i] = new Text();
            texts[0].DisplayedString = "Extra points: " + extraPoints;
            texts[1].DisplayedString = "Total points: " + data.points;
            texts[2].DisplayedString = "Extra coins: " + extraCoins;
            texts[3].DisplayedString = "Total coins: " + data.coins;
            for(int i=0; i<4 ; i++)
            {
                texts[i].Color = Color.White;
                texts[i].CharacterSize = 40;
                texts[i].Font = DataLoader.font1;
                texts[i].Position = new Vector2f(GameProperties.GetWinX / 6, 3 * GameProperties.GetWinY / 15 + 80*(i+1));
            }

            while (window.IsOpen && controllerLoop)
            {
                window.Clear();
                window.Draw(firstText);
                foreach(Text text in texts) window.Draw(text);
                window.DispatchEvents();
                window.Display();
            }
        }

        private void UpdateData(int extraPoints, int extraCoins)
        {
            data.coins += extraCoins;
            data.points += extraPoints;
            data.SaveDataToFile();
            data.AddStat(extraPoints);
            data.SaveStatsDataToFile();
        }

#endregion
        #region KeyEvents

        private void KeyPressedEvent(object sender, KeyEventArgs e)
        {
            switch (gameStatus)
            {
                case GameStatus.GAME:
                    BackToMenuAfterEsc(e);
                    break;
                case GameStatus.CHOSERACKET:
                    BackToMenuAfterEsc(e);
                    MenuOptions(e);
                    if (e.Code == Keyboard.Key.Return) ChoseRacket(e);
                    break;
                case GameStatus.SHOP:
                    MenuOptions(e);
                    BackToMenuAfterEsc(e);
                    if (e.Code == Keyboard.Key.Return) ShopOptions(e);

                    break;
                case GameStatus.OPTIONS:
                    BackToMenuAfterEsc(e);
                    MenuOptions(e);
                    SettingsOptions(e);
                    break;
                case GameStatus.STATS:
                    BackToMenuAfterEsc(e);
                    break;
                case GameStatus.MENU:
                    if (e.Code == Keyboard.Key.Escape) 
                    {
                        gameStatus = GameStatus.EXIT;
                        controllerLoop = false;
                    }
                    else if (e.Code == Keyboard.Key.Return) MainMenuOptions(e);
                    else MenuOptions(e);
                    break;
                case GameStatus.GAMESUMATION:
                    BackToMenuAfterEsc(e);
                    break;
            }
        }

        private void BackToMenuAfterEsc(KeyEventArgs e)
        {
            if (e.Code == Keyboard.Key.Escape)
            {
                gameStatus = GameStatus.MENU;
                controllerLoop = false;
            }
        }

        private void MenuOptions(KeyEventArgs e)
        {
            if (e.Code == Keyboard.Key.Up || e.Code == Keyboard.Key.W) menu.MoveUp();
            else if (e.Code == Keyboard.Key.Down || e.Code == Keyboard.Key.S) menu.MoveDown();
        }

        private void MainMenuOptions(KeyEventArgs e)
        {
            switch (menu.SelectedOption)
            {
                case 0:
                    gameStatus = GameStatus.GAME;
                    break;
                case 1:
                    gameStatus = GameStatus.SHOP;
                    break;
                case 2:
                    gameStatus = GameStatus.OPTIONS;
                    break;
                case 3:
                    gameStatus = GameStatus.STATS;
                    break;
                case 4:
                    gameStatus = GameStatus.EXIT;
                    break;
                default:
                    gameStatus = GameStatus.MENU;
                    break;
            }
            controllerLoop = false;
        }

        private void ShopOptions(KeyEventArgs e)
        {
            ShopMenu shopMenu = (ShopMenu) menu;
            switch (menu.SelectedOption)
            {
                case 0:
                    shopMenu.BuyRacket(data, Rackets.FAST);
                    break;
                case 1:
                    shopMenu.BuyRacket(data, Rackets.LONG);
                    break;
                case 2:
                    shopMenu.BuyRacket(data, Rackets.STRONG);
                    break;
                case 3:
                    shopMenu.BuyRacket(data, Rackets.LUCKY);
                    break;
                case 4:
                    shopMenu.BuyRacket(data, Rackets.MASTER);
                    break;
            }
        }

        private void SettingsOptions(KeyEventArgs e)
        {
            if (e.Code == Keyboard.Key.Return)
                switch (menu.SelectedOption)
            {
                case 0:
                    data.LoadFromData();
                    data.LoadStatsFromData();
                    break;
                case 1:
                    data.SaveDataToFile();
                    data.SaveStatsDataToFile();
                    break;
                case 2:
                    data.coins += 10000;
                    data.points += 12345;
                    break;
                case 3:
                    data.SetToDefault();
                    break;
                case 4:
                    gameStatus = GameStatus.MENU;
                    controllerLoop = false;
                    break;
            }
        }

        private void ChoseRacket(KeyEventArgs e)
        {
            switch (menu.SelectedOption)
            {
                case 5:
                    if (data.CheckEquipmentStatus(Rackets.BASIC))
                    {
                        racket = new BasicRacket(GameProperties.GetWinX / 2, GameProperties.GetWinY - 20);
                        controllerLoop = false;
                    }

                    break;
                case 0:
                    if (data.CheckEquipmentStatus(Rackets.FAST))
                    {
                        racket = new FastRacket(GameProperties.GetWinX / 2, GameProperties.GetWinY - 20);
                        controllerLoop = false;
                    }
                    break;
                case 1:
                    if (data.CheckEquipmentStatus(Rackets.LONG))
                    {
                        racket = new LongRacket(GameProperties.GetWinX / 2, GameProperties.GetWinY - 20);
                        controllerLoop = false;
                    }
                    break;
                case 2:
                    if (data.CheckEquipmentStatus(Rackets.STRONG))
                    {
                        racket = new StrongRacket(GameProperties.GetWinX / 2, GameProperties.GetWinY - 20);
                        controllerLoop = false;
                    }
                    break;
                case 3:
                    if (data.CheckEquipmentStatus(Rackets.LUCKY))
                    {
                        racket = new LuckyRacket(GameProperties.GetWinX / 2, GameProperties.GetWinY - 20);
                        controllerLoop = false;
                    }
                    break;
                case 4:
                    if (data.CheckEquipmentStatus(Rackets.MASTER))
                    {
                        racket = new MasterRacket(GameProperties.GetWinX / 2, GameProperties.GetWinY - 20);
                        controllerLoop = false;
                    }
                    break;
            }
            IRacketBehaviour racketBehaviour = (IRacketBehaviour) racket;
            racketBehaviour?.SetSprite(GameProperties.GetWinX / 2, GameProperties.GetWinY - 20);
        }

        #endregion
    }
}