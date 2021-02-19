using System;
using System.Collections.Generic;

public static class EventBroker
{
    private static Dictionary<Event, List<Action>> Events { get; }

    static EventBroker()
    {
        Events = new Dictionary<Event, List<Action>>();
    }

    public static void Subscribe(Event @event, Action action)
    {
        if (!Events.TryGetValue(@event, out List<Action> actions))
        {
            actions = new List<Action>();
        }
        actions.Add(action);
        Events[@event] = actions;
    }

    public static void Trigger(Event @event)
    {
        if (Events.TryGetValue(@event, out List<Action> actions))
        {
            foreach (Action action in actions)
            {
                action?.Invoke();
            }
        }
    }

    public static void Unsubscribe(Event @event, Action action)
    {
        if (!Events.TryGetValue(@event, out List<Action> actions))
        {
            actions = new List<Action>();
        }
        _ = actions.Remove(action);
        Events[@event] = actions;
    }
}

public static class EventBroker<TEventArgs>
{
    private static Dictionary<EventWithArgs, List<Action<TEventArgs>>> Events { get; }

    static EventBroker()
    {
        Events = new Dictionary<EventWithArgs, List<Action<TEventArgs>>>();
    }

    public static void Subscribe(EventWithArgs eventWithArgs, Action<TEventArgs> action)
    {
        if (!Events.TryGetValue(eventWithArgs, out List<Action<TEventArgs>> actions))
        {
            actions = new List<Action<TEventArgs>>();
        }
        actions.Add(action);
        Events[eventWithArgs] = actions;
    }

    public static void Trigger(EventWithArgs eventWithArgs, TEventArgs eventArg)
    {
        if (Events.TryGetValue(eventWithArgs, out List<Action<TEventArgs>> actions))
        {
            foreach (Action<TEventArgs> action in actions)
            {
                action?.Invoke(eventArg);
            }
        }
    }

    public static void Unsubscribe(EventWithArgs eventWithArgs, Action<TEventArgs> action)
    {
        if (!Events.TryGetValue(eventWithArgs, out List<Action<TEventArgs>> actions))
        {
            actions = new List<Action<TEventArgs>>();
        }
        _ = actions.Remove(action);
        Events[eventWithArgs] = actions;
    }
}
