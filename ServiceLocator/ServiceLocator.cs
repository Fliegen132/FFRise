using System;
using System.Collections.Generic;
using UnityEngine;

public class ServiceLocator 
{
    private readonly Dictionary<string, IService> _services = new Dictionary<string, IService>();

    public static ServiceLocator Current = null;
    public ServiceLocator()
    {
        if (Current != null)
        {
            return;
        }
        Current = this;
    }

    public void Register<T>(T service) where T : IService
    {
        string key = typeof(T).Name;
        if (_services.ContainsKey(key))
        {
            Debug.Log($"{key} уже зарегестрирован");
            return;
        }
        _services.Add(key, service);
    }

    public T Get<T>() where T : IService
    {
        string key = typeof(T).Name;
        if (!_services.ContainsKey(key))
        {
            Debug.Log($"{key} еще не зарегестрирован");
            throw new InvalidOperationException();
        }

        return (T)_services[key];
    }

    public void UnregisterAll()
    {
        _services.Clear();
    }
}
