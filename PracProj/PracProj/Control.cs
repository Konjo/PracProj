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
    public class Control {
        #region Fields
        protected string text;
        protected string name;
        protected bool enabled;
        protected Color color;
        protected Vector2 position;
        protected Vector2 size;
        protected SpriteFont font;
        #endregion

        #region Properties
        public string Text {
            get { return text; }
            set { text = value; }
        }

        public string Name {
            get { return name; }
            set { name = value; }
        }

        public bool Enabled {
            get { return enabled; }
            set { enabled = value; }
        }

        public Color Color {
            get { return color; }
            set { color = value; }
        }

        public Vector2 Position {
            get { return position; }
            set { position = value; }
        }

        public Vector2 Size {
            get { return size; }
            set { size = value; }
        }

        public SpriteFont Font {
            get { return font; }
            set { font = value; }
        }
        #endregion

        #region Constructors
        public Control()
            : this(Vector2.Zero) {
        }

        public Control(Vector2 position) {
            this.Position = position;
            text = " ";
            name = " ";
            enabled = true;
            color = Color.White;
        }
        #endregion

        #region Methods
        public virtual void Update(GameTime gameTime) {
        }

        /*public virtual void UpdateInput(InputState input) {
        }*/

        public virtual void Draw(SpriteBatch spriteBatch) {
        }
        #endregion
    }
}
