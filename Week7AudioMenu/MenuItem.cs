using Engine.Engines;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Week7AudioMenu
{
    class MenuItem
    {
        public bool selected;
        public string text;
        public Texture2D tx;
        public Rectangle bounds;
        public MenuItem(Texture2D background, string caption, Rectangle bound)
        {
            tx = background;
            text = caption;
            bounds = bound;
            selected = false;
        }

        public void update()
        {
            if(InputEngine.IsKeyPressed(Keys.Enter))
            {
                SoundEffectInstance soundPlayer = null;
                AudioPlayer.AudioManager.Play(ref soundPlayer, text);
            }
        }

        public void Draw(SpriteBatch sp, SpriteFont font)
        {
            sp.Draw(tx, bounds, Color.White);
            sp.DrawString(font, text, 
                new Vector2(bounds.Left + bounds.Width / 2,
                                            bounds.Top + bounds.Height / 2), 
                Color.White);
        }
    }
}
