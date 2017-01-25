using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace BreakOut_Android
{
    class Ball
    {
        Vector2 motion;
        Vector2 position;
        float ballSpeed = 4;
        Texture2D texture;
        Rectangle screenBounds;

        public Ball(Texture2D texture, Rectangle screenBounds)
        {
            this.texture = texture;
            this.screenBounds = screenBounds;
        }

        public void Update()
        {
            position += motion * ballSpeed;
            CheckWallCollision();
        }

        private void CheckWallCollision()
        {
            if (position.X < 0)
            {
                position.X = 0;
                motion.X *= -1;
            }
            if (position.X + texture.Width > screenBounds.Width)
            {
                position.X = screenBounds.Width - texture.Width;
                motion.X *= -1;
            }
            if (position.Y < 0)
            {
                position.Y = 0;
                motion.Y *= -1;
            }
        }

        public void SetInStartPosition(Rectangle paddleLocation)
        {
            motion = new Vector2(1, -1);
            position.Y = paddleLocation.Y - texture.Height;
            position.X = paddleLocation.X + (paddleLocation.Width - texture.Width) / 2;
        }

        public bool OffBottom()
        {
            if (position.Y > screenBounds.Height)
                return true;
            return false;
        }

        public void PaddleCollision(Rectangle paddleLocation)
        {
            Rectangle ballLocation = GetBounds();
            if (paddleLocation.Intersects(ballLocation))
            {
                position.Y = paddleLocation.Y - texture.Height;
                motion.Y *= -1;
                if (position.X > paddleLocation.X + paddleLocation.Width / 2 + ballLocation.Width)
                {
                    motion.X = 1;
                }
                else if (position.X < paddleLocation.X + paddleLocation.Width / 2 - ballLocation.Width)
                {
                    motion.X = -1;
                }
            }
        }

        public bool BlockCollision(Rectangle blockLocation)
        {
            Rectangle ballLocation = GetBounds();
            if (blockLocation.Intersects(ballLocation))
            {
                if (position.X > blockLocation.X + 24 || position.X < blockLocation.X)
                {
                    motion.X *= -1;
                }
                else
                {
                    if (position.Y < blockLocation.Y)
                    {
                        position.Y = blockLocation.Y - texture.Height;
                    }
                    else
                    {
                        position.Y = blockLocation.Y + texture.Height;
                    }
                    motion.Y *= -1;
                }
                return true;
            }
            return false;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, position, Color.White);
        }

        public Rectangle GetBounds()
        {
            return new Rectangle(
                                (int)position.X,
                                (int)position.Y,
                                texture.Width,
                                texture.Height);
        }
    }
}
