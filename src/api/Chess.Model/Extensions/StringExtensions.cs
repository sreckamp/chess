using System;
using Chess.Model.Models;

namespace Chess.Model.Extensions
{
    public static class StringExtensions
    {
        public static Color ToColor(this string name)
        {
            if ((name?.Length ?? 0) < 2) return Color.None;

            name = char.ToUpper(name[0]) + name[1..].ToLower();
            return Enum.Parse<Color>(name);
        }

        public static PieceType ToPieceType(this string name)
        {
            if ((name?.Length ?? 0) < 2) return PieceType.Empty;

            name = char.ToUpper(name[0]) + name[1..].ToLower();
            return Enum.Parse<PieceType>(name);
        }
    }
}