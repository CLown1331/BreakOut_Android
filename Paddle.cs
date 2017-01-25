using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Input.Touch;

namespace BreakOut_Android
{
    class Paddle
    {
        Vector2 position;
        Vector2 motion;
        float paddleSpeed = 10f;
        Texture2D texture;
        Rectangle screenBounds;
        Matrix matrix;

        public Paddle(Texture2D texture, Rectangle screenBounds, Matrix matrix)
        {
            this.texture = texture;
            this.screenBounds = screenBounds;
            this.matrix = matrix;
            SetInStartPosition();
        }

        public void Update()
        {
            motion = Vector2.Zero;

            TouchCollection touchCollection = TouchPanel.GetState();
            if ( touchCollection.Count > 0 )
            {
                Vector2 pos = Vector2.Transform(touchCollection[0].Position, matrix);
                motion.X = pos.X - position.X;
                if ( motion.X >= 15 && motion.X <= 75 ) motion.X = 0;
                else if (motion.X > 0) motion.X = 1;
                else if (motion.X < 0) motion.X = -1;
                else motion.X = 0;
            }

            motion.X *= paddleSpeed;
            position += motion;
            LockPaddle();
        }

        private void LockPaddle()
        {
            if (position.X < 0)
            {
                position.X = 0;
            }
            if (position.X + texture.Width > screenBounds.Width)
            {
                position.X = screenBounds.Width - texture.Width;
            }
        }

        public void SetInStartPosition()
        {
            position.X = (screenBounds.Width - texture.Width) / 2;
            position.Y = screenBounds.Height - texture.Height - 50;
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
