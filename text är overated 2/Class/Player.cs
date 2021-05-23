using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework.Input;
using System.Text;
using System.Threading.Tasks;

namespace text_är_overated_2.Class
{
    public class Player : Component
    {
        public Projectile Projectile;

        private Texture2D PlayerTexture;

        public Color PlayerColor = Color.White;

        public Rectangle PlayerRect
        {
            get
            {
                return new Rectangle((int)Position.X, (int)Position.Y, 95, 100);
            }
        }

        public Player(Texture2D texture)
        {
            PlayerTexture = texture;
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(PlayerTexture, PlayerRect, PlayerColor);
        }

        public override void Update(GameTime gameTime, List<Component> components)
        {
            Direction = new Vector2((float)Math.Cos(Rotation), (float)Math.Sin(Rotation));

            Toggle1 = Toggle2;
            Toggle2 = Keyboard.GetState();

            if (Toggle1.IsKeyDown(Keys.Space) && Toggle2.IsKeyUp(Keys.Space))
            {
                ProjectileAdd(components);
            }

        }

        private void ProjectileAdd(List<Component> components)
        {
            var projectile = Projectile.Clone() as Projectile;
            projectile.Direction = this.Direction;
            projectile.Position = this.Position;
            projectile.LinearVelocity = this.LinearVelocity * 2;
            projectile.LifeSpan = 2f;
            projectile.Parent = this;

            components.Add(projectile);
        }

        
    }
}
