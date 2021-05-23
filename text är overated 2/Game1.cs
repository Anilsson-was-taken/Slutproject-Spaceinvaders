using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System.Net.Configuration;
using System.Threading;
using Microsoft.Xna.Framework.Input;
using System;
using Microsoft.Xna.Framework.Media;
using text_är_overated_2.Class;
using text_är_overated_2;
using System.Threading.Tasks;


namespace Spaceinvder_Project
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        readonly GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        private List<Component> GameComponents;

        Rectangle PlayerRect1 = new Rectangle(350, 650, 95, 100);
        Rectangle PlayerRect2 = new Rectangle(350, 650, 95, 100);
        Rectangle PlayerRect3 = new Rectangle(350, 650, 95, 100);

        Rectangle GameBackgroundRect1 = new Rectangle(0, 0, 1366, 786);
        Rectangle GameBackgroundRect2 = new Rectangle(0, -786, 1366, 786);
        Rectangle BackgroundMenuRect = new Rectangle(0, 0, 1366, 768);
        Rectangle CursorRect = new Rectangle(400, 215, 50, 50);

        private Song LevanPolka;

        private Texture2D PlayerTexture1;
        private Texture2D PlayerTexture2;
        private Texture2D PlayerTexture3;
        private Texture2D GameBackgroundTexture1;
        private Texture2D GameBackgroundTexture2;
        private Texture2D BacgroundMenuTexture;
        private Texture2D CursorTexture;

        private SpriteFont Font;




        int TextureChange = 0;
        int BackgroundIndex = 0;
        int menyTime = 0;
        int menyvalue = 0;
        int level = 1;
        //int SpacebarToggle1 = 0;
        //int SpacebarToggle2 = 0;



        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            IsMouseVisible = false;
            graphics.IsFullScreen = true;
            graphics.PreferredBackBufferWidth = 1366;
            graphics.PreferredBackBufferHeight = 786;
        }

        public Game1(GraphicsDeviceManager graphics)
        {
            this.graphics = graphics;
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            IsMouseVisible = false;


            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            KeyboardState kstate = Keyboard.GetState();

            PlayerTexture1 = Content.Load<Texture2D>("Textures/player1");
            PlayerTexture2 = Content.Load<Texture2D>("Textures/player2");
            PlayerTexture3 = Content.Load<Texture2D>("Textures/player3");
            GameBackgroundTexture1 = Content.Load<Texture2D>("Backgrounds/Base background");
            GameBackgroundTexture2 = Content.Load<Texture2D>("Backgrounds/Base background2");
            BacgroundMenuTexture = Content.Load<Texture2D>("Backgrounds/backgroundmenu");
            CursorTexture = Content.Load<Texture2D>("Textures/pek pil");
            Font = Content.Load<SpriteFont>("Font");
            LevanPolka = Content.Load<Song>("Sound/levan");
            MediaPlayer.Play(LevanPolka);
            MediaPlayer.IsRepeating = true;

            Projectile ProjectileLeft = new Projectile(Content.Load<Texture2D>("Textures/projectile"))
            {
                Position = new Vector2(350, 500),
                ProjectileColor = Color.White

            };
            Projectile ProjectileRight = new Projectile(Content.Load<Texture2D>("Textures/projectile"))
            {
                Position = new Vector2(375, 500),
                ProjectileColor = Color.White
            };
            Player Player1 = new Player(Content.Load<Texture2D>("Textures/player1"))
            {
                Position = new Vector2(200, 200),
            };
            Player Player2 = new Player(Content.Load<Texture2D>("Textures/player2"))
            {
                Position = new Vector2(200, 300),
            };
            Player Player3 = new Player(Content.Load<Texture2D>("Textures/player3"))
            {
                Position = new Vector2(200, 400),
            };

            GameComponents = new List<Component>
            {
                Player1,
                Player2,
                Player3,
                ProjectileLeft,
                ProjectileRight
            };


            /*if (BackgroundIndex == 1 && kstate.IsKeyUp(Keys.Space) && SpacebarToggle2 == 1)
            {
                ProjectileLeft.PPosition.Y += 3;
            }*/
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            foreach(var component in GameComponents.ToArray())
            {
                component.Update(gameTime, GameComponents);
            }


            for (int i = 0; i < Components.Count; i++)
            {
                if (GameComponents[i].IsRemoved)
                {
                    Components.RemoveAt(i);
                    i--;
                }
            }
            
            base.Update(gameTime);

            // TODO: Add your update logic here


            TextureChange++;
            if (TextureChange >= 80)
            {
                TextureChange = 0;
            }
        }




            
        

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {

            // TODO: Add your drawing code here
            spriteBatch.Begin();

            KeyboardState kstate = Keyboard.GetState();

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

            if (kstate.IsKeyDown(Keys.R))
                Exit();

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

                if (kstate.IsKeyDown(Keys.D))
                {
                    PlayerRect1.X += 7;
                    PlayerRect2.X += 7;
                    PlayerRect3.X += 7;
                }
                else if (kstate.IsKeyDown(Keys.A))
                {
                    PlayerRect1.X -= 7;
                    PlayerRect2.X -= 7;
                    PlayerRect3.X -= 7;
                }

                if (PlayerRect1.X <= -100 || PlayerRect2.X <= -100 || PlayerRect3.X <= -100)
                {
                    PlayerRect1.X = 1465;
                    PlayerRect2.X = 1465;
                    PlayerRect3.X = 1465;
                }
                if (PlayerRect1.X >= 1466 || PlayerRect2.X >= 1466 || PlayerRect3.X >= 1466)
                {
                    PlayerRect1.X = -100;
                    PlayerRect2.X = -100;
                    PlayerRect3.X = -100;
                }

                if (TextureChange < 20)
                {
                    spriteBatch.Draw(PlayerTexture1, PlayerRect1, Color.White);
                }
                else if (TextureChange < 40)
                {
                    spriteBatch.Draw(PlayerTexture2, PlayerRect2, Color.White);
                }
                else if (TextureChange < 60)
                {
                    spriteBatch.Draw(PlayerTexture3, PlayerRect3, Color.White);
                }
                else if (TextureChange < 80)    
                {
                    spriteBatch.Draw(PlayerTexture2, PlayerRect2, Color.White);
                }

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