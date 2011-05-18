using System.Globalization;

namespace WowTools.Core
{
    /// <summary>
    ///  Represents a coordinates of WoW object without orientation.
    /// </summary>
    public struct Coords3
    {
        public float X;
        public float Y;
        public float Z;

        /// <summary>
        ///  Converts the numeric values of this instance to its equivalent string representations, separator is space.
        /// </summary>
        public override string ToString()
        {
            return string.Format(CultureInfo.InvariantCulture, "{0} {1} {2}", X, Y, Z);
        }
    }
}
