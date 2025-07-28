using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using UnityEditor;

/// <summary>
/// <para>
/// Pool of objects of the same type. Objects can be enabled and disabled dynamically without unnecessary instantiation and destruction hence without much overhead and extra memory allocation. Needs createFunc and isActiveFunc to be assigned before usage. Can work with any object types due to generics and delegate-powered checks.
/// </para>
/// <para>
/// When an object becomes inactive on client (entity died, destroyed, gone off screen, used etc) it should be disabled for the Pool to recognize it as inactive. Then, whenever it is needed, it can be taken as inactive back and reinited and adapted to match new use case and enabled back. Because disabling and enabling an entity is highly game-dependend it all must be done on the game side.
/// </para>
/// </summary>
public class Pool<T>
{
    public readonly List<T> items;

    public bool isEmpty { get { return items.Count == 0; } }

    /// <summary>
    /// Function that will be called upon instantiation of a new object. Is only used in TakeInactiveOrCreate function and its variations.
    /// </summary>
    protected Func<T> factoryFunc;

    /// <summary>
    /// Function that determines whether an object is considered active or not.
    /// </summary>
    protected Func<T, bool> isActiveFunc;

    protected Action<T> onGet;

    protected Action<T> onRelease;

    /// <summary>
    /// </summary>
    /// <param name="isAvailable">Function that determines whether an object is available for getting or not.</param>
    /// <param name="createFunc">Function that will be called on instantiation of a new object. Is only used in TakeInactiveOrCreate function and its variations.</param>
    public Pool(Func<T> factoryFunc, Func<T, bool> isAvailable, Action<T> onGet, Action<T> onRelease, int capacity = 8)
    {
        this.factoryFunc = factoryFunc;
        this.isActiveFunc = isAvailable;
        this.onGet = onGet;
        this.onRelease = onRelease;

        items = new List<T>(capacity);
    }

    public void Populate(int count, bool defaultState = false)
    {
        if (count > 0)
        {
            for (int i = 0; i < count; i++)
            {
                T item = RecordNew(factoryFunc());
                if (defaultState == true) onGet(item);
                if (defaultState == false) onRelease(item);
            }
        }
    }

#nullable enable

    /// <summary>
    /// Returns the first inactive object from pool or default if there is no active object or the pool is empty.
    /// </summary>
    /// <returns></returns>
    public T? PeekInactive() 
    {
        foreach (T item in items)
        {
            if (!isActiveFunc(item))
            {
                return item;
            }   
        }

        return default(T?);
    }

    /// <summary>
    /// Returns the first active object from pool or default if there is no active object or the pool is empty.
    /// </summary>
    public T? PeekActive()
    {
        foreach (T item in items)
        {
            if (isActiveFunc(item))
            {
                return item;
            }
        }

        return default(T?);
    }

    /// <summary>
    ///  Returns the first inactive object from pool which follows condition or null if there is no suitable object or the pool is empty.
    /// </summary>
    /// <param name="condition">Extra condition on each element which determines whether a certain (inactive) object is suitable or not</param>
    public T? PeekInactive(Func<T, bool> condition)
    {
        foreach (T item in items)
        {
            if (!isActiveFunc(item) && condition.Invoke(item))
                return item;
        }

        return default(T?);
    }

    /// <summary>
    /// Returns the first active object from pool which follows condition or null if there is no suitable object or the pool is empty.
    /// </summary>
    /// /// <param name="condition">Extra condition on each element which determines whether a certain (active) object is suitable or not</param>
    public T? PeekActive(Func<T, bool> condition)
    {
        foreach (T item in items)
        {
            if (isActiveFunc(item) && condition.Invoke(item))
                return item;
        }

        return default(T?);
    }

    /// <summary>
    /// Assigns the first inactive object that meets the specified condition to obj, or null if no suitable object is available.
    /// </summary>
    /// <param name="condition">Extra condition on each element which determines whether a certain (inactive) object is suitable or not</param>
    /// <returns>
    /// True if the result object is not null, otherwise false
    /// </returns>
    public bool TryTakeInactive([NotNullWhen(true)] out T? result, Func<T, bool>? condition = null)
    {
        if (condition == null) result = PeekInactive();
        else result = PeekInactive(condition);

        return result != null;
    }

    /// <summary>
    /// Assigns the first active object that meets the specified condition to `obj`, or null if no suitable object is available.
    /// </summary>
    /// <param name="condition">Extra condition on each element which determines whether a certain (active) object is suitable or not</param>
    /// <returns>
    /// True if the result object is not null, otherwise false
    /// </returns>
    public bool TryTakeActive([NotNullWhen(true)] out T? result, Func<T, bool>? condition = null)
    {
        if (condition == null) result = PeekActive();
        else result = PeekActive(condition);

        return result != null;
    }

    /// <summary>
    /// Takes an inactive object from the pool or creates and records a new object using default type contructor.
    /// </summary>
    public T TakeInactiveOrCreate()
    {
        if (TryTakeInactive(out T? result))
            return result;
        T res = factoryFunc.Invoke();
        onGet(res);
        return RecordNew(res);
    }

    /// <summary>
    /// Takes an inactive object from the pool or creates and records a new object using default type contructor.
    /// </summary>
    /// <param name="condition">Extra condition on each element which determines whether a certain (inactive) object is suitable or not</param>
    /// <returns>
    /// Returns an inactive object from the pool or the one created.
    /// </returns>
    public T TakeInactiveOrCreate(Func<T, bool> condition)
    {
        if (TryTakeInactive(out T? result, condition))
            return result;
        T res = factoryFunc.Invoke();
        onGet(res);
        return RecordNew(res);
    }

#nullable restore

    public void Release(T item)
    {
        onRelease(item);
    }

    /// <summary>
    /// Records a new object to the pool without checking whether it is needed.
    /// </summary>
    /// <returns>
    /// Returns the added object back
    /// </returns>
    public T RecordNew(T item)
    {
        items.Add(item);
        return item;
    }

    /// <summary>
    /// Removes the given object 
    /// </summary>
    /// <returns>True if the given object was successfully removed</returns>
    public bool Unrecord(T obj)
    {
        return items.Remove(obj);
    }
}
