using System.Collections.Generic;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;


namespace RoBuddies.View.HUD
{
    /// <summary>
    /// basis class for HeadUpDisplay Menue
    /// </summary>
    class HUDMenu : HUD
    {
        /// <summary>
        /// border size of contained menu page 
        /// </summary>
        protected int MenuPageBorder = 50;

        /// <summary>
        /// extra border size on top of contained menu page
        /// </summary>
        protected int MenuPageTopExtraBorder = 15;

        /// <summary>
        /// color of menu headline
        /// </summary>
        protected Color HeadLineColor = Color.Orchid;

        /// <summary>
        /// the active menu page
        /// </summary>
        protected HUDMenuPage activePage;

        /// <summary>
        /// history of all previous menu pages to enable a back feature
        /// </summary>
        protected List<HUDMenuPage> pageHistory;

        /// <summary>
        /// old keyboard state
        /// </summary>
        public KeyboardState oldKeyboardState { get; set; }

        /// <summary>
        /// new keyboard state
        /// </summary>
        public KeyboardState newKeyboardState { get; set; }

        /// <summary>
        /// prefered width of menu window
        /// </summary>
        public int PreferedWidth { get; set; }

        /// <summary>
        /// prefered height of menu window
        /// </summary>
        public int PreferedHeight { get; set; }

        /// <summary>
        /// called if viewport resized
        /// </summary>
        public override void OnViewPortResize()
        {
            Viewport newViewport = this.viewport;
            if (newViewport.Width > this.PreferedWidth)
            {
                newViewport.X = newViewport.Width / 2 - this.PreferedWidth / 2;
                newViewport.Width = this.PreferedWidth;
            }
            if (newViewport.Height > this.PreferedHeight)
            {
                newViewport.Y = newViewport.Height / 2 - this.PreferedHeight / 2;
                newViewport.Height = this.PreferedHeight;
            }
            this.viewport = newViewport;
            this.backgroundDest = new Rectangle(0, 0, this.viewport.Width, this.viewport.Height);

            if (this.ActivePage != null)
            {
                Viewport temp = this.viewport;
                temp.Width -= 2 * MenuPageBorder;
                temp.Height -= 2 * MenuPageBorder;
                temp.X += MenuPageBorder;
                temp.Y += MenuPageBorder + MenuPageTopExtraBorder;
                this.ActivePage.Viewport = temp;
            }
        }

        /// <summary>
        /// called if visibility of the HUD is changed
        /// </summary>
        public override void OnVisibilityChange()
        {
            this.ActivePage = this.DefaultPage;
        }

        /// <summary>
        /// actual menu page
        /// </summary>
        public HUDMenuPage ActivePage
        {
            get { return this.activePage; }
            set
            {
                this.activePage = value;
                this.Viewport = this.viewport; // dirty way to get actual (sub-)viewport
                if (this.activePage != null) { this.pageHistory.Add(this.activePage); }
            }
        }

        /// <summary>
        /// default menu page 
        /// </summary>
        public HUDMenuPage DefaultPage { get; set; }

        /// <summary>
        /// constructor for a new Head Up Display Menu
        /// </summary>
        /// <param name="game">the game</param>
        public HUDMenu(RoBuddies game)
            : base(game)
        {
            this.pageHistory = new List<HUDMenuPage>();
            this.backgroundColor = Color.Black;
            this.IsVisible = false;
            this.PreferedWidth = 800;
            this.PreferedHeight = 400;
            this.newKeyboardState = Keyboard.GetState();
        }


        /// <summary>
        /// updates all elements
        /// </summary>
        /// <param name="gameTime">gametime</param>
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            this.oldKeyboardState = this.newKeyboardState;
            this.newKeyboardState = Keyboard.GetState();

            if (this.newKeyboardState.IsKeyDown(Keys.Escape) && this.oldKeyboardState.IsKeyUp(Keys.Escape))
            {
                if (!this.isVisible)
                {
                    this.pageHistory.Clear();
                    this.ActivePage = this.DefaultPage;
                    this.IsVisible = true;
                }
                else
                {
                    if (this.pageHistory.Count > 0) { this.pageHistory.RemoveAt(this.pageHistory.Count - 1); }  // active page should be always here, but nobody knows ..
                    if (this.pageHistory.Count == 0 || this.ActivePage == this.DefaultPage)
                    {
                        IsVisible = !IsVisible;
                    }
                    else
                    {
                        this.ActivePage.OnExit();
                        this.ActivePage = this.pageHistory[this.pageHistory.Count - 1];
                    }
                }
            }

            if (this.ActivePage != null && IsVisible)
            {
                this.ActivePage.Update(gameTime);
            }
        }

        /// <summary>
        /// draw all elements and background
        /// </summary>
        /// <param name="spriteBtach">spritebatch</param>
        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);

            if (IsVisible)
            {
                if (this.ActivePage != null)
                {
                    this.ActivePage.Draw(spriteBatch);
                }

            }
        }
    }
}
