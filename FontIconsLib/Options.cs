using System.Drawing;

namespace FontIconsLib
{
    public class Options
    {
        public Options()
        {
            Size = 32;
            Location = new Point(0, 0);
            ForeColor = Color.Black;
            BackColor = Color.Transparent;
            BorderColor = Color.Gray;
            ShowBorder = false;
        }

        /// <summary>
        /// Image square size in pixels
        /// </summary>
        public int Size { get; set; }

        /// <summary>
        /// Position of image
        /// </summary>
        public Point Location { get; set; }

        public Color ForeColor { get; set; }

        public Color BackColor { get; set; }

        public Color BorderColor { get; set; }

        public bool ShowBorder { get; set; }
    }
}
