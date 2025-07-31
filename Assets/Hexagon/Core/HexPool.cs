#nullable enable

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using UnityEngine.Assertions;

public sealed class Pool<T> where T : class
{
    private readonly List<T> _items;

    private readonly Func<T> _factory;
    private readonly Func<T, bool> _isActive;
    private readonly Action<T> _onGet;
    private readonly Action<T> _onRelease;

    public int TotalCount => _items.Count;
    public int InactiveCount => TotalCount - ActiveCount;
    public int ActiveCount => activeCount;

    private int activeCount = 0;

    public static Pool<T> Create(Func<T> factory, Action<T> onGet, Action<T> onRelease, Func<T, bool> isActive, int initialCapacity = 8, int listCapacity = 20)
    {
        Pool<T> pool = new Pool<T>(factory, onGet, onRelease, isActive, listCapacity);
        return pool;
    }

    internal Pool(Func<T> factory, Action<T> onGet, Action<T> onRelease, Func<T, bool> isActive, int listCapacity)
    {
        _factory = factory;
        _onGet = onGet;
        _onRelease = onRelease;
        _isActive = isActive;

        _items = new List<T>(listCapacity);
    }

    public void Populate(int delta)
    {
        for (int i = 0; i < delta; i++)
        {
            T item = _factory();
            _items.Add(item);
        }
    }

    public T Get()
    {
        T? result = null;
        bool exists = ViewInactive(out result);
        if (!exists)
        {
            result = _factory();
            _items.Add(result);
        }
        return Get(result!);
    }

    public T Get(T item)
    {
        Assert.IsTrue(_items.Contains(item));
        activeCount--;
        _onGet?.Invoke(item);
        return item;
    }

    public bool ViewInactive([NotNullWhen(true)] out T? result)
    {
        foreach (T item in _items)
        {
            if (!_isActive(item))
            {
                result = item;
                return true;
            }
        }
        result = null;
        return false;
    }

    public bool ViewActive([NotNullWhen(true)] out T? result)
    {
        foreach (T item in _items)
        {
            if (_isActive(item))
            {
                result = item;
                return true;
            }
        }
        result = null;
        return false;
    }

    public void Release(T item)
    {
        activeCount--;
        _onRelease?.Invoke(item);
    }

    public void ReleaseAll()
    {
        foreach (T item in _items)
        {
            if (_isActive(item))
            {
                Release(item);
            }
        }
    }
}