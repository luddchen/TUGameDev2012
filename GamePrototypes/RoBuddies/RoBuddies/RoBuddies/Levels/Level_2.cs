using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using FarseerPhysics.Dynamics;
using FarseerPhysics.Factories;
using Robuddies.Objects;

using System.Linq;
using System.Text;

namespace Robuddies.Levels
{
    class Level_2 : Level
    {

        public Level_2(Game1 game) 
            : base(game)
        {
        }

        public override void LoadContent() 
        {
            base.LoadContent();
            backgroundColor = Color.LightBlue; //changed according to the background content

            Texture2D groundTex = game.Content.Load<Texture2D>("Sprites\\Ground1");

            Layer layerHighWall = new Layer(Camera);
            layerHighWall.LoadContent();
            layerHighWall.Depth = 0.8f;

            GameObject highWall;
            highWall = new GameObject(groundTex, new Vector2(0, groundTex.Height));
            highWall.Scale = 4;
            layerHighWall.add(highWall);

            GameObject highWall2;
            highWall2 = new GameObject(groundTex, new Vector2(0, groundTex.Height*5));
            highWall2.Scale = 4;
            layerHighWall.add(highWall2);
            
            GameObject highWall3;
            highWall3 = new GameObject(groundTex, new Vector2(0, groundTex.Height*9));
            highWall3.Scale = 4;
            layerHighWall.add(highWall3);
            
            GameObject highWall4;
            highWall4 = new GameObject(groundTex, new Vector2(0, groundTex.Height*13));
            highWall4.Scale = 4;
            layerHighWall.add(highWall4);


            GameObject highWall12;
            highWall12 = new GameObject(groundTex, new Vector2(highWall.Width * 2, groundTex.Height));
            highWall12.Scale = 4;
            layerHighWall.add(highWall12);

            GameObject highWall22;
            highWall22 = new GameObject(groundTex, new Vector2(highWall.Width * 2, groundTex.Height * 5));
            highWall22.Scale = 4;
            layerHighWall.add(highWall22);

            GameObject highWall32;
            highWall32 = new GameObject(groundTex, new Vector2(highWall.Width * 2, groundTex.Height * 9));
            highWall32.Scale = 4;
            layerHighWall.add(highWall32);

            GameObject highWall42;
            highWall42 = new GameObject(groundTex, new Vector2(highWall.Width * 2, groundTex.Height * 13));
            highWall42.Scale = 4;
            layerHighWall.add(highWall42);

            layers.Add(layerHighWall);

            mainLayer.remove(player);
            player = new Robot(game.Content, new Vector2(0, groundTex.Height * 14), gameWorld, this);
            player.Scale *= 0.3f;
            mainLayer.add(player);
            //player.Position = new Vector2(TitleSafe.Width / 2, groundTex.Height * 14);
            activePart = player.ActivePart;
        
        }
    }
}
