using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoPong
{
    class Sprite
    {
        public Texture2D texture2D;
        public Vector2 position;
        public Rectangle HitBox;
        Color tint;
        public int xSpeed = 2;
       public int ySpeed = 2;

        public Sprite(Texture2D texture2D, Vector2 position, Color tint)
        {
            this.texture2D = texture2D;
            this.position = position;
            this.tint = tint;

            
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture2D, position, tint);
        }
        public void Update(Viewport viewport, Paddle paddle1, Paddle paddle2)
        {
            HitBox = new Rectangle((int)position.X, (int)position.Y, texture2D.Width, texture2D.Height);

            if (HitBox.Intersects(paddle1.HitBox))
            {
                xSpeed = 2;
            }
            if(HitBox.Intersects(paddle2.HitBox))
            {
                xSpeed = -2;
            }

            if (position.Y + texture2D.Height > viewport.Height)
            {
                ySpeed = -2;
            }
            if (position.Y < 0)
            {
                ySpeed = 2;
            }
            position.X += 5 * xSpeed;
            position.Y += 5 * ySpeed;
        }
    }
}
