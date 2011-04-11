using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace PracProj {
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game {
        #region Fields
        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;
        private ContentManager contentManager;
        private Texture2D cursor;
        private MenuManager menuManager;
        private DynamicObjectManager dynamicObjectManager;
        private StaticObjectManager staticObjectManager;
        private GameState state;
        private Player player;
        public static Random rand;
        #endregion

        #region Properties
        public GameState State {
            get {
                return state;
            }
            set {
                this.state = value;
            }
        }

        public MenuManager Menu {
            get {
                return menuManager;
            }
        }

        public DynamicObjectManager DynamicObjectManager {
            get {
                return dynamicObjectManager;
            }
        }

        public StaticObjectManager StaticObjectManager {
            get {
                return staticObjectManager;
            }
        }

        public ContentManager ContentManager {
            get {
                return contentManager;
            }
        }

        public Player Player {
            get {
                return player;
            }
            set {
                this.player = value;
            }
        }

        public Vector2 Center {
            get {
                return new Vector2(Window.ClientBounds.Width / 2, Window.ClientBounds.Height / 2);
            }
        }
        #endregion

        #region Constructors
        public Game1() {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            contentManager = new ContentManager(Services);
            contentManager.RootDirectory = "Content";
            State = GameState.SignIn;
            rand = new Random();

            graphics.PreferredBackBufferHeight = 700;
            graphics.PreferredBackBufferWidth = 1200;
        }
        #endregion

        #region Methods
        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize() {
            // TODO: Add your initialization logic here
            menuManager = new MenuManager(this);
            Components.Add(menuManager);

            staticObjectManager = new StaticObjectManager(this);
            Components.Add(staticObjectManager);

            dynamicObjectManager = new DynamicObjectManager(this);
            Components.Add(dynamicObjectManager);


            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent() {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            cursor = contentManager.Load<Texture2D>(@"Textures\Cursors\cursor1");
            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent() {
            // TODO: Unload any non ContentManager content here
        }

        protected void Activate(GameComponent component) {
            component.Enabled = true;
            if (component is DrawableGameComponent) ((DrawableGameComponent)component).Visible = true;
        }

        protected void Deactivate(GameComponent component) {
            component.Enabled = false;
            if (component is DrawableGameComponent) ((DrawableGameComponent)component).Visible = false;
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime) {
            if (Keyboard.GetState().IsKeyDown(Keys.Escape)) {
                Menu.State = MenuState.Pause;
                Activate(Menu);
                Deactivate(DynamicObjectManager);
                Deactivate(StaticObjectManager);
                State = GameState.Start;
            }

            switch (State) {
                case GameState.SignIn:
                case GameState.CreateSession:
                case GameState.FindSession:
                case GameState.Start:
                    Activate(Menu);
                    Deactivate(DynamicObjectManager);
                    Deactivate(StaticObjectManager);
                    break;
                case GameState.InGame:
                    Activate(DynamicObjectManager);
                    Activate(StaticObjectManager);
                    Deactivate(Menu);
                    break;
            }

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime) {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            // TODO: Add your drawing code here

            base.Draw(gameTime);

            spriteBatch.Begin();
            spriteBatch.Draw(cursor, new Vector2(Mouse.GetState().X, Mouse.GetState().Y), Color.White);
            spriteBatch.End();
        }
        #endregion
    }
}
