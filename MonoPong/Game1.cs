using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace MonoPong
{        
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Sprite ball;
        Paddle paddle1;
        Paddle paddle2;
        KeyboardState keyboard;
        int leftPlayerScore = 0;
        int rightPlayerScore = 0;
        string gameText = "Press space to start";
        SpriteFont font;
        Random random = new Random();
        bool isGameRunning = true;
        bool isAI = false;
        bool LeftWins = false;
        bool RightWins = false;


        Sprite backGround;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }


        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            graphics.PreferredBackBufferHeight = 720;
            graphics.PreferredBackBufferWidth = 1280;
            graphics.ApplyChanges();

            IsMouseVisible = true;

            base.Initialize();
        }

        protected override void LoadContent()
        {

            //Ball
            spriteBatch = new SpriteBatch(GraphicsDevice);
            Texture2D ballTexture = Content.Load<Texture2D>("smirkBall2");
            Vector2 ballPos = new Vector2(100, 100);
            Color ballTint = Color.White;
            ball = new Sprite(ballTexture, ballPos, ballTint);
            //Paddles
            Texture2D paddleTexture = Content.Load<Texture2D>("paddle");
            Vector2 paddlePos = new Vector2(20, 100);
            Color paddleTint = Color.White;
            paddle1 = new Paddle(paddleTexture, paddlePos, paddleTint, TimeSpan.Zero);
            Texture2D paddle2Texture = Content.Load<Texture2D>("paddle2");
            Vector2 paddle2Pos = new Vector2(1230, 100);
            Color paddle2Tint = Color.White;
            paddle2 = new Paddle(paddle2Texture, paddle2Pos, paddle2Tint, TimeSpan.FromMilliseconds(250));
            //background
            Texture2D backgroundImage = Content.Load<Texture2D>("background");
            Vector2 backgroundPos = new Vector2(0, 0);
            Color backgroundTint = Color.White;
            backGround = new Sprite(backgroundImage, backgroundPos, backgroundTint);


            font = Content.Load<SpriteFont>("Font");

        }


        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }


        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            ball.Update(GraphicsDevice.Viewport, paddle1, paddle2);
            paddle2.AIUpdate(GraphicsDevice.Viewport, gameTime, random, isAI, Keys.Up, Keys.Down, ball.position);

            if(ball.position.X < 0)
            {
                ball.position.X = 640;
                ball.position.Y = 360;
                ball.xSpeed = 0;
                ball.ySpeed = 0;
                isGameRunning = false;
                rightPlayerScore++;
            }
            else if(ball.position.X + ball.texture2D.Width > GraphicsDevice.Viewport.Width)
            {
                ball.position.X = 640;
                ball.position.Y = 360;
                ball.xSpeed = 0;
                ball.ySpeed = 0;
                isGameRunning = false;
                leftPlayerScore++;
            }
            keyboard = Keyboard.GetState();
            if(keyboard.IsKeyDown(Keys.Space))
            {
                ball.ySpeed = 2;
                if(leftPlayerScore > rightPlayerScore)
                {
                    ball.xSpeed = -2;
                    isGameRunning = true;
                }
                else
                {
                    isGameRunning = true;
                    ball.xSpeed = 2;
                }
            }
            if(keyboard.IsKeyDown(Keys.I))
            {
                isAI = true;
            }
            paddle2.AIUpdate(GraphicsDevice.Viewport, gameTime, random, isAI, Keys.Up, Keys.Down, ball.position);
            // TODO: Add your update logic here
            if(leftPlayerScore > 10)
            {
                LeftWins = true;
            }
            else if(rightPlayerScore > 10)
            {
                RightWins = true;
            }
            base.Update(gameTime);
        }


        protected override void Draw(GameTime gameTime)
        {
            
            GraphicsDevice.Clear(Color.CornflowerBlue);
            
            spriteBatch.Begin();
            backGround.Draw(spriteBatch);
            paddle1.Draw(spriteBatch);
            ball.Draw(spriteBatch);
            paddle1.PlayerUpdate(Keys.W, Keys.S, GraphicsDevice.Viewport);
            paddle2.Draw(spriteBatch);
                       

            spriteBatch.DrawString(font, leftPlayerScore.ToString(), new Vector2(400, 20), Color.Black);
            spriteBatch.DrawString(font, rightPlayerScore.ToString(), new Vector2(800, 20), Color.Black);
            if(!isGameRunning)
            {
                spriteBatch.DrawString(font, gameText, new Vector2(500, 50), Color.Black);


            }
            if(!isAI)
            {
                spriteBatch.DrawString(font, "Press I for AI", new Vector2(500, 70), Color.Black);
            }
            if(LeftWins)
            {
                spriteBatch.DrawString(font, "Left Player Wins!!!", new Vector2(500, 90), Color.Black);
            }
            else if(RightWins)
            {
                spriteBatch.DrawString(font, "Right Player Wins!!!", new Vector2(500, 900), Color.Black);
            }


            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
