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
    /// Bolt will send data throught network to player which it colidates with, with its damge.
    /// </summary>
    /// <remarks>If lifetime less than 0 then bolt is dead.</remarks>
    public class Bolt : DynamicGameObject {
        #region Fields
        protected float damage;
        protected float speed;
        protected Vector2 direction;
        protected int lifeTime;
        protected List<DynamicGameObject> dobjects;
        protected List<GameObject> sobjects;
        protected int team;
        #endregion

        #region Properties
        public float Damage {
            get {
                return this.damage;
            }
            protected set {
                this.damage = value;
            }
        }

        public int LifeTime {
            get {
                return this.lifeTime;
            }
            protected set {
                this.lifeTime = value;
            }
        }
        #endregion

        #region Constructors
        public Bolt(Vector2 position, Texture2D texture, float speed, Vector2 direction, List<DynamicGameObject> dobjects, List<GameObject> sobjects, int team)
            : base(position, texture) {
            this.damage = 1f;
            this.lifeTime = 100;
            this.speed = speed;
            this.dobjects = dobjects;
            this.sobjects = sobjects;
            this.direction = direction;
            this.team = team;
        }
        #endregion

        #region Methods
        public override void Update(GameTime gameTime) {
            this.position += direction * speed;
            LifeTime--;

            foreach (DynamicGameObject dgo in dobjects) {
                if (((Monster)dgo).Team != this.team) {
                    if (this.Colide(dgo)) {
                        ((Monster)dgo).Damage(Damage);
                        LifeTime = 0;
                    }
                }
            }

            foreach (GameObject go in sobjects) {
                if (this.Colide(go)) {
                    LifeTime = 0;
                }
            }

            base.Update(gameTime);
        }
        #endregion
    }
}
