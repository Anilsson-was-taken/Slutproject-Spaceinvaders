using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using text_är_overated_2.Class;
using text_är_overated_2;

namespace Spaceinvder_Project
{

    public class Game1 : Game
    {
        readonly GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        private List<Component> GameComponents;

        Rectangle GameBackgroundRect1 = new Rectangle(0, 0, 1366, 786);
        Rectangle GameBackgroundRect2 = new Rectangle(0, -786, 1366, 786);
        Rectangle BackgroundMenuRect = new Rectangle(0, 0, 1366, 768);
        Rectangle CursorRect = new Rectangle(400, 215, 50, 50);

        private Song LevanPolka;

        
        private Texture2D GameBackgroundTexture1;
        private Texture2D GameBackgroundTexture2;
        private Texture2D BacgroundMenuTexture;
        private Texture2D CursorTexture;

        private SpriteFont Font;

        private Player Player1;
        private Player Player2;
        private Player Player3;


        int TextureChange = 0;
        int BackgroundIndex = 0;
        int menyTime = 0;
        int menyvalue = 0;
        //int level = 1

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            IsMouseVisible = false;
            graphics.IsFullScreen = true;
            graphics.PreferredBackBufferWidth = 1366;
            graphics.PreferredBackBufferHeight = 786;
        }


        protected override void Initialize()
        {


            IsMouseVisible = false;


            base.Initialize();
        }

 
        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            GameBackgroundTexture1 = Content.Load<Texture2D>("Backgrounds/Base background");
            GameBackgroundTexture2 = Content.Load<Texture2D>("Backgrounds/Base background2");
            BacgroundMenuTexture = Content.Load<Texture2D>("Backgrounds/backgroundmenu");
            CursorTexture = Content.Load<Texture2D>("Textures/pek pil");
            Font = Content.Load<SpriteFont>("Font");
            LevanPolka = Content.Load<Song>("Sound/levan");
            MediaPlayer.Play(LevanPolka);
            MediaPlayer.IsRepeating = true;

            var playerTexture1 = Content.Load<Texture2D>("Textures/player1");
            var playerTexture3 = Content.Load<Texture2D>("Textures/player3");
            var playerTexture2 = Content.Load<Texture2D>("Textures/player2");

            GameComponents = new List<Component>()
            {
                new Player(playerTexture1)
                {
                    Position = new Vector2(500, 700),
                    Projectile = new Projectile(Content.Load<Texture2D>("Textures/projectile")),
                },
            };
            
        }


        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }


        protected override void Update(GameTime gameTime)
        {
            foreach (var component in GameComponents.ToArray())
            {
                component.Update(gameTime, GameComponents);
            }

            IsRemovedMethod();

            base.Update(gameTime);

            
        }

        private void IsRemovedMethod()
        {
            for (int i = 0; i < Components.Count; i++)
            {
                if (GameComponents[i].IsRemoved)
                {
                    GameComponents.RemoveAt(i);
                    i--;
                }
            }
        }







        protected override void Draw(GameTime gameTime)
        {

            GraphicsDevice.Clear(Color.Black);
            
            spriteBatch.Begin();

            
            KeyboardState kstate = Keyboard.GetState();

            if (kstate.IsKeyDown(Keys.R))
                Exit();

            
            spriteBatch.Draw(GameBackgroundTexture1, GameBackgroundRect1, Color.White);
            spriteBatch.Draw(GameBackgroundTexture2, GameBackgroundRect2, Color.White);

            if (GameBackgroundRect1.Y <= 786)
            {
                GameBackgroundRect1.Y += 6;
            }
            else if (GameBackgroundRect1.Y >= 786)
            {
                GameBackgroundRect1.Y = -779;
            }

            if (GameBackgroundRect2.Y <= 786)
            {
                GameBackgroundRect2.Y += 6;
            }
            else if (GameBackgroundRect2.Y >= 786)
            {
                GameBackgroundRect2.Y = -779;
            }

            
            if (kstate.IsKeyUp(Keys.W) && kstate.IsKeyUp(Keys.S) && kstate.IsKeyUp(Keys.Enter))
            {
                menyTime = 0;
            }
            else if (menyTime >= 30)
            {
                menyTime = 0;
            }
            else
            {
                menyTime++;
            }

            if (CursorRect.Y == 210)
            {
                menyvalue = 0;
            }
            else if (CursorRect.Y == 360)
            {
                menyvalue = 1;
            }
            else if (CursorRect.Y == 510)
            {
                menyvalue = 2;
            }

            if (kstate.IsKeyDown(Keys.S) && menyvalue == 2 && menyTime < 20)
            {
                CursorRect.X = 400;
                CursorRect.Y = 210;
                menyTime = 20;
            }
            else if (kstate.IsKeyDown(Keys.S) && menyvalue == 0 && menyTime < 20)
            {
                CursorRect.X = 400;
                CursorRect.Y = 360;
                menyTime = 20;

            }
            else if (kstate.IsKeyDown(Keys.S) && menyvalue == 1 && menyTime < 20)
            {
                CursorRect.X = 400;
                CursorRect.Y = 510;
                menyTime = 20;
            }
            else if (kstate.IsKeyDown(Keys.W) && menyvalue == 2 && menyTime < 20)
            {
                CursorRect.X = 400;
                CursorRect.Y = 360;
                menyTime = 20;
            }
            else if (kstate.IsKeyDown(Keys.W) && menyvalue == 0 && menyTime < 20)
            {
                CursorRect.X = 400;
                CursorRect.Y = 510;
                menyTime = 20;

            }
            else if (kstate.IsKeyDown(Keys.W) && menyvalue == 1 && menyTime < 20)
            {
                CursorRect.X = 400;
                CursorRect.Y = 210;
                menyTime = 20;
            }

            if (BackgroundIndex == 0)
            {
                spriteBatch.Draw(CursorTexture, CursorRect, Color.White);
                spriteBatch.Draw(BacgroundMenuTexture, BackgroundMenuRect, Color.White);
                if (kstate.IsKeyDown(Keys.Enter) && menyvalue == 0 && menyTime < 20)
                {
                    BackgroundIndex = 1;
                }
                else if (kstate.IsKeyDown(Keys.Enter) && menyvalue == 2 && menyTime < 20)
                {
                    Exit();
                }
                //else if ((kstate.IsKeyDown(Keys.Enter) && menyvalue == 1)


                spriteBatch.DrawString(Font, "Start Game", new Vector2(500, 200), Color.White);
                spriteBatch.DrawString(Font, "Controls", new Vector2(540, 350), Color.White);
                spriteBatch.DrawString(Font, "Exit", new Vector2(600, 500), Color.White);
            }
            else if (kstate.IsKeyDown(Keys.Escape) && BackgroundIndex == 1)
            {
                BackgroundIndex = 2;
            }
            else if (BackgroundIndex == 2)
            {
                spriteBatch.Draw(CursorTexture, CursorRect, Color.White);
                spriteBatch.Draw(BacgroundMenuTexture, BackgroundMenuRect, Color.White);

                if (kstate.IsKeyDown(Keys.Enter) && menyvalue == 0)
                {
                    BackgroundIndex = 1;
                }
                else if (kstate.IsKeyDown(Keys.Enter) && menyvalue == 2)
                {
                    menyTime = 20;
                    BackgroundIndex = 0;
                    CursorRect.Y = 210;
                    menyvalue = 0;

                }
                //else if ((kstate.IsKeyDown(Keys.Enter) && menyvalue == 1)

                

                spriteBatch.DrawString(Font, "Continue Game", new Vector2(460, 200), Color.White);
                spriteBatch.DrawString(Font, "Controls", new Vector2(540, 350), Color.White);
                spriteBatch.DrawString(Font, "Exit to menu", new Vector2(490, 500), Color.White);
            }
            else if (BackgroundIndex == 1)
            {



                foreach (var component in GameComponents)
                {
                    component.Draw(gameTime, spriteBatch);
                }

            }


            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}