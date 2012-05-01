using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Fall_Ball.Objects;
using Microsoft.Xna.Framework;

// author: Thomas

namespace Fall_Ball
{
    class LevelBottomReach : Level
    {
        protected int score = 1000;
        protected bool ballOneReachedBottom = false;
        protected bool ballTwoReachedBottom = false;
        protected bool timerStarted = false;
        protected bool timerStopped = false;
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

            overlay.ButtomString = "Score: " + newScore;
            overlay.ButtomString2 = "";

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
            }

            if (newScore < 0)
            {
                newScore = 0;
            }
            return newScore;
        }
    }
}
