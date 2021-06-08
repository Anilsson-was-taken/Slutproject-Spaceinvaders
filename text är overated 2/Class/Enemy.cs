using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Spaceinvder_Project;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using text_är_overated_2.Class;
using System.Threading.Tasks;

namespace text_är_overated_2.Class
{
    public class Enemy : Component
    {
        //samma som projectile med texture
        public Enemy(Texture2D texture) 
            : base(texture)
        {
            //sätter att y posionen alltid börjar på 0 och x är randm mellan skärmen (0-1366)
            Position = new Vector2(Game1.Random.Next(135, Game1.BufferWidth+60), -BaseTexture.Height);
            //random speed mellan 7 och 15, 7 för att backgrunden går med speden 6 vilket för det fult ifall jag tar mindre än 7 och 15 för balanseringen
            Speed = Game1.Random.Next(7, 15);
        }

        public override void Update(GameTime gameTime, List<Component> GameComponents) 
        {
            //hastighet
            Position.Y += Speed;

            //ifall den träffar botten försviner den medhjälp av isremoved boolen som tar bort ur listan i game1
            if(Rectangle.Bottom >= Game1.BufferHeight + 200) 
            {
                IsRemoved = true;
            }

            //ifall componenten i listan är en enemyklass forssätt, ifall den reagerar med en hitbox som inte är player så försvinner den
            //!(componet is Player)) tog en halv timme, utan den så kunda playerna ibland bara ta ett skott och inte bry sig men nu indikerar jag att..
            // metoriten inte reagerar med player utan att player reagerar med metoriten (i player klassen)
            foreach (var componet in GameComponents)
            {
                if (componet is Enemy)
                {
                    continue;
                }

                if (componet.Rectangle.Intersects(this.Rectangle) && !(componet is Player))
                {
                    //tar bort på samma sätt som tidigare
                    IsRemoved = true;
                }
            }
        }
    }
}
