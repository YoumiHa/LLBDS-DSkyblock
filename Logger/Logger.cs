using Colorful;
using System;
using System.Drawing;
using System.Collections.Generic;
using System.Linq;
using Console = Colorful.Console;

namespace Skyblock.Logger
{
    internal class logger
    {
        public static void Error<T>(T x)
        {
            var c = DateTime.Now.ToString("T");
            StyleSheet styleSheet = new StyleSheet(Color.FromArgb(255, 204, 204, 204));
            styleSheet.AddStyle("ERROR[a-z]*", Color.Red);
            styleSheet.AddStyle($"{c}", Color.FromArgb(255, 152, 190, 202));
            styleSheet.AddStyle("SkyBlock[a-z]*", Color.Pink);
            Console.WriteLineStyled($"{c} ERROR [SkyBlock] {x}", styleSheet);
        }
        public static void Info<T>(T x)
        {
            var c = DateTime.Now.ToString("T");
            StyleSheet styleSheet = new StyleSheet(Color.FromArgb(255, 204, 204, 204));
            styleSheet.AddStyle("INFO[a-z]*", Color.Green);
            styleSheet.AddStyle("SkyBlock[a-z]*", Color.Pink);
            styleSheet.AddStyle($"{c}", Color.FromArgb(255, 152, 190, 202));
            Console.WriteLineStyled($"{c} INFO [SkyBlock] {x}", styleSheet);
        }
        public static void Warn<T>(T x)
        {
            var c = DateTime.Now.ToString("T");
            StyleSheet styleSheet = new StyleSheet(Color.FromArgb(255, 204, 204, 204));
            styleSheet.AddStyle("WARN[a-z]*", Color.Yellow);
            styleSheet.AddStyle("SkyBlock[a-z]*", Color.Pink);
            styleSheet.AddStyle($"{c}", Color.FromArgb(255, 152, 190, 202));
            Console.WriteLineStyled($"{c} WARN [SkyBlock] {x}", styleSheet);
        }
    }
}
