﻿using Xamarin.Forms;

namespace XamsungHealth.Lib
{
	public interface IColor
	{
		Color Color { get; }
	}

	public static class ColorElement
	{
		public static readonly BindableProperty ColorProperty =
			BindableProperty.Create(nameof(IColor.Color), typeof(Color), typeof(IColor), Color.Default);
	}
}