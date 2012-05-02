using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Fall_Ball.Objects;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Audio;

// author: Thomas

namespace Fall_Ball
{
    abstract class LevelBottomReach : Level
    {
        protected int score = 500;
        protected int bonusItemScore = 200;
        protected bool ballOneReachedBottom = false;
        protected bool ballTwoReachedBottom = false;
        protected bool timerStarted = false;
        protected bool timerStopped = false;
        protected bool levelLost = false;
        protected Square bottomBorder;
        protected TimeSpan timerStartedAt;
        protected TimeSpan timerStoppedAt;


        public LevelBottomReach(List<Texture2D> textures, SpriteBatch batch)
            : base(textures, batch)
        {

        }

        public override void update(GameTime gameTime)
        {
            int newScore = calculateScore(gameTime);

            overlay.BottomCenterString = "Score: " + newScore;
            overlay.BottomString = "";
            overlay.BottomString2 = "";

            base.update(gameTime);
        }

        private int calculateScore(GameTime gameTime)
        {
            int newScore = score;

            // start timer
            if ((ballOneReachedBottom || ballTwoReachedBottom)
                && !timerStarted)
            {
                timerStarted = true;
                timerStartedAt = gameTime.TotalGameTime;
            }

            // hold timer after both balls reached the bottom
            if (ballOneReachedBottom && ballTwoReachedBottom && !timerStopped)
            {
                MediaPlayer.Stop();
                Game1.endGameEffect.Play();
                timerStoppedAt = gameTime.TotalGameTime;
                timerStopped = true;
            }

            // reduce score after timer started
            if (timerStarted && !timerStopped)
            {
                TimeSpan timeDif = gameTime.TotalGameTime.Subtract(timerStartedAt);
                newScore = score - timeDif.Seconds * 100 - timeDif.Milliseconds / 10;
            }

            // calculate final score
            if (timerStopped)
            {
                TimeSpan timeDif = timerStoppedAt.Subtract(timerStartedAt);
                newScore = score - timeDif.Seconds * 100 - timeDif.Milliseconds / 10;
                overlay.CenterString = "Level Cleared!";
            }

            if (newScore < 0 && !levelLost)
            {
                levelLost = true;
                MediaPlayer.Stop();
                Game1.lostGameEffect.Play();
                newScore = 0;
            }
            if(newScore < 0) {
                overlay.CenterString = "Fail!";
                newScore = 0;
            }
            return newScore;
        }
    }
}
