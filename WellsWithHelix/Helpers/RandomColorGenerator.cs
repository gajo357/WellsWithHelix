using System;
using System.Windows.Media;

namespace WellsWithHelix.Helpers
{
    public class RandomColorGenerator
    {
        private static Random _random = new Random();
        internal static Color GenerateColor()
        {
            return Color.FromRgb((byte)_random.Next(1, 255), (byte)_random.Next(1, 255), (byte)_random.Next(1, 233));
        }
    }
}
