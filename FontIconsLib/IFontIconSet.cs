using System.Collections.Generic;
using System.Drawing;

namespace FontIconsLib
{
    public interface IFontIconSet
    {
        string Name { get; }
        string Version { get; }
        IReadOnlyDictionary<string, int> Glyphs { get; }
        Icon GetIcon(string name, Color color);
        Icon GetIcon(string name, Options opts = null);
        Bitmap GetImage(string name, Color color);
        Bitmap GetImage(string name, Options opts = null);
    }
}
