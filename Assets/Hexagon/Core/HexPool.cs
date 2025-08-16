#nullable enable

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using UnityEngine.Assertions;

namespace Hexagon
{
    /// <summary>
    /// Manages a collection of objects that can be reused, tracking their state via a delegate.
    /// This implementation relies on an external function to determine if an object is active or inactive.
    /// Operations that search for objects, such as <see cref="Get()"/> or <see cref="ViewExistingInactive(out T?)"/>, have a performance cost that scales linearly with the total number of items.
    /// <br/>
    /// See @ref hexagon_pool_notes Hexagon Pool Notes
    /// </summary>
    /// <typeparam name="T">The type of object to be managed. Must be a class.</typeparam>
    public sealed class Pool<T> where T : class
    {
        private readonly List<T> _items;

        private readonly Func<T> _factory;
        private readonly Func<T, bool> _isActive;
        private readonly Action<T> _onGet;
        private readonly Action<T> _onRelease;

        /// <summary>
        /// The total number of objects currently managed by the pool, both active and inactive.
        /// </summary>
        public int TotalCount => _items.Count;

        /// <summary>
        /// The current number of inactive objects in the pool.
        /// </summary>
        public int InactiveCount => TotalCount - ActiveCount;

        /// <summary>
        /// The current number of objects considered active by the pool.
        /// </summary>
        public int ActiveCount => activeCount;

        private int activeCount = 0;

        /// <summary>
        /// Creates and initializes a new instance of the <see cref="Pool{T}"/>.
        /// </summary>
        /// <param name="factory">The function used to create new object instances.</param>
        /// <param name="onGet">The action to perform on an object when it is retrieved from the pool.</param>
        /// <param name="onRelease">The action to perform on an object when it is returned to the pool.</param>
        /// <param name="isActive">The function that determines if an object is currently active.</param>
        /// <param name="listCapacity">The initial capacity of the internal list that stores the objects.</param>
        /// <returns>A new, configured instance of the pool.</returns>
        public static Pool<T> Create(Func<T> factory, Action<T> onGet, Action<T> onRelease, Func<T, bool> isActive, int listCapacity = 20)
        {
            Pool<T> pool = new Pool<T>(factory, onGet, onRelease, isActive, listCapacity);
            return pool;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Pool{T}"/>.
        /// </summary>
        internal Pool(Func<T> factory, Action<T> onGet, Action<T> onRelease, Func<T, bool> isActive, int listCapacity)
        {
            _factory = factory;
            _onGet = onGet;
            _onRelease = onRelease;
            _isActive = isActive;

            _items = new List<T>(listCapacity);
        }

        /// <summary>
        /// Creates and adds a specified number of new items to the pool using the factory function.
        /// </summary>
        /// <param name="delta">The number of new items to create and add.</param>
        public void Populate(int delta)
        {
            for (int i = 0; i < delta; i++)
            {
                T item = _factory();
                _items.Add(item);
            }
        }

        /// <summary>
        /// Retrieves an inactive object from the pool. If none are available, a new one is created and added to the pool.
        /// </summary>
        /// <returns>An object ready for use.</returns>
        public T Get()
        {
            T? result = null;
            bool exists = ViewExistingInactive(out result);
            if (!exists)
            {
                result = _factory();
                _items.Add(result);
            }
            return Get(result!);
        }

        /// <summary>
        /// Marks a specific, known item as active, invoking the OnGet action on it.
        /// </summary>
        /// <remarks>
        /// This method is intended to be used on an item that has been retrieved via <see cref="ViewExistingInactive"/>.
        /// </remarks>
        /// <param name="item">The item to formally get.</param>
        /// <returns>The item that was passed in.</returns>
        public T Get(T item)
        {
            Assert.IsTrue(_items.Contains(item));
            activeCount--;
            _onGet?.Invoke(item);
            return item;
        }

        /// <summary>
        /// Finds the first inactive object in the pool without changing its state.
        /// </summary>
        /// <param name="result">The inactive object, if one was found; otherwise, null.</param>
        /// <returns><c>true</c> if an inactive object was found; otherwise, <c>false</c>.</returns>
        public bool ViewExistingInactive([NotNullWhen(true)] out T? result)
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

        /// <summary>
        /// Finds the first active object in the pool without changing its state.
        /// </summary>
        /// <param name="result">The active object, if one was found; otherwise, null.</param>
        /// <returns><c>true</c> if an active object was found; otherwise, <c>false</c>.</returns>
        public bool ViewExistingActive([NotNullWhen(true)] out T? result)
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

        /// <summary>
        /// Informs the pool that an item is no longer in use, invoking the OnRelease action on it.
        /// </summary>
        /// <param name="item">The item to release.</param>
        public void Release(T item)
        {
            activeCount--;
            _onRelease?.Invoke(item);
        }

        /// <summary>
        /// Releases all objects in the pool that are currently considered active.
        /// </summary>
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
}