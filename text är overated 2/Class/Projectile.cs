﻿using System;
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
        //player projectile och enemy anänver alla component och public override update. jag gör ett skellet i component och stoppar in den i andra klasser..
        //update barar overidar component och det är också därför som dom är virtual i update.
        public Projectile(Texture2D texture)
            : base(texture) 
        {
            
        }
        

        public override void Update(GameTime gameTime, List<Component> GameComponents)
        {
            //timer som dödar skottet i player klassen efter 5 sekunder
            timer += (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (timer >= LifeSpan)
            {
                IsRemoved = true;
            }
            //hastigheten av skottet
            Position.Y -= 5;

            //ungefär lika dan som i player. Ifall det är ett skott och det interactar (vilket den bara kan med enemy) så blir boolen isremoved = true..
            //som leder till att skottet tars bort ur GameComponents i game1
            foreach (var componet in GameComponents)
            {
                if (componet is Projectile)
                {
                    continue;
                }

                if (componet.Rectangle.Intersects(this.Rectangle))
                {
                    IsRemoved = true;
                }
            }
        }
    }

}
