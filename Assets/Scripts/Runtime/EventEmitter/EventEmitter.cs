using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EventEmitter : MonoBehaviour
{
    private static EventEmitter _instance;
    public static EventEmitter Instance
    {
        get
        {
            if (_instance == null)
            {
                GameObject go = new GameObject("EventEmitter");
                _instance = go.AddComponent<EventEmitter>();
                DontDestroyOnLoad(go);
            }
            return _instance;
        }
    }

    private Dictionary<EGlobalEvent, UnityEventBase> eventDictionary;

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
            Init();
        }
        else if (_instance != this)
        {
            Destroy(gameObject);
        }
    }

    private void Init()
    {
        if (eventDictionary == null)
        {
            eventDictionary = new Dictionary<EGlobalEvent, UnityEventBase>();
        }
    }

    public void On(EGlobalEvent eventName, UnityAction listener)
    {
        Off(eventName, listener);

        if (eventDictionary.TryGetValue(eventName, out UnityEventBase thisEventBase))
        {
            UnityEvent thisEvent = thisEventBase as UnityEvent;
            thisEvent?.AddListener(listener);
        }
        else
        {
            UnityEvent thisEvent = new UnityEvent();
            thisEvent.AddListener(listener);
            eventDictionary.Add(eventName, thisEvent);
        }
    }

    public void On<T>(EGlobalEvent eventName, UnityAction<T> listener)
    {
        Off(eventName, listener);

        if (eventDictionary.TryGetValue(eventName, out UnityEventBase thisEventBase))
        {
            UnityEvent<T> thisEvent = thisEventBase as UnityEvent<T>;
            thisEvent?.AddListener(listener);
        }
        else
        {
            UnityEvent<T> thisEvent = new UnityEvent<T>();
            thisEvent.AddListener(listener);
            eventDictionary.Add(eventName, thisEvent);
        }
    }

    public void Once(EGlobalEvent eventName, UnityAction listener)
    {
        UnityAction wrapper = null;
        wrapper = () =>
        {
            listener.Invoke();
            Off(eventName, wrapper);
        };
        On(eventName, wrapper);
    }

    public void Once<T>(EGlobalEvent eventName, UnityAction<T> listener)
    {
        UnityAction<T> wrapper = null;
        wrapper = (param) =>
        {
            listener.Invoke(param);
            Off(eventName, wrapper);
        };
        On(eventName, wrapper);
    }

    public void Off(EGlobalEvent eventName, UnityAction listener)
    {
        if (eventDictionary.TryGetValue(eventName, out UnityEventBase thisEventBase))
        {
            UnityEvent thisEvent = thisEventBase as UnityEvent;
            thisEvent?.RemoveListener(listener);
        }
    }

    public void Off<T>(EGlobalEvent eventName, UnityAction<T> listener)
    {
        if (eventDictionary.TryGetValue(eventName, out UnityEventBase thisEventBase))
        {
            UnityEvent<T> thisEvent = thisEventBase as UnityEvent<T>;
            thisEvent?.RemoveListener(listener);
        }
    }

    public void Emit(EGlobalEvent eventName)
    {
        if (eventDictionary.TryGetValue(eventName, out UnityEventBase thisEventBase))
        {
            UnityEvent thisEvent = thisEventBase as UnityEvent;
            thisEvent?.Invoke();
        }
    }

    public void Emit<T>(EGlobalEvent eventName, T param)
    {
        if (eventDictionary.TryGetValue(eventName, out UnityEventBase thisEventBase))
        {
            UnityEvent<T> thisEvent = thisEventBase as UnityEvent<T>;
            thisEvent?.Invoke(param);
        }
    }
}