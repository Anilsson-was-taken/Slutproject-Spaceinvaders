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
    public abstract class Component : ICloneable
    {
        public abstract void Draw(GameTime gameTime, SpriteBatch spriteBatch);

        public abstract void Update(GameTime gameTime, List<Component> components);


        public float LifeSpan = 0f;

        public bool IsRemoved = false;

        public float LinearVelocity = 4f;

        public KeyboardState Toggle1;

        public KeyboardState Toggle2;

        public Vector2 Position;

        public Vector2 Direction;

        public Component Parent;

        public float Rotation;

        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }
}
