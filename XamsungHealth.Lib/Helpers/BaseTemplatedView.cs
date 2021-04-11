using System;
using Xamarin.Forms;

//Adapted from https://github.com/xamarin/XamarinCommunityToolkit/blob/main/src/CommunityToolkit/Xamarin.CommunityToolkit/Views/BaseTemplatedView.shared.cs
namespace XamsungHealth.Lib
{
	/// <summary>
	/// Abstract class that templated views should inherit
	/// </summary>
	/// <typeparam name="TControl">The type of the control that this template will be used for</typeparam>
	public abstract class BaseTemplatedView<TControl> : TemplatedView where TControl : View, new()
	{
		TControl? control;

		protected TControl Control => control ?? throw new NullReferenceException();

		/// <summary>
		/// Constructor of <see cref="BaseTemplatedView{TControl}" />
		/// </summary>
		public BaseTemplatedView() : this(null)
		{
		}

		public BaseTemplatedView(params object[]? parameters)
			=> InitControlTemplate(parameters);

		public virtual void InitControlTemplate(params object[]? parameters)
		{
			if (parameters == null)
			{
				ControlTemplate = new ControlTemplate(typeof(TControl));
			}
			else
			{
				ControlTemplate = new ControlTemplate(() => { return Activator.CreateInstance(typeof(TControl), parameters); });
			}
		}

		protected override void OnBindingContextChanged()
		{
			base.OnBindingContextChanged();

			if (control != null)
				Control.BindingContext = BindingContext;
		}

		protected override void OnChildAdded(Element child)
		{
			if (control == null && child is TControl content)
			{
				control = content;
				OnControlInitialized(Control);
			}

			base.OnChildAdded(child);
		}

		protected abstract void OnControlInitialized(TControl control);
	}
}