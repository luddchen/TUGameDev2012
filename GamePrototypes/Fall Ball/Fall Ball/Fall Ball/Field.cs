using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Fall_Ball.Objects;

namespace Fall_Ball
{
    // contains the gamefield
    class Field
    {

        public List<GameObject> objects;

        public Field()
        {
            this.objects = new List<GameObject>();
        }

        public void draw(Vector2 offset, float scale)
        {
            foreach (GameObject obj in objects) 
            { 
                obj.draw( offset , scale); 
            }
        }

        public void drawMap(Vector2 offset, float scale)
        {
            foreach (GameObject obj in objects)
            {
                obj.drawMap(offset, scale);
            }
        }

        public void add(GameObject gameObject)
        {
            this.objects.Add( gameObject );
        }

        public void remove(GameObject gameObject)
        {
            this.objects.Remove(gameObject);
        }

    }
}
