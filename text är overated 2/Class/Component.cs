using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Input;

namespace text_är_overated_2
{
    public class Component : ICloneable
    {
        //Icloeable gör så jag kan kolna skotten i player klassen

        public float LifeSpan = 0f;

        public bool IsRemoved = false;

        protected KeyboardState Toggle1;

        protected KeyboardState Toggle2;

        public Vector2 Position;

        public Vector2 Start;

        public Vector2 LeftStart;

        public int xOffset;

        protected Texture2D BaseTexture;

        public float Speed;

        public Texture2D EnemyTexture;

        //ger mig bastextyren som jag använder i alla klasser, har igenom 2 på start då hade mitt första skott i mitten, årkade inte bara ändra det
        public Component(Texture2D texture)
        {
            BaseTexture = texture;
            Start = new Vector2(1366 / 2, 700);
        }

        //virtual så den används i andra klasser
        public virtual void Update(GameTime gameTime, List<Component> GameComponents)
        {

        }

        //har en postion för varje klass individuelt med samma variabel (Position). Skappar en rectangle (en hitbox) för varje klass med position y och x igenom..
        //5 och 5 då den är 0.20f i drwa funktion. Den anänvder klassens texture och position och skaper en individuel rectangle
        //med samma namn: rectangle. så man kan bara använda hitboxen i just klassen vilket gör en extremet fin och optimaserad kod med det tog typ 2h att få
        //allt att funka. detta är en get och return metod
        public Rectangle Rectangle
        {
            get
            {
                return new Rectangle((int)Position.X, (int)Position.Y, BaseTexture.Width/5, BaseTexture.Height/5);
            }
        }

        //virtual så den används i all andra, bas texturn för alla, postion för alla, null då det fungerar, färg, 0(rotation) start vilket är start postion,..
        //0.2f = ner skalat 5 gånger inga extra effekter, 0 eftersom det fungerar (vet inte vad det är)
        public virtual void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(BaseTexture, Position, null, Color.White, 0, Start, 0.20f, SpriteEffects.None, 0);
        }

        //kan clonas och returnas
        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }
}
