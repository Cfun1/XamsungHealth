using System;
using System.Globalization;
using Xamarin.CommunityToolkit.Extensions.Internals;
using Xamarin.Forms;

namespace XamsungHealth.Lib.Converters
{
	/// <summary>
	/// Converts a double to an object or a boolean based on a comparaison.
	/// </summary>
	public class ConditionalDoubleConverter : ConditionalDoubleConverter<object>
	{
	}

	/// <summary>
	/// Converts a double to an object or a boolean based on a comparaison.
	/// </summary>
	public class ConditionalDoubleConverter<TObject> : ValueConverterExtension, IValueConverter
	{
		enum Modes
		{
			Bool,
			Object
		}

		Modes mode = Modes.Bool;

		public double? ComparingValue { get; set; }

		/// <summary>
		/// The object that corresponds to False value.
		/// </summary>
		public string? ComparisonOperator { get; set; }

		/// <summary>
		/// The object that corresponds to True value.
		/// </summary>
		public TObject? TrueObject { get; set; }

		/// <summary>
		/// The object that corresponds to False value.
		/// </summary>
		public TObject? FalseObject { get; set; }

		/// <summary>
		/// Converts a double to an object or a boolean based on a comparaison.
		/// </summary>
		/// <param name="value">The value to convert.</param>
		/// <param name="targetType">The type of the binding target property. This is not implemented.</param>
		/// <param name="parameter">Additional parameter for the converter to handle. This is not implemented.</param>
		/// <param name="culture">The culture to use in the converter.  This is not implemented.</param>
		/// <returns>The object assigned to <see cref="TrueObject"/> if (value <see cref="ComparisonOperator"/> <see cref="ComparingValue"/>) equals True and <see cref="TrueObject"/> is not null, if <see cref="TrueObject"/> is null it returns a true boolean, otherwise the value assigned to <see cref="FalseObject"/>, if no value is assigned thenit return False boolean.</returns>
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			if (ComparisonOperator == null || ComparingValue == null)
			{
				throw new ArgumentNullException(nameof(ComparingValue), $"{nameof(ComparingValue)} and {nameof(ComparisonOperator)} parameters shouldn't be null");
			}

			if (value is not double)
			{
				throw new ArgumentException("is expected to be of type double", nameof(value));
			}

			if (ComparisonOperator is not string)
			{
				throw new ArgumentException("is expected to be of type string", nameof(ComparisonOperator));
			}

			if (!(TrueObject == null ^ FalseObject != null))
			{
				throw new ArgumentNullException(nameof(TrueObject), $"{nameof(TrueObject)} and {nameof(FalseObject)} should be either defined both or omitted both");
			}

			if (TrueObject != null)
			{
				mode = Modes.Object;
			}

			return (ComparisonOperator) switch
			{
				"<" => EvaluateCondition((double)value < ComparingValue),
				"<=" => EvaluateCondition((double)value <= ComparingValue),
				"==" => EvaluateCondition((double)value == ComparingValue),
				"!=" => EvaluateCondition((double)value != ComparingValue),
				">=" => EvaluateCondition((double)value >= ComparingValue),
				">" => EvaluateCondition((double)value > ComparingValue),
				_ => throw new ArgumentNullException(nameof(ComparisonOperator), $"ComparisonOperator \"{ComparisonOperator}\" is not supported. Use <, >, ==, !=, <=, >= instead."),
			};
		}

		private object EvaluateCondition(bool v)
			=> v ? mode == Modes.Object ?
					TrueObject! : true
						: mode == Modes.Object ? FalseObject! : false;

		/// <summary>
		/// This method is not implemented and will throw a <see cref="NotImplementedException"/>.
		/// </summary>
		/// <param name="value">N/A</param>
		/// <param name="targetType">N/A</param>
		/// <param name="parameter">N/A</param>
		/// <param name="culture">N/A</param>
		/// <returns>N/A</returns>
		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
			=> throw new NotImplementedException();
	}
}
