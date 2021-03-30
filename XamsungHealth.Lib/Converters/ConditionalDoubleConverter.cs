using System;
using System.Globalization;
using Xamarin.CommunityToolkit.Extensions.Internals;
using Xamarin.Forms;

namespace XamsungHealth.Lib.Converters
{
	public class ConditionalDoubleConverter : ConditionalDoubleConverter<object>
	{
	}

	public class ConditionalDoubleConverter<TObject> : ValueConverterExtension, IValueConverter
	{
		enum Modes
		{
			Bool,
			Object

		}
		Modes Mode = Modes.Bool;

		public double? Value { get; set; }
		public string? Condition { get; set; }
		public TObject? TrueObject { get; set; }
		public TObject? FalseObject { get; set; }

		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			if (Condition == null || Value == null)
				throw new ArgumentNullException("Value and Condition parameters shouldn't be null");

			if (value is not double)
				throw new ArgumentNullException("value is expected to be of type double");

			if (!(TrueObject == null ^ FalseObject != null))
				throw new ArgumentNullException("Both TrueObject and FalseObject should be defined or omited");

			if (TrueObject != null)
				Mode = Modes.Object;

			return (Condition) switch
			{
				"<" => EvaluateCondition((double)value < Value),

				"<=" => EvaluateCondition((double)value <= Value),
				"==" => EvaluateCondition((double)value == Value),
				"!=" => EvaluateCondition((double)value != Value),
				">=" => EvaluateCondition((double)value >= Value),
				">" => EvaluateCondition((double)value > Value),
				_ => throw new ArgumentNullException("Condition {0} Not found.", Condition),
			};
		}

		private object EvaluateCondition(bool v)
			=> v ? Mode == Modes.Object ?
					TrueObject! : true
						: Mode == Modes.Object ?
							FalseObject! : false;


		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
				=> throw new NotImplementedException();
	}
}
