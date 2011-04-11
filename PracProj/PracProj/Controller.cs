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
    public class Controller : GameComponent {
        #region Fields
        // Dictionary will contain keys which can manage the movement etc.
        private Dictionary<string, Keys> keys;
        private Player player;
        #endregion

        #region Properties
        public bool this[string key] {
            get {
                return KeyboardState.IsKeyDown(keys[key]);
            }
        }

        public KeyboardState KeyboardState {
            get {
                return Keyboard.GetState();
            }
        }

        public Player Player {
            get {
                return this.player;
            }
        }
        #endregion

        #region Constructors
        public Controller(Game1 game, Player player)
            : base(game) {
                this.player = player;
                keys = new Dictionary<string, Keys>();
                keys.Add("A", Keys.A);
                keys.Add("Left", Keys.Left);
                keys.Add("D", Keys.D);
                keys.Add("Right", Keys.Right);
                keys.Add("W", Keys.W);
                keys.Add("Up", Keys.Up);
                keys.Add("S", Keys.S);
                keys.Add("Down", Keys.Down);
                
            /*keys = new Dictionary<Keys, string>();
            keys.Add(Keys.A, "Move left");
            keys.Add(Keys.Left, "Move left");

            keys.Add(Keys.D, "Move right");
            keys.Add(Keys.Right, "Move right");

            keys.Add(Keys.W, "Move up");
            keys.Add(Keys.Up, "Move up");

            keys.Add(Keys.S, "Move down");
            keys.Add(Keys.Down, "Move down");*/
        }
        #endregion

        #region Methods
        public override void Update(GameTime gameTime) {
            Vector2 move = Vector2.Zero;
            if (this["A"] || this["Left"]) move += new Vector2(-Player.Speed, 0);
            if (this["D"] || this["Right"]) move += new Vector2(Player.Speed, 0);
            if (this["W"] || this["Up"]) move += new Vector2(0, -Player.Speed);
            if (this["S"] || this["Down"]) move += new Vector2(0, Player.Speed);
            if (Mouse.GetState().LeftButton == ButtonState.Pressed) {
                Player.Shoot(new Vector2(Mouse.GetState().X - (Player.Position.X + Player.Camera.X), Mouse.GetState().Y - (Player.Position.Y + Player.Camera.Y)));
            }

            Player.Move(new Vector2(move.X, 0));
            foreach (GameObject go in ((Game1)Game).StaticObjectManager.Objects) {
                if (Vector2.Distance(go.Position, Player.Position) < 500) {
                    if (Player.Colide(go)) Player.Move(new Vector2(-move.X, 0));
                }
            }

            Player.Move(new Vector2(0, move.Y));
            foreach (GameObject go in ((Game1)Game).StaticObjectManager.Objects) {
                if (Vector2.Distance(go.Position, Player.Position) < 500) {
                    if (Player.Colide(go)) Player.Move(new Vector2(0, -move.Y));
                }
            }
            //Player.Move(move);
            base.Update(gameTime);
        }
        #endregion
    }
}
