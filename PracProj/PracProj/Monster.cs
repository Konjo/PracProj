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
    public class Monster : DynamicGameObject, IHealth {
        #region Fields
        protected int team;
        protected Dictionary<GameObjectState, List<IStrategy>> strategies;
        //protected List<IStrategy> strategies;
        protected Weapon weapon;
        protected ContentManager contentManager;
        protected List<DynamicGameObject> dobjects;
        protected List<GameObject> sobjects;
        protected float movementSpeed;
        protected float health;
        #endregion

        #region Properties
        public float Health {
            get {
                return this.health;
            }
            set {
                this.health = value;
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

        public Weapon Weapon {
            get {
                return this.weapon;
            }
            set {
            }
        }

        public int Team {
            get {
                return this.team;
            }
            set {
                this.team = value;
            }
        }

        /*public ICollection<IStrategy> Strategies {
            get {
                return this.strategies;
            }
        }*/

        public float Speed {
            get {
                return this.movementSpeed;
            }
        }
        #endregion

        #region Constructors
        public Monster(Vector2 position, Texture2D texture, int team, ContentManager contentManager, Dictionary<string, List<DynamicGameObject>> dobjects, List<GameObject> sobjects)
            : base(position, texture) {
            angle = 0;
            this.contentManager = contentManager;
            this.dobjects = dobjects["Monster"];
            this.sobjects = sobjects;
            Team = team;
            weapon = new Weapon(10, contentManager.Load<Texture2D>(@"Textures\Bolts\bolt0"), Dobjects, Sobjects, Team);
            Init();
        }
        #endregion

        #region Methods
        public override void Init() {
            movementSpeed = 2f;
            this.Width = 10;
            this.health = 5;
            if (this is Monster) {
                strategies = new Dictionary<GameObjectState, List<IStrategy>>();
                strategies.Add(GameObjectState.Idle, new List<IStrategy>());
                strategies[GameObjectState.Idle].Add(new StrategyMove(Dobjects, Sobjects));
                //strategies = new List<IStrategy>();
                //strategies.Add(new StrategyMove(Dobjects, Sobjects));
            }
        }

        public void Damage(float damage) {
            this.Health -= damage;
        }

        public virtual void Shoot(Vector2 direction) {
            direction.Normalize();
            Weapon.Shoot(this.Position, direction);
        }

        public override void Update(GameTime gameTime) {
            Weapon.Update(gameTime);
            foreach (IStrategy s in strategies[State]) s.Action(this);

            base.Update(gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch) {
            Weapon.Draw(spriteBatch);
            base.Draw(spriteBatch);
        }
        #endregion
    }
}
