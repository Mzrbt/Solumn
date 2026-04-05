using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Solumnn.UI
{
    public class Button
    {
        private Rectangle _rectangle;

        private Color _colorNormal = Color.DarkGray;
        private Color _colorHover = Color.Gray;

        private SpriteFont _font;
        private string _text;

        private Texture2D _texture;

        private MouseState _previousMouseState;

        public Action OnClick { get; set; }

        private bool _isHovered;

        public Button(Rectangle rectangle, string text, SpriteFont font, GraphicsDevice graphicsDevice)
        {
            _rectangle = rectangle;
            _text = text;
            _font = font;

            _texture = new Texture2D(graphicsDevice, 1, 1);
            _texture.SetData(new[] { Color.White });
        }

        public void Update()
        {
            MouseState mouse = Mouse.GetState();
            Point mousePoint = new Point(mouse.X, mouse.Y);

            _isHovered = _rectangle.Contains(mousePoint);

            if (_isHovered
                && mouse.LeftButton == ButtonState.Released
                && _previousMouseState.LeftButton == ButtonState.Pressed)
            {
                OnClick?.Invoke();
            }

            _previousMouseState = mouse;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            Color color = _isHovered ? _colorHover : _colorNormal;
            spriteBatch.Draw(_texture, _rectangle, color);

            Vector2 textSize = _font.MeasureString(_text);
            Vector2 textPos = new Vector2(
                _rectangle.X + (_rectangle.Width - textSize.X) / 2,
                _rectangle.Y + (_rectangle.Height - textSize.Y) / 2
            );

            spriteBatch.DrawString(_font, _text, textPos, Color.White);
        }
    }
}