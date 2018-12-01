using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoPong
{
    class Paddle
    {
        public Texture2D texture2D;
        public Vector2 position;
        public Rectangle HitBox;
        Color tint;
        int ySpeed;
        TimeSpan time;
        TimeSpan elapsedTime;
        Random random;

        public Paddle(Texture2D texture2D, Vector2 vector2, Color color, TimeSpan time)
        {
            this.texture2D = texture2D;
            this.position = vector2;
            this.tint = color;
            this.time = time;
            elapsedTime = TimeSpan.Zero;
            
        }

        public void PlayerUpdate(Keys keyUp, Keys keyDown, Viewport viewport)
        {
            KeyboardState keyboard = Keyboard.GetState();

            if(keyboard.IsKeyDown(keyUp))
            {
                if(position.Y > 0)
                {
                    position.Y -= 10;
                }
                
            }
            else if(keyboard.IsKeyDown(keyDown))
            {
            if(position.Y + texture2D.Height < viewport.Height)
                {
                    position.Y += 10;
                }
                
            }

            HitBox = new Rectangle((int)position.X, (int)position.Y, texture2D.Width, texture2D.Height);
        }

        public void AIUpdate(Viewport viewport, GameTime gameTime, Random random, bool isAI, Keys keyUp, Keys keyDown, Vector2 ballPos)
        {
            int tmp = random.Next(-400, 400);
            
            KeyboardState keyboard = Keyboard.GetState();
            elapsedTime += gameTime.ElapsedGameTime;
            if (isAI)
            {


                if (elapsedTime > time)
                {
                    elapsedTime = TimeSpan.Zero;

                        if(ballPos.Y + tmp > position.Y)
                        {
                            position.Y += 25;
                        }
                        else
                        {
                            position.Y -= 25;
                        }
                    

                }
            }
            else
            {
                

                if (keyboard.IsKeyDown(keyUp))
                {
                    if (position.Y > 0)
                    {
                        position.Y -= 10;
                    }

                }
                else if (keyboard.IsKeyDown(keyDown))
                {
                    if (position.Y + texture2D.Height < viewport.Height)
                    {
                        position.Y += 10;
                    }

                }
            }
            HitBox = new Rectangle((int)position.X, (int)position.Y, texture2D.Width, texture2D.Height);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture2D, position, tint);
            
        }

    }
}
