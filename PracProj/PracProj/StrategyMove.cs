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
    public class StrategyMove : IStrategy {
        #region Fields
        private List<DynamicGameObject> dobjects;
        private List<GameObject> sobjects;
        private List<Player> players;
        #endregion

        #region Properties
        #endregion

        #region Constructors
        public StrategyMove(List<DynamicGameObject> dobjects, List<GameObject> sobjects) {
            this.dobjects = dobjects;
            this.sobjects = sobjects;
            this.players = new List<Player>();
            foreach (DynamicGameObject dgo in dobjects) {
                if (dgo is Player) players.Add((Player)dgo);
            }
        }
        #endregion

        #region Methods
        public void Action(Monster monster) {
            /*monster.Position += new Vector2((float)Game1.rand.NextDouble(), (float)Game1.rand.NextDouble());
            monster.Angle = Game1.rand.Next(5);*/
            float distance = 10000000;
            Player nearestPlayer = null;
            foreach (Player p in players) {
                if (Vector2.Distance(p.Position, monster.Position) < 200) {
                    if (distance > Vector2.Distance(p.Position, monster.Position)) {
                        distance = Vector2.Distance(p.Position, monster.Position);
                        nearestPlayer = p;
                    }
                }
            }
            if (nearestPlayer != null) {
                Vector2 move = Vector2.Zero;
                move = new Vector2(nearestPlayer.Position.X - monster.Position.X, nearestPlayer.Position.Y - monster.Position.Y);
                move.Normalize();
                //monster.Position += move * monster.Speed;

                // Move if there is no collision with static game objects
                monster.Position += new Vector2(move.X, 0) * monster.Speed;
                foreach (GameObject go in sobjects) {
                    if (Vector2.Distance(go.Position, monster.Position) < 500) {
                        if (monster.Colide(go)) monster.Position -= new Vector2(move.X, 0) * monster.Speed;
                    }
                }

                monster.Position += new Vector2(0, move.Y) * monster.Speed;
                foreach (GameObject go in sobjects) {
                    if (Vector2.Distance(go.Position, monster.Position) < 500) {
                        if (monster.Colide(go)) monster.Position -= new Vector2(0, move.Y) * monster.Speed;
                    }
                }
            }
        }
        #endregion
    }
}
