using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System.Net.Configuration;
using System.Threading;
using Microsoft.Xna.Framework.Input;
using System;

namespace Spaceinvder_Project
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        private Color _backgroundColour = Color.Black;



        Rectangle PlayerRect1 = new Rectangle(350, 650, 95, 100);
        Rectangle PlayerRect2 = new Rectangle(350, 650, 95, 100);
        Rectangle PlayerRect3 = new Rectangle(350, 650, 95, 100);

        public List<Rectangle> PlayerList = new List<Rectangle>();

        Rectangle beamtest = new Rectangle(400, 225, 40, 40);
        Rectangle GameBackgroundRect1 = new Rectangle(0, 0, 1366, 786);
        Rectangle GameBackgroundRect2 = new Rectangle(0, -786, 1366, 786);
        Rectangle BackgroundMenuRect = new Rectangle(0, 0, 1366, 768);
        Rectangle CursorRect = new Rectangle(400, 200, 75, 75);


        private Texture2D BeamT;
        private Texture2D PlayerTexture1;
        private Texture2D PlayerTexture2;
        private Texture2D PlayerTexture3;
        private Texture2D GameBackgroundTexture1;
        private Texture2D GameBackgroundTexture2;
        private Texture2D BacgroundMenuTexture;
        private Texture2D CursorTexture;

        private SpriteFont font;




        int TextureChange = 0;
        int BackgroundIndex = 0;





        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            IsMouseVisible = false;
            graphics.IsFullScreen = true;
            graphics.PreferredBackBufferWidth = 1366;
            graphics.PreferredBackBufferHeight = 786;

            PlayerList.Add(PlayerRect1);
            PlayerList.Add(PlayerRect2);
            PlayerList.Add(PlayerRect3);
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
            PlayerTexture1 = Content.Load<Texture2D>("Textures/player1");
            BeamT = Content.Load<Texture2D>("Textures/laser beam");
            PlayerTexture2 = Content.Load<Texture2D>("Textures/player2");
            PlayerTexture3 = Content.Load<Texture2D>("Textures/player3");
            GameBackgroundTexture1 = Content.Load<Texture2D>("Backgrounds/Base background");
            GameBackgroundTexture2 = Content.Load<Texture2D>("Backgrounds/Base background2");
            BacgroundMenuTexture = Content.Load<Texture2D>("Backgrounds/backgroundmenu");
            CursorTexture = Content.Load<Texture2D>("Textures/pek pil");
            font = Content.Load<SpriteFont>("Font");


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

            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here


            TextureChange++;
            if (TextureChange >= 80)
            {
                TextureChange = 0;
            }


            base.Update(gameTime);
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

            if (BackgroundIndex == 0)
            {
                spriteBatch.Draw(GameBackgroundTexture1, GameBackgroundRect1, Color.White);
                spriteBatch.Draw(GameBackgroundTexture2, GameBackgroundRect2, Color.White);
                spriteBatch.Draw(BacgroundMenuTexture, BackgroundMenuRect, Color.White);
                spriteBatch.Draw(CursorTexture, CursorRect, Color.White);

                if (GameBackgroundRect1.Y <= 786)
                {
                    GameBackgroundRect1.Y += 3;
                }
                else if (GameBackgroundRect1.Y >= 786)
                {
                    GameBackgroundRect1.Y = -779;
                }

                if (GameBackgroundRect2.Y <= 786)
                {
                    GameBackgroundRect2.Y += 3;
                }
                else if (GameBackgroundRect2.Y >= 786)
                {
                    GameBackgroundRect2.Y = -779;
                }

                spriteBatch.DrawString(font, "Start", new Vector2(100, 100), Color.White);
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
                //spriteBatch.Draw(BeamT, beamtest, null, Color.White, MathHelper.PiOver2, new Vector2(0, 0), SpriteEffects.None, 0);

            }

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
