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
    public class Button : Control {
        #region Fields
        protected Texture2D texture;
        protected Rectangle bounds;
        protected ButtonStatus state = ButtonStatus.Up;
        protected Nullable<MenuState> change;
        public event EventHandler Clicked;
        #endregion

        #region Properties
        public new Vector2 Position {
            get {
                return base.Position;
            }
            set {
                base.Position = value;
                bounds = new Rectangle((int)base.Position.X, (int)base.Position.Y, texture.Width, texture.Height);
            }
        }
        #endregion

        #region Constructors
        public Button(Texture2D texture, Vector2 position, string text, SpriteFont font, Game1 game)
            : base(position) {
            base.Text = text;
            this.texture = texture;
            this.Font = font;
            bounds = new Rectangle((int)position.X, (int)position.Y, texture.Width, texture.Height);

            switch (Text) {
                case "Create":
                    change = MenuState.Create;
                    break;
                case "Join":
                    change = MenuState.Join;
                    break;
                case "Exit to menu":
                case "Back":
                    change = MenuState.Menu;
                    break;
                default:
                    change = null;
                    break;
            }

            if (Text.Contains("Level")) change = MenuState.Start;
            WireUpEvents(game);
        }
        #endregion

        #region Methods
        // Clicked event
        protected void WireUpEvents(Game1 game) {
            // if this button is for change menu state
            if (change != null) {
                Clicked += delegate(object sender, EventArgs e) {
                    ((Button)sender).state = ButtonStatus.Up;
                    game.Menu.ChangeState(change.Value, ((Button)sender).Text);
                };
            } else {
                Clicked += delegate(object sender, EventArgs e) {
                    ((Button)sender).state = ButtonStatus.Up;
                    if (Text.Contains("Exit")) {
                        game.Exit();
                    }
                    if (Text.Contains("Resume")) {
                        game.State = GameState.InGame;
                    }
                };
            }
        }

        protected bool MouseOver(MouseState mouseState) {
            return bounds.Contains(mouseState.X, mouseState.Y);
        }

        public override void Update(GameTime gameTime) {
            switch (state) {
                case ButtonStatus.Clicked:
                case ButtonStatus.Up:
                    this.Color = Color.White;
                    bounds = new Rectangle((int)base.Position.X, (int)base.Position.Y, texture.Width, texture.Height);
                    break;
                /*case ButtonStatus.Down:
                    this.Color = Color.Red;
                    break;*/
            }
            if (MouseOver(Mouse.GetState())) {
                this.Color = Color.Green;
                if (Mouse.GetState().LeftButton == ButtonState.Pressed) {
                    this.state = ButtonStatus.Down;
                }
                if (Mouse.GetState().LeftButton == ButtonState.Released && this.state == ButtonStatus.Down) {
                    state = ButtonStatus.Clicked;
                    // Fire up Clicked event
                    if (Clicked != null) {
                        Clicked(this, EventArgs.Empty);
                    }
                }
            } else {
                state = ButtonStatus.Up;
            }
            base.Update(gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch) {
            if (Enabled) {
                spriteBatch.Draw(texture, bounds, Color);
                spriteBatch.DrawString(Font, Text, Position + new Vector2(texture.Width / 2 - Font.MeasureString(Text).X / 2, texture.Height / 2 - Font.MeasureString(Text).Y / 2), Color.White);
            }
            base.Draw(spriteBatch);
        }
        #endregion
    }
}
