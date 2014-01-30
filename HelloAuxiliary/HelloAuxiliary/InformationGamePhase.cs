using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HelloAuxiliaryNamespace
{
    class InformationGamePhase : Auxiliary.GamePhase
    {
        // **** NOTE: You can also override the Update() method here. It will be called by Auxiliary during Root.Update().
        protected override void Draw(Microsoft.Xna.Framework.Graphics.SpriteBatch sb, Microsoft.Xna.Framework.Game game, float elapsedSeconds)
        {
            Auxiliary.Primitives.DrawAndFillRoundedRectangle(
                new Microsoft.Xna.Framework.Rectangle(10, 10, 400, 50),
                Microsoft.Xna.Framework.Color.Cyan,
                Microsoft.Xna.Framework.Color.Black,
                5);
            Auxiliary.Primitives.DrawMultiLineText(
                "Hello, Auxiliary!",
                new Microsoft.Xna.Framework.Rectangle(10, 10, 400, 50),
                Microsoft.Xna.Framework.Color.Black,
                Auxiliary.Library.FontVerdana,
                Auxiliary.Primitives.TextAlignment.Middle);
            base.Draw(sb, game, elapsedSeconds);
        }
    }
}
