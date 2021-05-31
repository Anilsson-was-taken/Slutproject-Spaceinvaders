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
        public float LifeSpan = 0f;

        public bool IsRemoved = false;

        protected KeyboardState Toggle1;

        protected KeyboardState Toggle2;

        public Vector2 Position;

        public Vector2 Start;

        public Vector2 LeftStart;

        public Vector2 RightStart;

        public int xOffset;

        protected Texture2D BaseTexture;

        public Component(Texture2D texture)
        {
            BaseTexture = texture;
            Start = new Vector2(BaseTexture.Width / 2, BaseTexture.Height / 2);
        }

        public virtual void Update(GameTime gameTime, List<Component> GameComponents)
        {

        }


        public virtual void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(BaseTexture, Position, null, Color.White, 0, Start, 0.20f, SpriteEffects.None, 0);
        }

        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }
}
