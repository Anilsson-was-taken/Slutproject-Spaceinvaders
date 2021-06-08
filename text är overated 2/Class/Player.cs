using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Threading.Tasks;
using text_är_overated_2.Class;
using Spaceinvder_Project;

namespace text_är_overated_2
{
    class Player : Component
    {
        public Projectile Projectile;

        public bool Dead = false;

        private int CountDelay = 0;

        private int Delay = 0;

        //base texture som är en byggd av en konstruktur ur klassen components vilket du ser att jag använde ifall du tittar på rad 14
        public Player(Texture2D texture)
            : base(texture) 
        {
            Position.X = Position.X - 20;
        }

        public override void Update(GameTime gameTime, List<Component> GameComponents) 
        {
            //toggle knapp
            Toggle1 = Toggle2;
            Toggle2 = Keyboard.GetState();

            //player speed höger vänster med keys
            if (Keyboard.GetState().IsKeyDown(Keys.A)) 
            {
                Position.X -= 7;
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.D))
            {
                Position.X += 7;
            }

            //delay på hur många skott jag kan skuta, den räknar skotten sen ifall sätter en delay efter 3 skott
            if(CountDelay >= 3) 
            {
                Delay++;
                if(Delay >= 60) 
                {
                    CountDelay = 0;
                    Delay = 0;
                }
            }

            //toggle knap med skott delay (reloud time)
            if (Toggle1.IsKeyDown(Keys.Space) && Toggle2.IsKeyUp(Keys.Space) && CountDelay < 3)
            {
                AddProjectileLeft(GameComponents);
                AddProjectileRight(GameComponents);
                CountDelay++;
            }

            //så player inte kan åka ut ur banan
            if (Position.X > 1366 + 20) 
            {
                Position.X = 1366 + 20;
            }
            else if (Position.X < 135) 
            {
                Position.X = 135;
            }


            //ifall component är en spelare så fortsätter, ifall rectangle(spelar ingen roll vilken men pga att skeppets skott är så långt ut kan bara..
            //metioriterna(enemy) interacta med skeppet) så är är bool Dead = true.
            foreach (var componet in GameComponents) 
            {
                if(componet is Player) 
                {
                    continue;
                }
                
                if (componet.Rectangle.Intersects(this.Rectangle))
                {
                    //dead = true för så att jag kan reseta med reset metoden i game1. this är där för att det fungerade efter många tester
                    this.Dead = true;
                }
            }
        }

        //två metoder nästan idenstiska (bara vänster och höger skott). först clonar metoden en projectile some en projectile. den indikerar lifespan vilekt gör.. 
        //att i projectile klassen kan skotte försvinna efter tiden 5f. xoffet behövs pga vänster höger skott. Orkade inte byta namnet leftstart men den det är..
        //en vektor2. postiionen av projektilen = this.position(skeppets positoin) + och - leftstart vilket ger höger vänster skott
        private void AddProjectileRight(List<Component> GameComponents)
        {
            var projectile = Projectile.Clone() as Projectile;
            projectile.LifeSpan = 5f;
            xOffset = 8;
            LeftStart = new Vector2(xOffset, -30);
            projectile.Position = this.Position + LeftStart;

            GameComponents.Add(projectile);
        }

        private void AddProjectileLeft(List<Component> GameComponents)
        {
            var projectile = Projectile.Clone() as Projectile;
            projectile.LifeSpan = 5f;
            xOffset = -98;
            LeftStart = new Vector2(xOffset, 30);
            projectile.Position = this.Position - LeftStart;

            GameComponents.Add(projectile);
        }

    }
}