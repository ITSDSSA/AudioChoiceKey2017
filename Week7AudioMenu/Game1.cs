using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Loader;
using AudioPlayer;
using Engine.Engines;
using Microsoft.Xna.Framework.Audio;
using System.Collections.Generic;

namespace Week7AudioMenu
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        SoundEffectInstance soundPlayer = null;
        List<string> soundNames = new List<string>();
        MenuItem item1;
        SpriteFont font;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
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
            new InputEngine(this);
            IsMouseVisible = true;

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

            AudioManager.SoundEffects = 
                ContentLoader.ContentLoad<SoundEffect>(Content, "Audio");

            foreach (KeyValuePair<string, SoundEffect> entry in AudioManager.SoundEffects)
                        soundNames.Add(entry.Key);
            font = Content.Load<SpriteFont>("font");

            item1 = new MenuItem(Content.Load<Texture2D>("image1"), 
                            soundNames[1], 
                                new Rectangle(10, 10, 100, 50));
            // TODO: use this.Content to load your game content here
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
            // Play sound based on digits 0 to 4
            Keys[] ks = InputEngine.CurrentKeyState.GetPressedKeys();
            foreach (var key in ks)
            {
                if((int)key >= 48 && (int)key - 48 < soundNames.Count)
                    AudioManager.Play(ref soundPlayer, soundNames[(int)key - 48]);
            }
            // TODO: Add your update logic here
            item1.update();
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            spriteBatch.Begin();
            item1.Draw(spriteBatch, font);
            spriteBatch.End();
            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }
    }
}
