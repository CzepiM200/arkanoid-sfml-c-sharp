using SFML.Graphics;
using SFML.System;

namespace ArkanoidCS
{
    abstract class RectGameObject : Drawable
    {
        #region Public Fields
        public uint lenghtX { get; protected set; }
        public uint lenghtY { get; protected set; }
        public RectangleShape rectangleShape { get; protected set; } = new RectangleShape();
        #endregion
        #region Constuctors
        protected RectGameObject(uint x, uint y)
        {
            rectangleShape.Position = new Vector2f(x, y);   
            rectangleShape.Origin = new Vector2f(lenghtX/2, lenghtY/2);
        }
        #endregion
        #region Public Methods
        public float GetSidePosition(char side) 
        {
            if (side == 'R') return rectangleShape.Position.X + (float)lenghtX / 2;
            if (side == 'L') return rectangleShape.Position.X - (float)lenghtX / 2;
            if (side == 'T') return rectangleShape.Position.Y - (float)lenghtY / 2;
            if (side == 'B') return rectangleShape.Position.Y + (float)lenghtY / 2;
            return -1;
        }

        public void Draw(RenderTarget target, RenderStates states) => target.Draw(rectangleShape);
#endregion
    }
}
