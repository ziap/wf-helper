using System;
using System.Drawing;
using System.Windows.Forms;

namespace Helper
{
    public class Form : System.Windows.Forms.Form
    {
        private Font font;
        private int height = 40;
        private int cursor_x = 0;
        private int cursor_y = 0;
        private int max_width = 0;

        public int margin = 15;
        public int border = 45;
        public float font_size
        {
            get { return font.Size; }
            set { font = new Font(Font.FontFamily, value); }
        }

        public T Add<T>(String text = "", int width = 0) where T : Control, new()
        {
            T t = new T();
            t.Font = font;
            t.Text = text;
            if (width == 0) width = TextRenderer.MeasureText(text, font).Width + 3;
            t.Size = new Size(width, height);
            t.Location = new Point(cursor_x + border, cursor_y + border);
            max_width = Math.Max(max_width, cursor_x + width);
            cursor_x += width + margin;
            Controls.Add(t);
            SetSize();
            return t;
        }

        public void Break(int new_height = 0)
        {
            cursor_x = 0;
            cursor_y += height + margin;
            if (new_height) height = new_height;
        }

        public Form(int first_height = 0)
        {
            font_size = 16;
            if (first_height) height = first_height;
            MaximizeBox = false;
            MinimizeBox = false;
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Load += (object e, EventArgs args) => CenterToScreen();
        }

        private void SetSize()
        {
            Size size = new Size(border * 2 + max_width, cursor_y + height + 2 * border);
            ClientSize = size;
        }
    }
}