namespace FontIconsLib
{
    public static class FontIcons
    {
        public static IFontIconSet BoxIconsLogos { get; } = new BoxIcons.Logos();

        public static IFontIconSet BoxIconsRegular { get; } = new BoxIcons.Regular();

        public static IFontIconSet BoxIconsSolid { get; } = new BoxIcons.Solid();




        public static IFontIconSet FontAwesomeBrands { get; } = new FontAwesome.Brands();

        public static IFontIconSet FontAwesomeRegular { get; } = new FontAwesome.Regular();

        public static IFontIconSet FontAwesomeSolid { get; } = new FontAwesome.Solid();




        public static IFontIconSet MaterialIconsOutlined { get; } = new MaterialIcons.Outlined();

        public static IFontIconSet MaterialIconsRegular { get; } = new MaterialIcons.Regular();

        public static IFontIconSet MaterialIconsRound { get; } = new MaterialIcons.Round();

        public static IFontIconSet MaterialIconsSharp { get; } = new MaterialIcons.Sharp();

        public static IFontIconSet MaterialIconsTwoTone { get; } = new MaterialIcons.TwoTone();
    }
}
