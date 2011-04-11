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
    public class DynamicObjectManager : StaticObjectManager {
        #region Fields
        //private List<DynamicGameObject> objects;
        private Dictionary<string, List<DynamicGameObject>> objects;
        private DynamicGameObject toDelete;
        #endregion

        #region Properties
        public new Dictionary<string, List<DynamicGameObject>> Objects {
            get {
                return objects;
            }
        }
        #endregion

        #region Constructors
        public DynamicObjectManager(Game1 game)
            : base(game) {
            objects = new Dictionary<string, List<DynamicGameObject>>();
            objects.Add("Monster", new List<DynamicGameObject>()); // in Monster we store monters and players
            objects.Add("Ground", new List<DynamicGameObject>());
        }
        #endregion

        #region Methods
        public override void CreateGame(string level) {
            Objects["Monster"].Clear();
            ((Game1)Game).Player = new Player(new Vector2(100, 100), contentManager.Load<Texture2D>(@"Textures\Players\player0"),
                0, contentManager,
                Objects, ((Game1)Game).StaticObjectManager.Objects, new Vector2(((Game1)Game).Center.X, ((Game1)Game).Center.Y));
            Objects["Monster"].Add(((Game1)Game).Player);
            ((Game1)Game).Components.Add(new Controller(((Game1)Game), ((Game1)Game).Player));

            for (int i = 0; i < 5; i++) {
                Objects["Monster"].Add(new Monster(new Vector2(300 + i * 55, 300), contentManager.Load<Texture2D>(@"Textures\Monsters\monster0"), 1, contentManager, Objects, ((Game1)Game).StaticObjectManager.Objects));
            }
        }

        public override void Update(GameTime gameTime) {
            toDelete = null;
            foreach (DynamicGameObject dgo in Objects["Monster"]) {
                //if (dgo is Monster) Mouse.SetPosition((int)dgo.Position.X, (int)dgo.Position.Y);
                dgo.Update(gameTime);
                if (((Monster)dgo).Health <= 0) toDelete = dgo;
            }

            if (toDelete != null) {
                Objects["Monster"].Remove(toDelete);
            }
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime) {
            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, null, null, null, null, Matrix.CreateTranslation(((Game1)Game).Player.Camera));
            foreach (DynamicGameObject dgo in Objects["Monster"]) dgo.Draw(spriteBatch);
            spriteBatch.End();

            //base.Draw(gameTime);
        }
        #endregion
    }
}
