using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace BreakOut_Android
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Rectangle screenRectangle;
        Rectangle ballRect;
        Queue<Block> Blocks;
        Ball ball;
        Paddle paddle;
        float ScaleX;
        float ScaleY;
        Matrix matrix;
        Matrix invMatrix;
        int block_count;
        private readonly int VirtualWidth = 360;
        private readonly int VirtualHeight = 640;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            graphics.IsFullScreen = true;
            graphics.PreferredBackBufferWidth = 720;
            graphics.PreferredBackBufferHeight = 1280;
            graphics.SupportedOrientations = DisplayOrientation.Portrait;
            screenRectangle = new Rectangle(
                                0,
                                0,
                                VirtualWidth,
                                VirtualHeight);

            ScaleX = (float) graphics.PreferredBackBufferWidth / VirtualWidth;
            ScaleY = (float) graphics.PreferredBackBufferHeight / VirtualHeight;
            matrix = Matrix.CreateScale(ScaleX, ScaleY, 1.0f);

            invMatrix = Matrix.Invert( matrix );

        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
        }


        private void StartGame()
        {
            paddle.SetInStartPosition();
            ball.SetInStartPosition(paddle.GetBounds());
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            Texture2D temp;

            temp = Content.Load<Texture2D>("paddle");
            paddle = new Paddle(temp, screenRectangle, invMatrix);

            temp = Content.Load<Texture2D>("ball");
            ball = new Ball(temp, screenRectangle);

            temp = Content.Load<Texture2D>("block");
            Blocks = new Queue<Block>();

            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 15; j++)
                {
                    Blocks.Enqueue(new Block(temp, new Vector2((j * 32), (i * 24))));
                }
            }

            StartGame();
            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                Exit();

            paddle.Update();
            ball.Update();

            block_count = Blocks.Count;
            Block temp;

            for (int i = 0; i < block_count; i++)
            {
                temp = Blocks.Dequeue();
                if (!ball.BlockCollision(temp.GetBounds()))
                {
                    Blocks.Enqueue(temp);
                }
            }

            ball.PaddleCollision(paddle.GetBounds());
            if (ball.OffBottom())
                StartGame();

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin( transformMatrix: matrix );

            ball.Draw(spriteBatch);
            paddle.Draw(spriteBatch);

            foreach (Block block in Blocks)
            {
                block.Draw(spriteBatch);
            }

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
