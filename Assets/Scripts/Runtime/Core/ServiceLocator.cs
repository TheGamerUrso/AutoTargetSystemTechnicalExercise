using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Simple service locator for dependency management.
/// Allows systems to register and retrieve services without tight coupling.
/// </summary>
public static class ServiceLocator
{
    private static readonly Dictionary<Type, object> services = new Dictionary<Type, object>();

    /// <summary>
    /// Register a service instance.
    /// </summary>
    /// <typeparam name="T">Service interface type.</typeparam>
    /// <param name="service">Service instance to register.</param>
    public static void Register<T>(T service) where T : class
    {
        var type = typeof(T);
        if (services.ContainsKey(type))
        {
            Debug.LogWarning($"Service of type {type.Name} is already registered. Overwriting.");
            services[type] = service;
        }
        else
        {
            services.Add(type, service);
        }
    }

    /// <summary>
    /// Unregister a service.
    /// </summary>
    /// <typeparam name="T">Service interface type.</typeparam>
    public static void Unregister<T>() where T : class
    {
        var type = typeof(T);
        if (services.ContainsKey(type))
        {
            services.Remove(type);
        }
    }

    /// <summary>
    /// Get a registered service instance.
    /// </summary>
    /// <typeparam name="T">Service interface type.</typeparam>
    /// <returns>The service instance, or null if not registered.</returns>
    public static T Get<T>() where T : class
    {
        var type = typeof(T);
        if (services.TryGetValue(type, out var service))
        {
            return service as T;
        }

        Debug.LogWarning($"Service of type {type.Name} not found in ServiceLocator.");
        return null;
    }

    /// <summary>
    /// Check if a service is registered.
    /// </summary>
    /// <typeparam name="T">Service interface type.</typeparam>
    /// <returns>True if the service is registered.</returns>
    public static bool IsRegistered<T>() where T : class
    {
        return services.ContainsKey(typeof(T));
    }

    /// <summary>
    /// Clear all registered services. Useful for cleanup between scenes.
    /// </summary>
    public static void Clear()
    {
        services.Clear();
    }
}
