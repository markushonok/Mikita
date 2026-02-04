using System;

namespace Mikita.Observation.Events.Raising;

public delegate void EventRaise<out T>(Action<T> pattern);