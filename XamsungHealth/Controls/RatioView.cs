using Xamarin.CommunityToolkit.Markup;
using Xamarin.Forms;

namespace XamsungHealth.Controls
{
	public class RatioView : ContentView
	{
		public RatioView()
		{
			Content = new StackLayout()
			{
				Orientation = StackOrientation.Horizontal,
				Children = {
					new Label().Bind(Label.TextProperty, source: RelativeBindingSource.TemplatedParent, path: "CurrentNumber").Font(size: 25, bold: true),

					new Label()
					{
						VerticalTextAlignment = TextAlignment.End,
						FormattedText = new FormattedString {
							Spans =
							{
								new Span().Bind(Span.TextProperty, source:
												RelativeBindingSource.TemplatedParent, path: nameof                                 (MainCardView.TotalNumber), stringFormat: "/{0} ").Font(size:10),

								new Span().Bind(Span.TextProperty, source:
										RelativeBindingSource.TemplatedParent, path: nameof(MainCardView.PrefixTotal)).Font(size:10)
							}
						}
					}
				}
			};
		}
	}
}
