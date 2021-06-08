using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using text_är_overated_2.Class;
using text_är_overated_2;
using System;

namespace Spaceinvder_Project
{

    public class Game1 : Game
    {
        readonly GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        public static Random Random;

        private int timer;

        public static int BufferWidth;
        public static int BufferHeight;

        private bool GameStarted = false;

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

        int BackgroundIndex = 0;
        int menyTime = 0;
        int menyvalue = 0;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            IsMouseVisible = false;
            graphics.IsFullScreen = true;
            graphics.PreferredBackBufferWidth = 1366;
            graphics.PreferredBackBufferHeight = 786;

            Random = new Random();

            BufferHeight = graphics.PreferredBackBufferHeight;
            BufferWidth = graphics.PreferredBackBufferWidth;

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
        }

        private void Reset()
        {
            //Reset metoden används både som start metod men också som metoden som resetar ifall man förlorar den går ut på att man skapar en lista ur klassen..
            //components och därefter medger positionen som är skärmen igenom 2 och att man skapar en ny projectile (skott) och indikerar texturn.
            //Gamestarted är en bool som används för att få en paus imellan man dött eller när man precist börjat spelet.
            var playerTexture = Content.Load<Texture2D>("Textures/player1.1");

            GameComponents = new List<Component>()
            {
                new Player(playerTexture)
                {
                    Position = new Vector2(BufferWidth/2, 750),
                    Projectile = new Projectile(Content.Load<Texture2D>("Textures/projectilee")),
                },
            };
            GameStarted = false;
        }

        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }


        protected override void Update(GameTime gameTime)
        {
            KeyboardState kstate = Keyboard.GetState();
            //menyTime är min delay int som anänds väldigt ofta, ifall man t.ex håller in space så kunde man tidigar gå igenom hela menyn vilket inte är optimalt
            //menyTime går ut på att reseta när den är 30 och då är 30 delayn
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

            //startar spelet ifall enter är nedtryckt, scenen är spelet (med delay)
            if (kstate.IsKeyDown(Keys.Enter) && BackgroundIndex == 1 && menyTime < 20) 
            {
                GameStarted = true;
            }
            //gör så inte spelet starts av sig själv
            if (!GameStarted)
            {
                return;
            }

            //timer är en timer som jag använder för få ut metioriter vilket får en delay av 5
            timer++;
            foreach (var component in GameComponents.ToArray())
            {
                component.Update(gameTime, GameComponents);
            }

            //backgroundindex är en int för vilken scen man är i. 0 = huvud meny 1 = spelet 2 = meny i spelet 3 = controls meny
            //när delay > 5 background index == 1 och spelet har startat skapas metoriterna
            if (timer > 3 && BackgroundIndex == 1 && GameStarted == true)
            {
                timer = 0;
                GameComponents.Add(new Enemy(Content.Load<Texture2D>("Textures/Metiorite2")));
            }

            //Detta är en for loop som tog ca 2 timmar. den går ut på att ta bort components ur min lista, components är både enemys och skott
            for (int i = 0; i < GameComponents.Count; i++)
            {
                //isremoved = true så åker det in hit vilket tar bort den från listan, med andra ord tar bort den helt
                var component = GameComponents[i];

                if (component.IsRemoved)
                {
                    GameComponents.RemoveAt(i);
                    i--;
                }
                //ifall component är en seplare och är död så linkar den till reset metoden vilket resetar spelet (du ser hur bool blir true i player klassen)
                if (component is Player)
                {
                    var player = component as Player;
                    if (player.Dead)
                    {
                        Reset();
                    }
                }
            }

            base.Update(gameTime);

        }


        protected override void Draw(GameTime gameTime)
        {

            GraphicsDevice.Clear(Color.Black);
            
            spriteBatch.Begin();

            
            KeyboardState kstate = Keyboard.GetState();

            //force exit knapp ifall spelet skulle krasha i develpoe tillståndet
            if (kstate.IsKeyDown(Keys.R))
                Exit();

            //2 identiska bakgrundsbilder
            spriteBatch.Draw(GameBackgroundTexture1, GameBackgroundRect1, Color.White);
            spriteBatch.Draw(GameBackgroundTexture2, GameBackgroundRect2, Color.White);

            //if sats som ger de 2 bakgrunderna samma hastighet och teleporterar en efter en vid en viss punkt vilket ger en illusion av att bakgrunden rör..
            //på sig, tänkta att detta, scener(backgroundindex) och min cursor skulle vara klasser men hade inte tid
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

            
            
            //min universiela kod för min cursor då placeringen längst up = 0, mitten = 1 och längst ner = 2
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
            //movment av min cursor och delay med hjälp av menytime
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
            //backgroundindex == 0 är att vi är i huvudmenyn
            if (BackgroundIndex == 0)
            {
                spriteBatch.Draw(CursorTexture, CursorRect, Color.White);
                spriteBatch.Draw(BacgroundMenuTexture, BackgroundMenuRect, Color.White);
                if (kstate.IsKeyDown(Keys.Enter) && menyvalue == 0 && menyTime < 20)
                {
                    //ifall vi är på positionen 1 och clickar enter med delay så resetar vi (i första fallet statar vi) med hjälp av metoden reset detta gör..
                    //så att jag både kan ha continue game (Defualt) och start new game med reset metoden
                    BackgroundIndex = 1;
                    Reset();
                    menyTime = 20;
                }
                else if (kstate.IsKeyDown(Keys.Enter) && menyvalue == 2 && menyTime < 20)
                {
                    Exit();
                }
                else if ((kstate.IsKeyDown(Keys.Enter) && menyvalue == 1 && menyTime < 20) && BackgroundIndex == 0)
                {
                    BackgroundIndex = 3;
                    //byter scen till 3 vilket är controls
                    menyTime = 20;
                }


                spriteBatch.DrawString(Font, "Start Game", new Vector2(500, 200), Color.White);
                spriteBatch.DrawString(Font, "Controls", new Vector2(540, 350), Color.White);
                spriteBatch.DrawString(Font, "Exit", new Vector2(600, 500), Color.White);
            }
            //backgroundidex == 1 och esc är att spele menyn kommer upp (allt som har med escape att göra har delay så jag tänker inte nämna det längre)
            else if (kstate.IsKeyDown(Keys.Escape) && BackgroundIndex == 1)
            {
                BackgroundIndex = 2;
                menyTime = 20;
            }
            //backgroundindex 2 är spel menyn
            else if (BackgroundIndex == 2)
            {
                spriteBatch.Draw(CursorTexture, CursorRect, Color.White);
                spriteBatch.Draw(BacgroundMenuTexture, BackgroundMenuRect, Color.White);

                if (kstate.IsKeyDown(Keys.Enter) && menyvalue == 0)
                {
                    BackgroundIndex = 1;
                    //går till baka till spelet utan att reseta
                }
                else if (kstate.IsKeyDown(Keys.Enter) && menyvalue == 2)
                {
                    menyTime = 20;
                    BackgroundIndex = 0;
                    CursorRect.Y = 210;
                    menyvalue = 0;

                }
                else if ((kstate.IsKeyDown(Keys.Enter) && menyvalue == 1) && menyTime < 20)
                {
                    Exit();
                }



                spriteBatch.DrawString(Font, "Continue Game", new Vector2(460, 200), Color.White);
                spriteBatch.DrawString(Font, "Exit Game", new Vector2(540, 350), Color.White);
                spriteBatch.DrawString(Font, "Exit to menu", new Vector2(510, 500), Color.White);
            }
            //bagroundindez == 3 är controls som jag tänkte lägga set och get metoder på dessa ifall jag hade tid
            else if (BackgroundIndex == 3) 
            {
                spriteBatch.DrawString(Font, "Press Enter to return to menu", new Vector2(250, 150), Color.White);
                spriteBatch.DrawString(Font, "A = Move Left", new Vector2(460, 300), Color.White);
                spriteBatch.DrawString(Font, "D = Move Right", new Vector2(450, 450), Color.White);
                spriteBatch.DrawString(Font, "Space = Shout bullets", new Vector2(350, 600), Color.White);

                if(kstate.IsKeyDown(Keys.Enter) && menyTime < 20) 
                {
                    BackgroundIndex = 0;
                    menyTime = 20;
                    CursorRect.Y = 210;
                }
            }
            //backgroundidex == 1 är spelet
            else if (BackgroundIndex == 1)
            {
                //för varje component i listan game components ritar den ut gameTime spritebatch
                foreach (var component in GameComponents)
                {
                    component.Draw(gameTime, spriteBatch);
                }

                //ifall spelet inte är igång så säger en text hur man startar spelet
                if (GameStarted == false) 
                {
                    spriteBatch.DrawString(Font, "Press Enter to start the game", new Vector2(250, 150), Color.White);
                }
                
            }
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}