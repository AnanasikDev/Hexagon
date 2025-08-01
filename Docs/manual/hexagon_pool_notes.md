## Pool

@page hexagon_pool_notes Guide

### Pool\<T> where T : class

A generic, one-list object manager. This class maintains a single collection of objects and uses a delegate to determine whether each object is currently "active" or "inactive".

It is designed for scenarios where objects have a distinct active/inactive state that **is managed externally**, and the primary goal is to reuse inactive objects rather than creating new ones.

### Core Concepts

The pool's behavior is defined by four primary delegates provided during construction:

1. **factory**: Creates a new object instance when no inactive objects are available. It is also responsible for managing the initial state.

2. **isActive**: Determines if the given object is active or inactive. This is the central mechanism for state tracking.

3. **onGet**: Transitions the gotten object from inactive to active. Make sure that `isActive` will treat the newly gotten object as active.

4. **onRelease**: Transitions the gotten object from active to inactive. Make sure that `isActive` will treat the released object as inactive.

---

Note: `isActive` **MUST** be synchronized with `onGet` and `onRelease`. The state is **NOT** tracked internally.

### Initialization

Create a pool instance using the static `Create` method, providing the necessary delegates.

```cs
public static Pool<T> Create(
    Func<T> factory,
    Action<T> onGet, 
    Action<T> onRelease,
    Func<T, bool> isActive,
    int listCapacity = 20
)
```

Example:
```cs
Pool<GameObject> pool = Pool<GameObject>.Create(
    factory: () =>
    {
        GameObject result = new GameObject("My gameobject");
        result.SetActive(false);
        return result;
    },
    onGet:      (GameObject item) => item.SetActive(true),
    onRelease:  (GameObject item) => item.SetActive(false),
    isActive:   (GameObject item) => item.activeSelf
);
```

### Recommended API
The primary and recommended way to retrieve an object is with the `Get()` method.

`Get()` Searches for the first available inactive object. If one is found, it is returned. If not, a new object is created using the `factory`, added to the pool, and then returned. `onGet` action is called automatically and the returned object is ready to use.

`Release(T item)` Releases the specified object and calls OnRelease action on it.

`ReleaseAll()` Releases all inactive objects.

`Populate(int delta)` Creates `delta` **new** objects and adds them to the pool. Their state is set by `factory` delegate.

### Extra API

Not recommended for general use. Must be used with caution.

`ViewExistingInactive(out T result)` Finds the first inactive object without changing its state. To finalize getting, `Get(item)` must be called. To discard getting no actions required.

`ViewExistingActive(out T result)` Finds the first active object without changing its state. To finalize releasing, `Release(item)` must be called. To discard releasing no actions required.

`Get(T item)` marks the specified object as gotten.

### Important Notes

#### External State Management

This pool does not internally manage the state of objects. It relies entirely on the `isActive` delegate to report the state. The user is responsible for ensuring that the `onGet` and `onRelease` actions correctly modify the object's state so that `isActive` returns the expected value.

#### Linear Search Performance

All `Get` and `View` operations perform a linear search through the collection. Performance will degrade as the total number of objects in the pool increases. This implementation is best suited for small collections.