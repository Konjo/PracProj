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
    public class GameObject {
        #region Fields
        protected Vector2 position;
        protected Texture2D texture;
        protected int height;
        protected int width;
        #endregion

        #region Properties
        public Vector2 Position {
            get {
                return position;
            }
            set {
                this.position = value;
            }
        }

        public Texture2D Texture {
            get {
                return texture;
            }
            protected set {
                this.texture = value;
            }
        }

        public int Height {
            get {
                return height;
            }
            protected set {
                this.height = value;
            }
        }

        public int Width {
            get {
                return width;
            }
            protected set {
                this.width = value;
            }
        }
        
        public Rectangle Rectangle {
            get {
                return new Rectangle((int)Position.X, (int)Position.Y, Texture.Width / Width, Texture.Height / Height);
            }
        }

        public Rectangle RectangleDraw {
            get {
                return new Rectangle(0, 0, Texture.Width / Width, Texture.Height / Height);
            }
        }

        public Vector2 Center {
            get {
                return new Vector2((Texture.Width / Width) / 2, (Texture.Height / Height) / 2);
            }
        }
        #endregion

        #region Constructors
        public GameObject(Vector2 position, Texture2D texture) {
            Position = position;
            Texture = texture;
            Width = 1;
            Height = 1;
        }
        #endregion

        #region Methods
        public virtual void Init() {
        }

        /// <returns>bool</returns>
        public virtual bool Colide(GameObject obj) {
            return this.Rectangle.Intersects(obj.Rectangle);
        }

        public virtual void Draw(SpriteBatch spriteBatch) {
            spriteBatch.Draw(Texture, Position, RectangleDraw, Color.White);
        }
        #endregion
    }
}
