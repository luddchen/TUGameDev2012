
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using RoBuddies.View.HUD;

namespace RoBuddies.View
{
    class EditorToolbar : HUD.HUD
    {

        private int HUD_height = 200;
        private int HUD_width = 200;

        public HUDString resetCamButton;
        public HUDString clearButton;
        public HUDString loadButton;
        public HUDString saveButton;
        public HUDString gridButton;

        public HUDTexture WallButton;
        public HUDTexture BudBudiButton;
        public HUDTexture CrateButton;
        public HUDTexture PipeButton;

        public override void OnViewPortResize()
        {
            this.viewport.Y = -1 + this.viewport.Height - this.HUD_height;
            this.viewport.X += 1;
            this.viewport.Height = this.HUD_height;
            this.viewport.Width = this.HUD_width;
            this.backgroundDest = new Rectangle(0, 0, this.viewport.Width, this.viewport.Height);
        }

        public EditorToolbar(RoBuddies game)
            : base(game)
        {
            SpriteFont font = game.Content.Load<SpriteFont>("Fonts\\Font");

            this.background = this.Game.Content.Load<Texture2D>("Sprites//SquareRound");
            this.backgroundColor = new Color(0, 0, 0, 192);
            this.AllElements.Add(new HUDString("Toolbar", null, new Vector2(this.HUD_width / 2, 12), Color.Red, null, 0.5f, 0, game.Content));

            this.resetCamButton = new HUDString("camera reset", font, new Vector2(this.HUD_width * 0.35f, 40), Color.Black, Color.Gray, 0.5f, 0, game.Content);
            this.AllElements.Add(this.resetCamButton);

            this.gridButton = new HUDString("grid", font, new Vector2(this.HUD_width * 0.8f, 40), Color.Red, Color.Gray, 0.5f, 0, game.Content);
            this.AllElements.Add(this.gridButton);

            this.clearButton = new HUDString("clear", font, new Vector2(1 * this.HUD_width / 4, 70), Color.Black, Color.Gray, 0.5f, 0, game.Content);
            this.AllElements.Add(this.clearButton);

            this.loadButton = new HUDString("load", font, new Vector2(2 * this.HUD_width / 4, 70), Color.Black, Color.Gray, 0.5f, 0, game.Content);
            this.AllElements.Add(this.loadButton);

            this.saveButton = new HUDString("save", font, new Vector2(3 * this.HUD_width / 4, 70), Color.Black, Color.Gray, 0.5f, 0, game.Content);
            this.AllElements.Add(this.saveButton);

            // add Wall Button
            this.WallButton = new HUDTexture(this.Game.Content.Load<Texture2D>("Sprites//Square"), new Vector2(this.HUD_width * 0.3f, 110), 
                                             this.HUD_width, this.HUD_width / 5, Color.Green, 0.5f, 0, game.Content);
            this.AllElements.Add(this.WallButton);

            // add BudBudi button for setting the starting position of the player in the level
            this.BudBudiButton = new HUDTexture(this.Game.Content.Load<Texture2D>("Sprites//Editor//BudBudi"), new Vector2(this.HUD_width * 0.7f, this.HUD_height * 0.7f),
                                                null, null, null, 0.5f, null, game.Content); 
            this.BudBudiButton.Height = this.BudBudiButton.Texture.Height / 2;
            this.BudBudiButton.Width = this.BudBudiButton.Texture.Width / 2;
            this.AllElements.Add(this.BudBudiButton);

            // add crate button
            this.CrateButton = new HUDTexture(this.Game.Content.Load<Texture2D>("Sprites//Crate2"), new Vector2(this.HUD_width * 0.3f, 160),
                                              null, null, null, 0.5f, null, game.Content);
            this.CrateButton.Height = this.CrateButton.Texture.Height / 2;
            this.CrateButton.Width = this.CrateButton.Texture.Width / 2;
            this.AllElements.Add(this.CrateButton);

            // add pipe button
            this.PipeButton = new HUDTexture(this.Game.Content.Load<Texture2D>("Sprites//PipeCenter"), new Vector2(this.HUD_width * 0.3f, 90),
                                             null, null, null, 0.5f, null, game.Content);
            this.PipeButton.Height = this.PipeButton.Texture.Height;
            this.PipeButton.Width = this.PipeButton.Texture.Width * 2;
            this.AllElements.Add(this.PipeButton);
        }



        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
        }
    }
}