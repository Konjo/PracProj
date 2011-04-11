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
    public class StaticObjectManager : DrawableGameComponent {
        #region Fields
        private List<GameObject> objects;
        protected SpriteBatch spriteBatch;
        protected ContentManager contentManager;
        #endregion

        #region Properties
        public List<GameObject> Objects {
            get {
                return objects;
            }
        }
        #endregion

        #region Constructors
        public StaticObjectManager(Game1 game)
            : base(game) {
            objects = new List<GameObject>();
            spriteBatch = new SpriteBatch(Game.GraphicsDevice);
            contentManager = game.ContentManager;
        }
        #endregion

        #region Methods
        public virtual void CreateGame(string level) {
            Objects.Clear();
            Objects.Add(new GameObject(new Vector2(-50, -50), contentManager.Load<Texture2D>(@"Textures\Grounds\ground0")));
        }

        public override void Draw(GameTime gameTime) {
            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, null, null, null, null, Matrix.CreateTranslation(((Game1)Game).Player.Camera));
            foreach (GameObject go in Objects) go.Draw(spriteBatch);
            spriteBatch.End();

            base.Draw(gameTime);
        }
        #endregion
    }
}
