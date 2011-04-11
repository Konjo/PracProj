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
    public class ControlManager {
        #region Fields
        protected Dictionary<string, Control> controls;
        #endregion

        #region Properties
        #endregion

        #region Constructors
        public ControlManager() {
            controls = new Dictionary<string, Control>();
        }
        #endregion

        #region Methods
        public Control GetControl(string key) {
            return controls[key];
        }

        public void AddControl(string key, Control value) {
            controls.Add(key, value);
        }

        public void RemoveControl(string key) {
            controls.Remove(key);
        }

        public void Update(GameTime gameTime) {
            foreach (var control in controls) {
                control.Value.Update(gameTime);
            }
        }

        public void Draw(SpriteBatch spriteBatch) {
            foreach (var control in controls) {
                control.Value.Draw(spriteBatch);
            }
        }
        #endregion
    }
}
