using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Threading.Tasks;
using text_är_overated_2.Class;

namespace text_är_overated_2
{
    class Player : Component
    {
        public Projectile Projectile;
        private Player playerTexture;

        public Player(Texture2D texture)
            : base(texture) 
        {

        }

        public override void Update(GameTime gameTime, List<Component> GameComponents) 
        {
            Toggle1 = Toggle2;
            Toggle2 = Keyboard.GetState();

            if (Keyboard.GetState().IsKeyDown(Keys.A)) 
            {
                Position.X -= 7;
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.D))
            {
                Position.X += 7;
            }

            if (Toggle1.IsKeyDown(Keys.Space) && Toggle2.IsKeyUp(Keys.Space))
            {
                AddProjectileLeft(GameComponents);
                AddProjectileRight(GameComponents);
            }
        }

        private void AddProjectileRight(List<Component> GameComponents)
        {
            var projectile = Projectile.Clone() as Projectile;
            projectile.LifeSpan = 5f;
            xOffset = 42;
            LeftStart = new Vector2(xOffset, -42);
            projectile.Position = this.Position + LeftStart;

            GameComponents.Add(projectile);
        }

        private void AddProjectileLeft(List<Component> GameComponents)
        {
            var projectile = Projectile.Clone() as Projectile;
            projectile.LifeSpan = 5f;
            xOffset = 45;
            LeftStart = new Vector2(xOffset, 45);
            projectile.Position = this.Position - LeftStart;

            GameComponents.Add(projectile);
        }




    }
}