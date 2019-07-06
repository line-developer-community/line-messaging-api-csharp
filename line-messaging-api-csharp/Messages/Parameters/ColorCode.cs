using LineDC.Messaging.JsonConverters;
using Newtonsoft.Json;
namespace LineDC.Messaging.Messages
{
    [JsonConverter(typeof(ToStringJsonConverter))]
    public class ColorCode
    {
        public string Value { get; }
        private ColorCode(string value)
        {
            Value = value;
        }
        public static ColorCode FromRgb(byte r, byte g, byte b)
        {
            return new ColorCode($"#{r.ToString("x2")}{g.ToString("x2")}{b.ToString("x2")}");
        }

        public override string ToString()
        {
            return Value;
        }

        public static ColorCode AliceBlue = new ColorCode("#F0F8FF");
        public static ColorCode AntiqueWhite = new ColorCode("#FAEBD7");
        public static ColorCode Aqua = new ColorCode("#00FFFF");
        public static ColorCode Aquamarine = new ColorCode("#7FFFD4");
        public static ColorCode Azure = new ColorCode("#F0FFFF");
        public static ColorCode Beige = new ColorCode("#F5F5DC");
        public static ColorCode Bisque = new ColorCode("#FFE4C4");
        public static ColorCode Black = new ColorCode("#000000");
        public static ColorCode BlanchedAlmond = new ColorCode("#FFEBCD");
        public static ColorCode Blue = new ColorCode("#0000FF");
        public static ColorCode BlueViolet = new ColorCode("#8A2BE2");
        public static ColorCode Brown = new ColorCode("#A52A2A");
        public static ColorCode BurlyWood = new ColorCode("#DEB887");
        public static ColorCode CadetBlue = new ColorCode("#5F9EA0");
        public static ColorCode Chartreuse = new ColorCode("#7FFF00");
        public static ColorCode Chocolate = new ColorCode("#D2691E");
        public static ColorCode Coral = new ColorCode("#FF7F50");
        public static ColorCode CornflowerBlue = new ColorCode("#6495ED");
        public static ColorCode Cornsilk = new ColorCode("#FFF8DC");
        public static ColorCode Crimson = new ColorCode("#DC143C");
        public static ColorCode Cyan = new ColorCode("#00FFFF");
        public static ColorCode DarkBlue = new ColorCode("#00008B");
        public static ColorCode DarkCyan = new ColorCode("#008B8B");
        public static ColorCode DarkGoldenRod = new ColorCode("#B8860B");
        public static ColorCode DarkGray = new ColorCode("#A9A9A9");
        public static ColorCode DarkGrey = new ColorCode("#A9A9A9");
        public static ColorCode DarkGreen = new ColorCode("#006400");
        public static ColorCode DarkKhaki = new ColorCode("#BDB76B");
        public static ColorCode DarkMagenta = new ColorCode("#8B008B");
        public static ColorCode DarkOliveGreen = new ColorCode("#556B2F");
        public static ColorCode DarkOrange = new ColorCode("#FF8C00");
        public static ColorCode DarkOrchid = new ColorCode("#9932CC");
        public static ColorCode DarkRed = new ColorCode("#8B0000");
        public static ColorCode DarkSalmon = new ColorCode("#E9967A");
        public static ColorCode DarkSeaGreen = new ColorCode("#8FBC8F");
        public static ColorCode DarkSlateBlue = new ColorCode("#483D8B");
        public static ColorCode DarkSlateGray = new ColorCode("#2F4F4F");
        public static ColorCode DarkSlateGrey = new ColorCode("#2F4F4F");
        public static ColorCode DarkTurquoise = new ColorCode("#00CED1");
        public static ColorCode DarkViolet = new ColorCode("#9400D3");
        public static ColorCode DeepPink = new ColorCode("#FF1493");
        public static ColorCode DeepSkyBlue = new ColorCode("#00BFFF");
        public static ColorCode DimGray = new ColorCode("#696969");
        public static ColorCode DimGrey = new ColorCode("#696969");
        public static ColorCode DodgerBlue = new ColorCode("#1E90FF");
        public static ColorCode FireBrick = new ColorCode("#B22222");
        public static ColorCode FloralWhite = new ColorCode("#FFFAF0");
        public static ColorCode ForestGreen = new ColorCode("#228B22");
        public static ColorCode Fuchsia = new ColorCode("#FF00FF");
        public static ColorCode Gainsboro = new ColorCode("#DCDCDC");
        public static ColorCode GhostWhite = new ColorCode("#F8F8FF");
        public static ColorCode Gold = new ColorCode("#FFD700");
        public static ColorCode GoldenRod = new ColorCode("#DAA520");
        public static ColorCode Gray = new ColorCode("#808080");
        public static ColorCode Grey = new ColorCode("#808080");
        public static ColorCode Green = new ColorCode("#008000");
        public static ColorCode GreenYellow = new ColorCode("#ADFF2F");
        public static ColorCode HoneyDew = new ColorCode("#F0FFF0");
        public static ColorCode HotPink = new ColorCode("#FF69B4");
        public static ColorCode IndianRed = new ColorCode("#CD5C5C");
        public static ColorCode Indigo = new ColorCode("#4B0082");
        public static ColorCode Ivory = new ColorCode("#FFFFF0");
        public static ColorCode Khaki = new ColorCode("#F0E68C");
        public static ColorCode Lavender = new ColorCode("#E6E6FA");
        public static ColorCode LavenderBlush = new ColorCode("#FFF0F5");
        public static ColorCode LawnGreen = new ColorCode("#7CFC00");
        public static ColorCode LemonChiffon = new ColorCode("#FFFACD");
        public static ColorCode LightBlue = new ColorCode("#ADD8E6");
        public static ColorCode LightCoral = new ColorCode("#F08080");
        public static ColorCode LightCyan = new ColorCode("#E0FFFF");
        public static ColorCode LightGoldenRodYellow = new ColorCode("#FAFAD2");
        public static ColorCode LightGray = new ColorCode("#D3D3D3");
        public static ColorCode LightGrey = new ColorCode("#D3D3D3");
        public static ColorCode LightGreen = new ColorCode("#90EE90");
        public static ColorCode LightPink = new ColorCode("#FFB6C1");
        public static ColorCode LightSalmon = new ColorCode("#FFA07A");
        public static ColorCode LightSeaGreen = new ColorCode("#20B2AA");
        public static ColorCode LightSkyBlue = new ColorCode("#87CEFA");
        public static ColorCode LightSlateGray = new ColorCode("#778899");
        public static ColorCode LightSlateGrey = new ColorCode("#778899");
        public static ColorCode LightSteelBlue = new ColorCode("#B0C4DE");
        public static ColorCode LightYellow = new ColorCode("#FFFFE0");
        public static ColorCode Lime = new ColorCode("#00FF00");
        public static ColorCode LimeGreen = new ColorCode("#32CD32");
        public static ColorCode Linen = new ColorCode("#FAF0E6");
        public static ColorCode Magenta = new ColorCode("#FF00FF");
        public static ColorCode Maroon = new ColorCode("#800000");
        public static ColorCode MediumAquaMarine = new ColorCode("#66CDAA");
        public static ColorCode MediumBlue = new ColorCode("#0000CD");
        public static ColorCode MediumOrchid = new ColorCode("#BA55D3");
        public static ColorCode MediumPurple = new ColorCode("#9370DB");
        public static ColorCode MediumSeaGreen = new ColorCode("#3CB371");
        public static ColorCode MediumSlateBlue = new ColorCode("#7B68EE");
        public static ColorCode MediumSpringGreen = new ColorCode("#00FA9A");
        public static ColorCode MediumTurquoise = new ColorCode("#48D1CC");
        public static ColorCode MediumVioletRed = new ColorCode("#C71585");
        public static ColorCode MidnightBlue = new ColorCode("#191970");
        public static ColorCode MintCream = new ColorCode("#F5FFFA");
        public static ColorCode MistyRose = new ColorCode("#FFE4E1");
        public static ColorCode Moccasin = new ColorCode("#FFE4B5");
        public static ColorCode NavajoWhite = new ColorCode("#FFDEAD");
        public static ColorCode Navy = new ColorCode("#000080");
        public static ColorCode OldLace = new ColorCode("#FDF5E6");
        public static ColorCode Olive = new ColorCode("#808000");
        public static ColorCode OliveDrab = new ColorCode("#6B8E23");
        public static ColorCode Orange = new ColorCode("#FFA500");
        public static ColorCode OrangeRed = new ColorCode("#FF4500");
        public static ColorCode Orchid = new ColorCode("#DA70D6");
        public static ColorCode PaleGoldenRod = new ColorCode("#EEE8AA");
        public static ColorCode PaleGreen = new ColorCode("#98FB98");
        public static ColorCode PaleTurquoise = new ColorCode("#AFEEEE");
        public static ColorCode PaleVioletRed = new ColorCode("#DB7093");
        public static ColorCode PapayaWhip = new ColorCode("#FFEFD5");
        public static ColorCode PeachPuff = new ColorCode("#FFDAB9");
        public static ColorCode Peru = new ColorCode("#CD853F");
        public static ColorCode Pink = new ColorCode("#FFC0CB");
        public static ColorCode Plum = new ColorCode("#DDA0DD");
        public static ColorCode PowderBlue = new ColorCode("#B0E0E6");
        public static ColorCode Purple = new ColorCode("#800080");
        public static ColorCode RebeccaPurple = new ColorCode("#663399");
        public static ColorCode Red = new ColorCode("#FF0000");
        public static ColorCode RosyBrown = new ColorCode("#BC8F8F");
        public static ColorCode RoyalBlue = new ColorCode("#4169E1");
        public static ColorCode SaddleBrown = new ColorCode("#8B4513");
        public static ColorCode Salmon = new ColorCode("#FA8072");
        public static ColorCode SandyBrown = new ColorCode("#F4A460");
        public static ColorCode SeaGreen = new ColorCode("#2E8B57");
        public static ColorCode SeaShell = new ColorCode("#FFF5EE");
        public static ColorCode Sienna = new ColorCode("#A0522D");
        public static ColorCode Silver = new ColorCode("#C0C0C0");
        public static ColorCode SkyBlue = new ColorCode("#87CEEB");
        public static ColorCode SlateBlue = new ColorCode("#6A5ACD");
        public static ColorCode SlateGray = new ColorCode("#708090");
        public static ColorCode SlateGrey = new ColorCode("#708090");
        public static ColorCode Snow = new ColorCode("#FFFAFA");
        public static ColorCode SpringGreen = new ColorCode("#00FF7F");
        public static ColorCode SteelBlue = new ColorCode("#4682B4");
        public static ColorCode Tan = new ColorCode("#D2B48C");
        public static ColorCode Teal = new ColorCode("#008080");
        public static ColorCode Thistle = new ColorCode("#D8BFD8");
        public static ColorCode Tomato = new ColorCode("#FF6347");
        public static ColorCode Turquoise = new ColorCode("#40E0D0");
        public static ColorCode Violet = new ColorCode("#EE82EE");
        public static ColorCode Wheat = new ColorCode("#F5DEB3");
        public static ColorCode White = new ColorCode("#FFFFFF");
        public static ColorCode WhiteSmoke = new ColorCode("#F5F5F5");
        public static ColorCode Yellow = new ColorCode("#FFFF00");
        public static ColorCode YellowGreen = new ColorCode("#9ACD32");


    }
}
