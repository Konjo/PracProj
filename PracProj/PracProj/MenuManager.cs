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
    public class MenuManager : DrawableGameComponent {
        #region Fields
        protected MenuState state;
        protected Dictionary<MenuState, ControlManager> controls;
        SpriteBatch spriteBatch;
        #endregion

        #region Properties
        public MenuState State {
            get {
                return this.state;
            }
            set {
                this.state = value;
            }
        }
        #endregion

        #region Constructors
        public MenuManager(Game1 game)
            : base(game) {
            spriteBatch = new SpriteBatch(Game.GraphicsDevice);
            state = MenuState.Menu;
            controls = new Dictionary<MenuState, ControlManager>();
            controls.Add(MenuState.Pause, new ControlManager());
            controls.Add(MenuState.Menu, new ControlManager());
            controls.Add(MenuState.Create, new ControlManager());
            controls.Add(MenuState.Join, new ControlManager());

            #region Start menu controls
            controls[MenuState.Menu].AddControl("Create",
                new Button(game.ContentManager.Load<Texture2D>(@"Textures\Controls\Buttons\button0"),
                    game.Center - new Vector2(50, 100), "Create",
                    game.ContentManager.Load<SpriteFont>(@"Fonts\font0"), game));
            controls[MenuState.Menu].AddControl("Join",
                    new Button(game.ContentManager.Load<Texture2D>(@"Textures\Controls\Buttons\button0"),
                        game.Center - new Vector2(50, 0), "Join",
                        game.ContentManager.Load<SpriteFont>(@"Fonts\font0"), game));
            controls[MenuState.Menu].AddControl("Exit",
                    new Button(game.ContentManager.Load<Texture2D>(@"Textures\Controls\Buttons\button0"),
                        game.Center - new Vector2(50, -100), "Exit",
                        game.ContentManager.Load<SpriteFont>(@"Fonts\font0"), game));
            #endregion

            #region Create game controls
            for (int i = 1; i <= 2; i++) {
                for (int j = 1; j <= 5; j++) {
                    controls[MenuState.Create].AddControl("Level " + ((i - 1) * 5 + j),
                        new Button(game.ContentManager.Load<Texture2D>(@"Textures\Controls\Buttons\level" + ((i - 1) * 5 + j)),
                            new Vector2(j * 180, i * 150), "Level " + ((i - 1) * 5 + j),
                                game.ContentManager.Load<SpriteFont>(@"Fonts\font0"), game));
                }
            }

            controls[MenuState.Create].AddControl("Back",
                new Button(game.ContentManager.Load<Texture2D>(@"Textures\Controls\Buttons\button0"),
                    game.Center - new Vector2(50, -100), "Back",
                        game.ContentManager.Load<SpriteFont>(@"Fonts\font0"), game));
            #endregion

            #region Join game controls
            controls[MenuState.Join].AddControl("Back",
                new Button(game.ContentManager.Load<Texture2D>(@"Textures\Controls\Buttons\button0"),
                    game.Center - new Vector2(50, -100), "Back",
                        game.ContentManager.Load<SpriteFont>(@"Fonts\font0"), game));
            #endregion

            #region Pause game controls
            controls[MenuState.Pause].AddControl("Exit to menu",
                new Button(game.ContentManager.Load<Texture2D>(@"Textures\Controls\Buttons\button0"),
                    game.Center - new Vector2(50, 0), "Exit to menu",
                        game.ContentManager.Load<SpriteFont>(@"Fonts\font0"), game));

            controls[MenuState.Pause].AddControl("Resume",
                new Button(game.ContentManager.Load<Texture2D>(@"Textures\Controls\Buttons\button0"),
                    game.Center - new Vector2(50, -100), "Resume",
                        game.ContentManager.Load<SpriteFont>(@"Fonts\font0"), game));
            #endregion
        }
        #endregion

        #region Methods
        public void ChangeState(MenuState state, string level) {
            this.state = state;
            // Create game
            if (state == MenuState.Start) {
                ((Game1)Game).StaticObjectManager.CreateGame(level);
                ((Game1)Game).DynamicObjectManager.CreateGame(level);
                ((Game1)Game).State = GameState.InGame;
            }
        }

        public override void Update(GameTime gameTime) {
            if (state != MenuState.Start) controls[state].Update(gameTime);

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime) {
            if (state != MenuState.Start) {
                spriteBatch.Begin();
                controls[state].Draw(spriteBatch);
                spriteBatch.End();
            }
            base.Draw(gameTime);
        }
        #endregion
    }
}
