using System.Globalization;
using SFML.Graphics;
using SFML.System;

namespace ArkanoidCS
{
    class Block : RectGameObject
    {
        #region Public Fields
        public bool status { get; set; } = true;
        public Blocks typeOfBlock { get; protected set; }
        public Sprite sprite { get; private set; }
        public Vector2f position { get; protected set; }
        #endregion
        #region Constructors
        public Block(uint x, uint y, Blocks type) : base(x, y)
        {  
            typeOfBlock = type;
            SetSprite();
            lenghtX = 60;
            lenghtY = 30;
            position = new Vector2f(x,y);
            rectangleShape.Size = new Vector2f(lenghtX, lenghtY);
            rectangleShape.Origin = new Vector2f(30, 15);            
        }
        #endregion
        #region Protected Methods
        protected void SetSprite()
        {
            if (typeOfBlock == Blocks.GREEN) sprite = new Sprite(DataLoader.block1);
            else if (typeOfBlock == Blocks.BLUE) sprite = new Sprite(DataLoader.block2);
            else if (typeOfBlock == Blocks.RED) sprite = new Sprite(DataLoader.block3);
            else if (typeOfBlock == Blocks.YELLOW) sprite = new Sprite(DataLoader.block4);
            sprite.Origin = new Vector2f(GameProperties.BlockWidth/2, GameProperties .BlockHeight/ 2);
            sprite.Position = rectangleShape.Position;
        }
        #endregion
        #region Public Methods
        public void DestroyBlock() => status = false;

        public new void Draw(RenderTarget target, RenderStates state) => target.Draw(new Sprite(DataLoader.block2), state);

        public void DrawBlock(RenderWindow window) => window.Draw(sprite);
#endregion
    }
}
