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
    class Block
    {
        Vector2 position;
        Texture2D texture;
        Rectangle bound;

        public Block(Texture2D texture, Vector2 position)
        {
            this.texture = texture;
            this.position = position;
            bound = new Rectangle(
                                (int)position.X,
                                (int)position.Y,
                                texture.Width,
                                texture.Height);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, position, Color.White);
        }

        public bool BallCollision(Rectangle ballLocation)
        {
            Rectangle blockLocation = new Rectangle(
                                                    (int)position.X,
                                                    (int)position.Y,
                                                    texture.Width,
                                                    texture.Height);
            if (ballLocation.Intersects(blockLocation))
            {
                return true;
            }
            return false;
        }

        public Rectangle GetBounds()
        {
            return bound;
        }
    }
}
