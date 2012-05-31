using System;
using System.Collections.Generic;

using RoBuddies.View.HUD;

namespace RoBuddies.View
{
    class EditorView : HUD.HUDLevelView
    {

        public HUDMouse Mouse;

        public EditorView(RoBuddies game)
            : base(game)
        {
            this.Mouse = new HUDMouse(game.Content);
            this.AllElements.Add(this.Mouse);
        }
    }
}
