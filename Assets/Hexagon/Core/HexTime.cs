using System.Collections;
using UnityEngine;

public static class HexTime
{
    /// <summary>
    /// Automatically executes the given action with delay
    /// </summary>
    public static void DelayedAction(float delaySeconds, System.Action action)
    {
        IEnumerator execute()
        {
            yield return new WaitForSeconds(delaySeconds);
            action?.Invoke();
        }
        HexCoroutineRunner.instance.StartCoroutine(execute());
    }

    /// <summary>
    /// Automatically executes the given action with delay and 1 argument
    /// </summary>
    public static void DelayedAction<In1>(float delaySeconds, System.Action<In1> action, In1 arg1)
    {
        IEnumerator execute()
        {
            yield return new WaitForSeconds(delaySeconds);
            action?.Invoke(arg1);
        }
        HexCoroutineRunner.instance.StartCoroutine(execute());
    }

    /// <summary>
    /// Automatically executes the given action with delay and 2 arguments
    /// </summary>
    public static void DelayedAction<In1, In2>(float delaySeconds, System.Action<In1, In2> action, In1 arg1, In2 arg2)
    {
        IEnumerator execute()
        {
            yield return new WaitForSeconds(delaySeconds);
            action?.Invoke(arg1, arg2);
        }
        HexCoroutineRunner.instance.StartCoroutine(execute());
    }

    /// <summary>
    /// Automatically executes the given action with delay and 3 arguments
    /// </summary>
    public static void DelayedAction<In1, In2, In3>(float delaySeconds, System.Action<In1, In2, In3> action, In1 arg1, In2 arg2, In3 arg3)
    {
        IEnumerator execute()
        {
            yield return new WaitForSeconds(delaySeconds);
            action?.Invoke(arg1, arg2, arg3);
        }
        HexCoroutineRunner.instance.StartCoroutine(execute());
    }

    /// <summary>
    /// Automatically executes the given action with delay and 4 arguments
    /// </summary>
    public static void DelayedAction<In1, In2, In3, In4>(float delaySeconds, System.Action<In1, In2, In3, In4> action, In1 arg1, In2 arg2, In3 arg3, In4 arg4)
    {
        IEnumerator execute()
        {
            yield return new WaitForSeconds(delaySeconds);
            action?.Invoke(arg1, arg2, arg3, arg4);
        }
        HexCoroutineRunner.instance.StartCoroutine(execute());
    }

    /// <summary>
    /// Automatically executes the given function with delay and returns the result
    /// </summary>
    public static void DelayedAction<Out>(float delaySeconds, System.Func<Out> func, System.Action<Out> onComplete)
    {
        IEnumerator execute()
        {
            yield return new WaitForSeconds(delaySeconds);
            Out result = func.Invoke();
            onComplete?.Invoke(result);
        }
        HexCoroutineRunner.instance.StartCoroutine(execute());
    }

    /// <summary>
    /// Automatically executes the given function with delay and returns the result (1 argument)
    /// </summary>
    public static void DelayedAction<In1, Out>(float delaySeconds, System.Func<In1, Out> func, In1 arg1, System.Action<Out> onComplete)
    {
        IEnumerator execute()
        {
            yield return new WaitForSeconds(delaySeconds);
            Out result = func.Invoke(arg1);
            onComplete?.Invoke(result);
        }
        HexCoroutineRunner.instance.StartCoroutine(execute());
    }

    /// <summary>
    /// Automatically executes the given function with delay and returns the result (2 arguments)
    /// </summary>
    public static void DelayedAction<In1, In2, Out>(float delaySeconds, System.Func<In1, In2, Out> func, In1 arg1, In2 arg2, System.Action<Out> onComplete)
    {
        IEnumerator execute()
        {
            yield return new WaitForSeconds(delaySeconds);
            Out result = func.Invoke(arg1, arg2);
            onComplete?.Invoke(result);
        }
        HexCoroutineRunner.instance.StartCoroutine(execute());
    }

    /// <summary>
    /// Automatically executes the given function with delay and returns the result (3 arguments)
    /// </summary>
    public static void DelayedAction<In1, In2, In3, Out>(float delaySeconds, System.Func<In1, In2, In3, Out> func, In1 arg1, In2 arg2, In3 arg3, System.Action<Out> onComplete)
    {
        IEnumerator execute()
        {
            yield return new WaitForSeconds(delaySeconds);
            Out result = func.Invoke(arg1, arg2, arg3);
            onComplete?.Invoke(result);
        }
        HexCoroutineRunner.instance.StartCoroutine(execute());
    }


    // Repeat action every interval
    public static IEnumerator RepeatAction(float interval, System.Action action)
    {
        while (true)
        {
            yield return new WaitForSeconds(interval);
            action?.Invoke();
        }
    }

    // Wait for a condition to become true
    public static IEnumerator WaitForCondition(System.Func<bool> condition, System.Action onSuccess)
    {
        yield return new WaitUntil(condition);
        onSuccess?.Invoke();
    }
}