using System;
using System.Collections.Generic;
using UnityEngine;

namespace Fakejam.Input
{
    public class Toolbox : MonoBehaviour
    {
        private static Toolbox _instance;
        
        private readonly List<object> _allObjects = new List<object>();
        private readonly Dictionary<Type, object> _cachedObjectMap = new Dictionary<Type, object>();

        private void Awake()
        {
            if (_instance != this)
            {
                Destroy(this);
                return;
            }

            _instance = this;
            DontDestroyOnLoad(this);
        }

        public static void Add(object accessibleObject)
        {
            _instance.AddInternal(accessibleObject);
        }
        
        public static T Get<T>() where T : class
        {
            return _instance.GetInternal<T>();
        }

        private void AddInternal(object accessibleObject)
        {
            if (accessibleObject is MonoBehaviour monoBehaviour)
            {
                monoBehaviour.transform.SetParent(transform, false);
            }
            _allObjects.Add(accessibleObject);
        }

        private T GetInternal<T>() where T : class
        {
            var type = typeof(T);
            if (_cachedObjectMap.TryGetValue(type, out object value))
            {
                return (T) value;
            }

            foreach (object element in _allObjects)
            {
                if (element is T tElement)
                {
                    _cachedObjectMap[type] = tElement;
                    return tElement;
                }
            }

            return null;
        }
    }
}