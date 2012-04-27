using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

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

        public void draw(Vector3 offset)
        {
            foreach (GameObject obj in objects) 
            { 
                obj.draw( offset ); 
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
