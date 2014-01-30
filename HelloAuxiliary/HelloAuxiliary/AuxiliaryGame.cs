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
// **** NOTE: Two namespace declarations.
using Auxiliary;
using Cother;
namespace HelloAuxiliaryNamespace
{
    public partial class AuxiliaryGame : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        public AuxiliaryGame()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            this.IsMouseVisible = true;
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            // **** NOTE: This function must be called before any other calls to Auxiliary functions.
            Root.Init(this, spriteBatch, graphics, new Resolution(1024,768));
            // **** NOTE: We are adding a new game phase to the stack. You may add several phases, they will all be displayed but the user
            // will be able to interact only with the topmost one.
            Root.PushPhase(new InformationGamePhase());
        }
     
        protected override void Update(GameTime gameTime)
        {
            // **** NOTE: We can detect key presses, not simply whether they are currently pushed down, this way:
            if (Root.WasKeyPressed(Keys.Escape)) this.Exit();
            if (Root.WasKeyPressed(Keys.Enter, ModifierKey.Alt))
            {
                Root.IsFullscreen = !Root.IsFullscreen;
            }

            // **** NOTE: Remember to put Root.Update() in your Update() method, like this:
            Root.Update(gameTime);
            base.Update(gameTime);
        }


        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            spriteBatch.Begin();
            // **** NOTE: Remember to put these draw methods in your Draw() method, like this: (They must be enclosed by spriteBatch.Begin() and spriteBatch.End().)
            Root.DrawPhase(gameTime);    // <!-- Here, the InformationGamePhase we pushed on the stack is drawn        
            Root.DrawOverlay(gameTime);
            spriteBatch.End();
            base.Draw(gameTime);
        }
        protected override void Initialize()
        {
            base.Initialize();
        }
        protected override void UnloadContent()
        {
        }
    }
}
