using Mikita.Observation.Events.Sources;
using Mikita.Routines.Assignment;
using Mikita.Steps;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Mikita.Observation.Events;

public static class Event<T>
	where T: class
	{
		public static ReactionCollection<T> NewEmpty
			=> Event.With<T>([]);
	}

public static class Event
	{
		public static EventPattern<T> Pattern<T>
			(
				Action<T> add,
				Action<T> remove
			)
			=> new(add, remove);

		public static ReactionCollection<Action> NewEmpty
			=> Event.With<Action>([]);

		public static ReactionCollection<T> With<T>
			(
				ICollection<T> reactions
			)
			where T: class
			=> new(reactions);
	}

public static class TestClass
	{
		public static void TestMethod()
			{
				var a = Event<IStep>.NewEmpty;
			}
	}