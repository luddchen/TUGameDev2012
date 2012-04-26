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
            for (int i = 0; i < this.objects.Count; i++) 
            { 
                this.objects[i].draw( offset ); 
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

        public List<GameObject> getPossibleCollisionObjects(GameObject testObject)
        {
            List<GameObject> list = new List<GameObject>();
            for (int i = 0; i < this.objects.Count; i++)
            {
                if (this.objects[i].box.Intersects(testObject.box))
                {
                    list.Add( this.objects[i] );
                }
            }
            return list;
        }

    }
}
