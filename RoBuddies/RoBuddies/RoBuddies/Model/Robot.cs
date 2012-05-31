using System;
using System.Collections.Generic;

using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace RoBuddies.Model
{

    /// <summary>
    /// collection of all robotparts
    /// </summary>
    class Robot
    {
        private IBody lowerPart;
        private IBody upperPart;
        private IBody partsCombined;
        private IBody head;
        private IBody activePart;

        private Level level;

        public Robot(ContentManager content, Vector2 pos, Level level) 
        {
            this.level = level;
            initRobot(content, pos);

        }

        private void initRobot(ContentManager content, Vector2 pos) 
        {
            Texture2D textureLowerPart = content.Load<Texture2D>("Sprite//Robot//Bud//0001");
        }

        public IBody LowerPart 
        {
            get { return lowerPart; }
        }

        public IBody UpperPart 
        {
            get { return upperPart; }
        }

        public IBody PartsCombined
        {
            get { return partsCombined; }
        }

        public IBody Head 
        {
            get { return head; }
            set { head = value; }
        }

        public IBody ActivePart
        {
            get { return activePart; }
            set { activePart = value; }
        }


        public void Update() 
        {
        }

    }
}
