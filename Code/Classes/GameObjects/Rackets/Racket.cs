using SFML.Graphics;
using SFML.System;
using SFML.Window;

namespace ArkanoidCS
{
    abstract class Racket : RectGameObject
    {
        #region Protected Fields
        protected float racketVelocity = 10.0f;
        #endregion
        #region Public Fields
        public Sprite sprite { get; protected set; }
        #endregion
        #region Constructor
        protected Racket(uint x, uint y) : base (x, y)
        {
            rectangleShape.FillColor = new Color(84, 84, 82);
        }
#endregion
        #region Public Methods
        public void Update()
        {
            if (Keyboard.IsKeyPressed(Keyboard.Key.Left))
            {
                if(GetSidePosition('L') > 0) MoveRacketLeft();
            }
            else if (Keyboard.IsKeyPressed(Keyboard.Key.Right))
            {
                if (GetSidePosition('R') < GameProperties.GetWinX) MoveRacketRight();
            }
        }

        public void MoveRacketLeft() 
        {
            rectangleShape.Position += new Vector2f(-racketVelocity, 0f);
            sprite.Position = rectangleShape.Position;
        }

        public void MoveRacketRight()
        {
            rectangleShape.Position += new Vector2f(racketVelocity, 0f);
            sprite.Position = rectangleShape.Position;
        }

        public void DrawRacket(RenderWindow window) => window.Draw(sprite);
#endregion
    }
}
