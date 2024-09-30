using System.Collections.Generic;
using System.Linq;

/// <summary>
/// Pool of objects of the same type. Objects can be enabled and disabled dynamically without unnecessary instantiation and destruction.
/// </summary>
public class Pool<T> where T : IPoolable, new()
{
    private readonly List<T> objects = new List<T>();
    public bool isActiveByDefault = true;

    public bool isEmpty { get { return objects.Count == 0; } }

    public T At(int index) =>
        index >= 0 ? objects[index] : default;

    /// <summary>
    /// Returns the first inactive object from pool or null if there is no active object or the pool is empty.
    /// </summary>
    /// <returns></returns>
    public T TakeInactive() => objects.FirstOrDefault(o => !o.isActiveInPool);

    /// <summary>
    /// Returns the first active object from pool or null if there is no active object or the pool is empty.
    /// </summary>
    public T TakeActive() => objects.FirstOrDefault(o => o.isActiveInPool);

    /// <summary>
    /// Puts the first inactive object from pool into out "obj" variable.
    /// </summary>
    /// <returns>True if the result object is not null, otherwise false</returns>
    public bool TryTakeInactive(out T obj)
    {
        obj = objects.FirstOrDefault(o => !o.isActiveInPool);
        return obj != null;
    }

    /// <summary>
    /// Puts the first active object from pool into out "obj" variable.
    /// </summary>
    /// <returns>True if the result object is not null, otherwise false</returns>
    public bool TryTakeActive(out T obj)
    {
        obj = objects.FirstOrDefault(o => o.isActiveInPool);
        return obj != null;
    }

    /// <summary>
    /// Takes an inactive object from the pool or creates and records a new object using default type contructor.
    /// </summary>
    public T TakeInactiveOrCreate()
    {
        if (TryTakeInactive(out T obj))
            return obj;
        T res = new T();
        return RecordNew(res);
    }

    /// <summary>
    /// Records a new object to the pool without checking if it is needed. Sets the default state.
    /// </summary>
    public T RecordNew(T newObj)
    {
        if (isActiveByDefault) newObj.EnableInPool();
        else newObj.DisableInPool();
        objects.Add(newObj);
        return newObj;
    }
}

/// <summary>
/// Pools allow only objects implementing this interface. IPoolable objects can be Enabled and Disabled rather than instantiated and destroyed. For instantiation all Pool elements must have constructors though.
/// </summary>
public interface IPoolable
{
    bool isActiveInPool { get; set; }
    public void EnableInPool();
    public void DisableInPool();
}