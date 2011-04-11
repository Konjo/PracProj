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
    public class Player : Monster {
        #region Fields
        protected Vector3 camera;
        #endregion

        #region Properties
        public Vector3 Camera {
            get {
                return this.camera;
            }
        }
        #endregion

        #region Constructors
        // That many items in constructor or only Game <- but it is dangerous
        public Player(Vector2 position, Texture2D texture, int team, ContentManager contentManager, Dictionary<string, List<DynamicGameObject>> dobjects, List<GameObject> sobjects, Vector2 center)
            : base(position, texture, team, contentManager, dobjects, sobjects) {
                camera = new Vector3(-position.X + center.X, -position.Y + center.Y, 0);
        }
        #endregion

        #region Methods
        public override void Init() {
            this.health = 1;
            this.Width = 1;
            this.movementSpeed = 3f;
        }

        public void Move(Vector2 vector) {
            Position += vector;
            camera.X -= vector.X;
            camera.Y -= vector.Y;
        }

        public float Angle(Vector2 v1, Vector2 v2) {
            if (v1.X <= v2.X && v1.Y <= v2.Y) return (float)Math.Atan(Vector2.Distance(v1, new Vector2(v1.X, v2.Y)) / Vector2.Distance(v2, new Vector2(v1.X, v2.Y))) - MathHelper.PiOver2;
            if (v1.X <= v2.X && v1.Y >= v2.Y) return (MathHelper.Pi + MathHelper.PiOver2) - (float)Math.Atan(Vector2.Distance(v1, new Vector2(v1.X, v2.Y)) / Vector2.Distance(v2, new Vector2(v1.X, v2.Y)));
            if (v1.X >= v2.X && v1.Y >= v2.Y) return (float)Math.Atan(Vector2.Distance(v1, new Vector2(v1.X, v2.Y)) / Vector2.Distance(v2, new Vector2(v1.X, v2.Y))) - (MathHelper.Pi + MathHelper.PiOver2);
            return MathHelper.PiOver2 - (float)Math.Atan(Vector2.Distance(v1, new Vector2(v1.X, v2.Y)) / Vector2.Distance(v2, new Vector2(v1.X, v2.Y)));
        }

        public override void Update(GameTime gameTime) {
            angle = Angle(new Vector2(Mouse.GetState().X, Mouse.GetState().Y), new Vector2(Position.X + camera.X, Position.Y + camera.Y));
            Weapon.Update(gameTime);
            //base.Update(gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch) {
            spriteBatch.Draw(Texture, Rectangle, RectangleDraw, Color.White, angle, Center, SpriteEffects.None, 0);
            Weapon.Draw(spriteBatch);

            //base.Draw(spriteBatch);
        }
        #endregion
    }
}
