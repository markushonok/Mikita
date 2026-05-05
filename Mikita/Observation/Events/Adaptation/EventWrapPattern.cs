namespace Mikita.Observation.Events.Adaptation;

public delegate TFrom EventWrapPattern<out TFrom, in TTo>(TTo target);