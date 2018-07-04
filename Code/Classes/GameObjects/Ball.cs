using SFML.Graphics;
using SFML.System;
using System;

namespace ArkanoidCS
{
    class Ball : Drawable
    {
        #region Public Fields
        public CircleShape circle { get; protected set; } = new CircleShape();
        public float ballVelocity { get; protected set; } = 8.0f;
        public float ballRadius { get; protected set; }= 10.0f;
        public Vector2f ballVelocityVector { get; set; }
        public Sprite sprite { get; protected set; }
        #endregion
        #region Constructors
        public Ball(uint x, uint y)
        {
            circle.Radius = ballRadius;
            circle.Position = new Vector2f(x,y);
            circle.Origin = new Vector2f(ballRadius/2, ballRadius/2);
            circle.FillColor = Color.White;
            sprite = new Sprite(DataLoader.blueBall);
            sprite.Origin = circle.Origin;
            sprite.Position = circle.Position;
            ballVelocityVector = new Vector2f(0.1f, ballVelocity);
        }
        #endregion
        #region Public Methods
        public void Update()
        {
            circle.Position += ballVelocityVector;
            sprite.Position = circle.Position;
            if (GetSidePosition('L') <= 0)
            {
                RevertVelocityVector(true, false);
            }
            else if (GetSidePosition('R') >= GameProperties.GetWinX)
            {
                RevertVelocityVector(true, false);
            }
            else if (GetSidePosition('T') <= 0)
            {
                RevertVelocityVector(false, true);
            }
            FixPosition();
        }

        public float GetSidePosition(char side)
        {
            if (side == 'R') return circle.Position.X + circle.Radius;
            if (side == 'L') return circle.Position.X - circle.Radius;
            if (side == 'T') return circle.Position.Y - circle.Radius;
            if (side == 'B') return circle.Position.Y + circle.Radius;
            return -1;
        }

        public void RevertVelocityVector(bool x, bool y)
        {
            if (x) ballVelocityVector = new Vector2f(ballVelocityVector.X * -1, ballVelocityVector.Y);
            if (y) ballVelocityVector = new Vector2f(ballVelocityVector.X, ballVelocityVector.Y * -1);
        }

        public void Draw(RenderTarget target, RenderStates states)
        {
            target.Draw(this.circle, states);
        }

        public void DrawBall(RenderWindow window)
        {
            window.Draw(sprite);
        }

        public void AddSpeed(int value)
        {
            ballVelocity += value;
            ballVelocityVector += new Vector2f(0f, ballVelocity);
        }
        #endregion
        #region Private Methods
        private void FixPosition()
        {
            if (GetSidePosition('L') < 0)
            {
                circle.Position += new Vector2f(Math.Abs(GetSidePosition('L')), 0f);
            }
            else if (GetSidePosition('R') > GameProperties.GetWinX)
            {
                circle.Position -= new Vector2f(GetSidePosition('R') - GameProperties.GetWinX, 0f);
            }
            else if (GetSidePosition('T') < 0)
            {
                circle.Position += new Vector2f(0f, Math.Abs(GetSidePosition('T')));
            }
        }
#endregion
    }
}
