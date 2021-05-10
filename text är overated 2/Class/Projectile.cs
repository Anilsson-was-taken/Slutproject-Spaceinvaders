using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace text_är_overated_2.Class
{
    public class Projectile : Component
    {
        private Texture2D ProjectileTexture;

        public Vector2 PPosition { get; set; }

        public Color ProjectileColor { get; set; }

        public Rectangle ProjectileRect
        {
            get
            {
                return new Rectangle((int)PPosition.X, (int)PPosition.Y, 30, 58);
                //return new Rectangle((int)PPosition.X, (int)PPosition.Y, ProjectileTexture.Width, ProjectileTexture.Height);
            }
        }

        public Projectile(Texture2D texture)
        {
            ProjectileTexture = texture;
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(ProjectileTexture, ProjectileRect, ProjectileColor);
        }

        private KeyboardState kstate = Keyboard.GetState();
        int SpacebarToggle1 = 0;
        int SpacebarToggle2 = 0;
        public override void Update(GameTime gameTime)
        {
            if(kstate.IsKeyDown(Keys.Space) && SpacebarToggle1 == 0)
            {

                SpacebarToggle2 = 1;
            }
            else if (kstate.IsKeyUp(Keys.Space) && SpacebarToggle2 == 1)
            {

                SpacebarToggle1 = 1;
            }
        }
    }
}
