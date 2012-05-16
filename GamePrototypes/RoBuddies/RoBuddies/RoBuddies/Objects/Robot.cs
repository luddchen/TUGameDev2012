using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace Robuddies.Objects
{
    class Robot : GameObject
    {
        private RobotPart bud;
        private RobotPart budi;
        private RobotPart budBudi;
        private RobotPart head;
        private RobotPart activePart;

        private List<RobotPart> robotParts;

        public Robot(ContentManager content, Vector2 pos)
        {
            robotParts = new List<RobotPart>();

            bud = new Bud(content, pos);
            budi = new Budi(content, pos);
            budBudi = new BudBudi(content, pos);

            robotParts.Add(budBudi);
            activePart = budBudi;
        }

        public List<RobotPart> RobotParts
        {
            get { return robotParts; }
            set { robotParts = value; }
        }

        public override float Size
        {
            get { return activePart.Size; }
            set { 
                foreach (RobotPart part in robotParts)
                {
                    part.Size = value;
                }
            }
        }

        public RobotPart Bud
        {
            get { return bud; }
        }

        public RobotPart Budi
        {
            get { return budi; }
        }

        public RobotPart BudBudi
        {
            get { return budBudi; }
        }

        public RobotPart Head
        {
            get { return head; }
            set { head = value; }
        }

        public RobotPart ActivePart
        {
            get { return activePart; }
            set { activePart = value; }
        }
    }
}
