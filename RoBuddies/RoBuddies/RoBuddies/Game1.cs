using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace RoBuddies
{
    /// <summary>
    /// Dies ist der Haupttyp für Ihr Spiel
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Ermöglicht dem Spiel die Durchführung einer Initialisierung, die es benötigt, bevor es ausgeführt werden kann.
        /// Dort kann es erforderliche Dienste abfragen und nicht mit der Grafik
        /// verbundenen Content laden.  Bei Aufruf von base.Initialize werden alle Komponenten aufgezählt
        /// sowie initialisiert.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Fügen Sie Ihre Initialisierungslogik hier hinzu

            base.Initialize();
        }

        /// <summary>
        /// LoadContent wird einmal pro Spiel aufgerufen und ist der Platz, wo
        /// Ihr gesamter Content geladen wird.
        /// </summary>
        protected override void LoadContent()
        {
            // Erstellen Sie einen neuen SpriteBatch, der zum Zeichnen von Texturen verwendet werden kann.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: Verwenden Sie this.Content, um Ihren Spiel-Content hier zu laden
        }

        /// <summary>
        /// UnloadContent wird einmal pro Spiel aufgerufen und ist der Ort, wo
        /// Ihr gesamter Content entladen wird.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Entladen Sie jeglichen Nicht-ContentManager-Content hier
        }

        /// <summary>
        /// Ermöglicht dem Spiel die Ausführung der Logik, wie zum Beispiel Aktualisierung der Welt,
        /// Überprüfung auf Kollisionen, Erfassung von Eingaben und Abspielen von Ton.
        /// </summary>
        /// <param name="gameTime">Bietet einen Schnappschuss der Timing-Werte.</param>
        protected override void Update(GameTime gameTime)
        {
            // Ermöglicht ein Beenden des Spiels
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            // TODO: Fügen Sie Ihre Aktualisierungslogik hier hinzu

            base.Update(gameTime);
        }

        /// <summary>
        /// Dies wird aufgerufen, wenn das Spiel selbst zeichnen soll.
        /// </summary>
        /// <param name="gameTime">Bietet einen Schnappschuss der Timing-Werte.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Fügen Sie Ihren Zeichnungscode hier hinzu

            base.Draw(gameTime);
        }
    }
}
