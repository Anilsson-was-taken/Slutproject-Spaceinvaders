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
        private float timer;

        public Projectile(Texture2D texture)
            : base(texture) 
        {
            
        }
        

        public override void Update(GameTime gameTime, List<Component> GameComponents)
        {

            timer += (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (timer >= LifeSpan)
            {
                IsRemoved = true;
            }

            Position.Y -= 5;
        }
    }

}
