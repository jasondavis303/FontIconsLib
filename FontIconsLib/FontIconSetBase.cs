using System;
using System.Drawing.Drawing2D;
using System.Drawing;
using System.Drawing.Text;
using System.Runtime.InteropServices;
using System.Collections.Generic;

namespace FontIconsLib
{
    abstract class FontIconSetBase : IFontIconSet
    {
        private readonly PrivateFontCollection _fonts = new PrivateFontCollection();

        public abstract string Name { get; }

        public abstract string Version { get; }
        
        public abstract IReadOnlyDictionary<string, int> Glyphs { get; }

        public FontIconSetBase(byte[] data)
        {
            IntPtr ptr = Marshal.AllocCoTaskMem(data.Length);
            Marshal.Copy(data, 0, ptr, data.Length);
            _fonts.AddMemoryFont(ptr, data.Length);
            Marshal.FreeCoTaskMem(ptr);
        }

        public Icon GetIcon(string name, Color color) => GetIcon(name, new Options { ForeColor = color });

        public Icon GetIcon(string name, Options opts = null)
        {
            opts ??= new Options();
            var img = GetImage(name, opts);
            return Icon.FromHandle(img.GetHicon());
        }



        public Bitmap GetImage(string name, Color color) => GetImage(name, new Options { BackColor = color });

        public Bitmap GetImage(string name, Options opts = null)
        {
            opts ??= new Options();

            var size = GetFontIconRealSize(opts.Size, Glyphs[name]);
            var bmpTemp = new Bitmap(size.Width, size.Height);
            using (Graphics g1 = Graphics.FromImage(bmpTemp))
            {
                g1.TextRenderingHint = TextRenderingHint.AntiAlias;
                g1.Clear(Color.Transparent);
                var font = GetIconFont(opts.Size);
                if (font != null)
                {
                    string character = char.ConvertFromUtf32(Glyphs[name]);
                    g1.DrawString(character, font, new SolidBrush(opts.ForeColor), 0, 0);
                    g1.DrawImage(bmpTemp, 0, 0);
                }
            }

            var bmp = ResizeImage(bmpTemp, opts);
            if (opts.ShowBorder)
            {
                using Graphics g2 = Graphics.FromImage(bmp);
                var pen = new Pen(opts.BorderColor, 1);
                var borderRect = new Rectangle(0, 0, (int)(opts.Size - pen.Width), (int)(opts.Size - pen.Width));
                g2.DrawRectangle(pen, borderRect);
            }
            return bmp;
        }

        private Font GetIconFont(int pixelSize)
        {
            var size = pixelSize / (16f / 12f); //pixel to point conversion rate
            return new Font(_fonts.Families[0], size, FontStyle.Regular, GraphicsUnit.Point);
        }

        private Size GetFontIconRealSize(int size, int iconIndex)
        {
            var bmpTemp = new Bitmap(size, size);
            using (Graphics g1 = Graphics.FromImage(bmpTemp))
            {
                g1.TextRenderingHint = TextRenderingHint.AntiAlias;
                g1.PixelOffsetMode = PixelOffsetMode.HighQuality;
                var font = GetIconFont(size);
                if (font != null)
                {
                    string character = char.ConvertFromUtf32(iconIndex);
                    var format = new StringFormat()
                    {
                        Alignment = StringAlignment.Center,
                        LineAlignment = StringAlignment.Center,
                        Trimming = StringTrimming.Word
                    };

                    var sizeF = g1.MeasureString(character, font, new Point(0, 0), format);
                    return sizeF.ToSize();
                }
            }
            return new Size(size, size);
        }

        private static Bitmap ResizeImage(Bitmap imgToResize, Options opts)
        {
            var srcWidth = imgToResize.Width;
            var srcHeight = imgToResize.Height;

            float ratio = (srcWidth > srcHeight) ? (srcWidth / (float)srcHeight) : (srcHeight / (float)srcWidth);

            var dstWidth = (int)Math.Ceiling(srcWidth / ratio);
            var dstHeight = (int)Math.Ceiling(srcHeight / ratio);

            var x = (int)Math.Round((opts.Size - dstWidth) / 2f, 0);
            var y = (int)(1 + Math.Round((opts.Size - dstHeight) / 2f, 0));

            Bitmap b = new(opts.Size + opts.Location.X, opts.Size + opts.Location.Y);
            using (Graphics g = Graphics.FromImage(b))
            {
                g.Clear(opts.BackColor);
                g.SmoothingMode = SmoothingMode.HighQuality;
                g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                g.PixelOffsetMode = PixelOffsetMode.HighQuality;
                g.DrawImage(imgToResize, x + opts.Location.X, y + opts.Location.Y, dstWidth, dstHeight);
            }
            return b;
        }

    }
}
