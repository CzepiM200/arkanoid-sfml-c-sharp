using SFML.Graphics;
using SFML.System;


namespace ArkanoidCS
{
    class FastRacket : Racket, IRacketBehaviour
    {
        public FastRacket(uint x, uint y) : base(x, y)
        {
            lenghtX = 220;
            lenghtY = 32;
            rectangleShape.Size = new Vector2f(lenghtX, lenghtY);
            rectangleShape.Origin = new Vector2f(lenghtX / 2, lenghtY / 2);
        }

        public void SetSprite(uint positioX, uint positionY)
        {
            sprite = new Sprite(DataLoader.racket3, new IntRect(0, 0, (int)lenghtX, (int)lenghtY))
            {
                Texture = { Smooth = true },
                Origin = new Vector2f(lenghtX / 2, lenghtY / 2),
                Position = new Vector2f(positioX, positionY)
            };
        }
    }
}