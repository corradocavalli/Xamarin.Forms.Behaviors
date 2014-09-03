
namespace Xamarin.Behaviors
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Linq.Expressions;
	using System.Reflection;
	using System.Windows.Input;
	using Xamarin.Forms;

	/// <summary>
	/// Invoked a command when an event raises
	/// </summary>
	public class EventToCommand : Behavior
	{
		public static readonly BindableProperty EventNameProperty = BindableProperty.Create<EventToCommand, string>(p => p.EventName, null);
		public static readonly BindableProperty CommandProperty = BindableProperty.Create<EventToCommand, ICommand>(p => p.Command, null);
		public static readonly BindableProperty CommandParameterProperty = BindableProperty.Create<EventToCommand, object>(p => p.CommandParameter, null);

		private Delegate handler;
		private EventInfo eventInfo;

		/// <summary>
		/// Gets or sets the name of the event to subscribe
		/// </summary>
		/// <value>
		/// The name of the event.
		/// </value>
		public string EventName
		{
			get { return (string)GetValue(EventNameProperty); }
			set { SetValue(EventNameProperty, value); }
		}

		/// <summary>
		/// Gets or sets the command to invoke when event raised
		/// </summary>
		/// <value>
		/// The command.
		/// </value>
		public ICommand Command
		{
			get { return (ICommand)GetValue(CommandProperty); }
			set { SetValue(CommandProperty, value); }
		}

		/// <summary>
		/// Gets or sets the optional command parameter.
		/// </summary>
		/// <value>
		/// The command parameter.
		/// </value>
		public object CommandParameter
		{
			get { return GetValue(CommandParameterProperty); }
			set { SetValue(CommandParameterProperty, value); }
		}

		protected override void OnAttach()
		{
			var events = this.AssociatedObject.GetType().GetRuntimeEvents();
			if (events.Any())
			{
				this.eventInfo = events.FirstOrDefault(e => e.Name == this.EventName);
				if (this.eventInfo == null) throw new ArgumentException(string.Format("EventToCommand: Can't find any event named '{0}' on attached type"));
				this.AddEventHandler(eventInfo, this.AssociatedObject, this.OnFired);
			}
		}


		protected override void OnDetach()
		{
			if (this.handler != null) this.eventInfo.RemoveEventHandler(this.AssociatedObject, this.handler);
		}

		/// <summary>
		/// Subscribes the event handler.
		/// </summary>
		/// <param name="eventInfo">The event information.</param>
		/// <param name="item">The item.</param>
		/// <param name="action">The action.</param>
		private void AddEventHandler(EventInfo eventInfo, object item, Action action)
		{
			//Got inspiration from here: http://stackoverflow.com/questions/9753366/subscribing-an-action-to-any-event-type-via-reflection
			//Maybe it is possible to pass Event arguments as CommanParameter

			var mi = eventInfo.EventHandlerType.GetRuntimeMethods().First(rtm => rtm.Name == "Invoke");
			List<ParameterExpression> parameters = mi.GetParameters().Select(p => Expression.Parameter(p.ParameterType)).ToList();
			MethodInfo actionMethodInfo = action.GetMethodInfo();
			Expression exp = Expression.Call(Expression.Constant(this), actionMethodInfo, null);
			this.handler = Expression.Lambda(eventInfo.EventHandlerType, exp, parameters).Compile();
			eventInfo.AddEventHandler(item, handler);
		}

		/// <summary>
		/// Called when subscribed event fires
		/// </summary>
		private void OnFired()
		{
			if (this.Command != null && this.Command.CanExecute(this.CommandParameter))
			{
				this.Command.Execute(this.CommandParameter);
			}
		}
	}
}
