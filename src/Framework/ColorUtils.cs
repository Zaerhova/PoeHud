﻿using System;
using System.Drawing;

namespace PoeHUD.Framework
{
	public static class ColorUtils
	{
		public static void ColorToHsv(Color color, out double hue, out double saturation, out double value)
		{
			int max = Math.Max(color.R, Math.Max(color.G, color.B));
			int min = Math.Min(color.R, Math.Min(color.G, color.B));

			hue = color.GetHue();
			saturation = (max == 0) ? 0 : 1d - (1d * min / max);
			value = max / 255d;
		}

		public static Color ColorFromHsv(double hue, double saturation, double value)
		{
			int hi = Convert.ToInt32(Math.Floor(hue / 60)) % 6;
			double f = hue / 60 - Math.Floor(hue / 60);

			value = value * 255;
			int v = Convert.ToInt32(value);
			int p = Convert.ToInt32(value * (1 - saturation));
			int q = Convert.ToInt32(value * (1 - f * saturation));
			int t = Convert.ToInt32(value * (1 - (1 - f) * saturation));

			switch (hi)
			{
				case 0:
					return Color.FromArgb(255, v, t, p);
				case 1:
					return Color.FromArgb(255, q, v, p);
				case 2:
					return Color.FromArgb(255, p, v, t);
				case 3:
					return Color.FromArgb(255, p, q, v);
				case 4:
					return Color.FromArgb(255, t, p, v);
				default:
					return Color.FromArgb(255, v, p, q);
			}
		}

		/// <summary>
		/// Returns the value for the requested primary color from the Color object.
		/// </summary>
		/// <param name="primaryColor">The primary color to return a value for.  Must be one of: Color.Red, Color.Green, Color.Blue.</param>
		/// <returns>Color value between 0 and 255</returns>
		public static int PrimaryColorValue(this Color _color, Color primaryColor)
		{
			if (primaryColor.Equals(Color.Red))
				return _color.R;
			if (primaryColor.Equals(Color.Green))
				return _color.G;
			if (primaryColor.Equals(Color.Blue))
				return _color.B;

			throw new ArgumentOutOfRangeException("Supplied color is not one of the valid colors");
		}
	}
}
