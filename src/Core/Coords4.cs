using System.Globalization;

namespace WowTools.Core
{
    /// <summary>
    ///  Represents a coordinates of WoW object with specified orientation.
    /// </summary>
    public struct Coords4
    {
        public float X;
        public float Y;
        public float Z;
        public float O;

        /// <summary>
        ///  Converts the numeric values of this instance to its equivalent string representations, separator is space.
        /// </summary>
        public override string ToString()
        {
            return string.Format(CultureInfo.InvariantCulture, "{0} {1} {2} {3}", X, Y, Z, O);
        }
    }
}
