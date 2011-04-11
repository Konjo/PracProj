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
    public class DynamicGameObject : GameObject {
        #region Fields
        protected Vector2 currentFrame;
        protected GameObjectState state;
        protected float angle;
        protected int timeSinceLastFrame;
        protected int millisecondsPerFrame;
        #endregion

        #region Properties
        public Vector2 CurrentFrame {
            get {
                return currentFrame;
            }
            set {
                this.currentFrame = value;
            }
        }

        public GameObjectState State {
            get {
                return state;
            }
            set {
                this.state = value;
            }
        }

        public NetworkSender NetworkSender {
            get {
                throw new System.NotImplementedException();
            }
            set {
            }
        }

        public new Rectangle RectangleDraw {
            get {
                return new Rectangle((int)(CurrentFrame.X * Texture.Width / Width),
                    (int)(CurrentFrame.Y * Texture.Height / Height),
                    Texture.Width / Width,
                    Texture.Height / Height);
            }
        }

        public Rectangle RectangleColision {
            get {
                return new Rectangle((int)(Position.X - Center.X), (int)(Position.Y - Center.Y), Texture.Width / Width, Texture.Height / Height);
            }
        }
        #endregion

        #region Constructors
        public DynamicGameObject(Vector2 position, Texture2D texture)
            : base(position, texture) {
            State = GameObjectState.Idle;
            CurrentFrame = new Vector2(0, 0);
            timeSinceLastFrame = 0;
            millisecondsPerFrame = 50;
            Width = 1;
            Height = 1;
        }
        #endregion

        #region Methods
        public override bool Colide(GameObject obj) {
            if (obj is DynamicGameObject) return this.RectangleColision.Intersects(((DynamicGameObject)obj).RectangleColision);
            return this.RectangleColision.Intersects(obj.Rectangle);
        }

        public override void Init() {

        }

        public virtual void Update(GameTime gameTime) {
            timeSinceLastFrame += gameTime.ElapsedGameTime.Milliseconds;
            if (timeSinceLastFrame > millisecondsPerFrame) {
                timeSinceLastFrame -= millisecondsPerFrame;
                currentFrame.X++;
                currentFrame.X %= Width;
            }
        }

        public override void Draw(SpriteBatch spriteBatch) {
            //spriteBatch.Draw(Texture, Position, RectangleDraw, Color.White);
            spriteBatch.Draw(Texture, Rectangle, RectangleDraw, Color.White, angle, Center, SpriteEffects.None, 0);
        }
        #endregion
    }
}
