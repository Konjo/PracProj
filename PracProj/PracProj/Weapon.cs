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
    public class Weapon {
        #region Fields
        protected int speed, reload, maxReload;
        protected Texture2D boltsTexture;
        protected List<Bolt> bolts;
        protected Bolt toDelete;
        protected List<DynamicGameObject> dobjects;
        protected List<GameObject> sobjects;
        protected int team;
        #endregion

        #region Properties
        public int Speed {
            get {
                return this.speed;
            }
            protected set {
                this.speed = value;
            }
        }

        public List<Bolt> Bolts {
            get {
                return this.bolts;
            }
        }

        public List<DynamicGameObject> Dobjects {
            get {
                return dobjects;
            }
            set {
                this.dobjects = value;
            }
        }

        public List<GameObject> Sobjects {
            get {
                return sobjects;
            }
            set {
                this.sobjects = value;
            }
        }
        #endregion

        #region Constructors
        public Weapon(int speed, Texture2D boltsTexture, List<DynamicGameObject> dobjects, List<GameObject> sobjects, int team) {
            this.boltsTexture = boltsTexture;
            this.speed = speed;
            this.dobjects = dobjects;
            this.sobjects = sobjects;
            maxReload = reload = 20;
            this.team = team;
            bolts = new List<Bolt>();
        }
        #endregion

        #region Methods
        public void Shoot(Vector2 position, Vector2 direction) {
            if (reload <= 0) {
                bolts.Add(new Bolt(position, boltsTexture, Speed, direction, Dobjects, Sobjects, team));
                reload = maxReload;
            }
        }

        public void ChangeBoltsTexture(Texture2D texture) {
            boltsTexture = texture;
        }

        public void Update(GameTime gameTime) {
            if(reload > 0) reload--;
            toDelete = null;
            foreach (Bolt b in bolts) {
                b.Update(gameTime);
                if (b.LifeTime <= 0) toDelete = b;
            }
            if(toDelete != null) bolts.Remove(toDelete);
        }

        public void Draw(SpriteBatch spriteBatch) {
            foreach (Bolt b in bolts) b.Draw(spriteBatch);
        }
        #endregion
    }
}
