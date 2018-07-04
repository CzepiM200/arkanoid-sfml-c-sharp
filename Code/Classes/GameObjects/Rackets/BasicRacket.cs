using SFML.Graphics;
using SFML.System;


namespace ArkanoidCS
{
    class BasicRacket : Racket, IRacketBehaviour
    {
        public BasicRacket(uint x, uint y) : base(x, y)
        {
            lenghtX = 200;
            lenghtY = 32;
            rectangleShape.Size = new Vector2f(lenghtX, lenghtY);
            rectangleShape.Origin = new Vector2f(lenghtX / 2, lenghtY / 2);
        }

        public void SetSprite(uint positioX, uint positionY)
        {
            sprite = new Sprite(DataLoader.racket1, new IntRect(0, 0, (int) lenghtX, (int) lenghtY))
            {
                Texture = {Smooth = true},
                Origin = new Vector2f(lenghtX / 2, lenghtY / 2),
                Position = new Vector2f(positioX, positionY)
            };
        }
    }
}
