#nullable enable

using System;
using System.Collections.Generic;
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
        T result = Get(PeekInactiveUnsafe());
        return result;
    }

    public T Get(T item)
    {
        activeCount++;
        _onGet?.Invoke(item);
        return item;
    }

    public T PeekInactiveUnsafe()
    {
        T? result = null;
        foreach (T item in _items)
        {
            if (!_isActive(item))
            {
                result = item;
                break;
            }
        }
        if (result == null)
            result = _factory();

        return result;
    }

    public T? PeekActiveUnsafe()
    {
        foreach (T item in _items)
        {
            if (_isActive(item))
            {
                return item;
            }
        }
        return null;
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

    /// <summary>
    /// Leases an item from the pool for inspection.
    /// Must be used in a 'using' block.
    /// </summary>
    public ItemLease<T> LeaseInactive()
    {
        T item = PeekInactiveUnsafe();
        return new ItemLease<T>(item, this);
    }

    public ItemLease<T> LeaseActive()
    {
        T? item = PeekActiveUnsafe();
        return new ItemLease<T>(item, this);
    }
}

/// <summary>
/// Represents a temporary, exclusive lease on a pooled item.
/// Either commit the lease with ConfirmAndGet(), or dispose of it
/// to return the item to the pool automatically.
/// </summary>
public readonly struct ItemLease<T> : IDisposable where T : class
{
    private readonly T? _item;
    private readonly Pool<T> _sourcePool; // Using your updated pool name

    internal ItemLease(T? item, Pool<T> sourcePool)
    {
        _item = item;
        _sourcePool = sourcePool;
    }

    public readonly bool HasValue => _item != null;

    /// <summary>
    /// The leased item, available for inspection.
    /// </summary>
    public readonly T GetReadOnlyItem()
    {
        Assert.IsNotNull(_item, "ItemLease value is null. GetReadOnlyItem must be called after checking HasValue");
        return _item!;
    }

    /// <summary>
    /// Confirms the lease and officially gets the item from the pool.
    /// This prevents the item from being returned upon disposal.
    /// </summary>
    /// <returns>The confirmed item.</returns>
    public T ConfirmAndGet()
    {
        if (HasValue)
        {
            _sourcePool.Get(_item!);
        }
        return _item!;
    }

    public T ConfirmAndRelease()
    {
        if (HasValue)
        {
            _sourcePool.Release(_item!);
        }
        return _item!;
    }

    /// <summary>
    /// If the lease was not confirmed, this automatically returns
    /// the item to the pool.
    /// </summary>
    public void Dispose()
    {
    }
}