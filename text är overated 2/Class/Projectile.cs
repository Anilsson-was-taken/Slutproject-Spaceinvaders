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

        private float timer;


        public Color ProjectileColor { get; set; }

        public Rectangle ProjectileRect
        {
            get
            {
                return new Rectangle((int)Position.X, (int)Position.Y, 30, 58);
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

        public override void Update(GameTime gameTime, List<Component> components)
        {
            timer += (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (timer > LifeSpan)
            {
                IsRemoved = true;
            }

            Position += Direction * LinearVelocity;
        }
    }
}
