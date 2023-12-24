using System;
using System.Collections.Generic;

public static class ServiceLocator
{
    private static readonly Dictionary<Type, object> Services = new();

    public static T GetService<T>() where T : class
    {
        var serviceType = typeof(T);
        if (Services.TryGetValue(serviceType, out var service))
        {
            return (T)service;
        }
        return TryCreateService<T>(serviceType);
    }

    private static T TryCreateService<T>(Type serviceType) where T : class
    {
        var serviceInstance = Activator.CreateInstance<T>();
        Services[serviceType] = serviceInstance;
        return serviceInstance;
    }
}