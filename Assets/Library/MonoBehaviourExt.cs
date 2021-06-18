using UnityEngine;
using System;
using System.Collections.Generic;
public class MonoBehaviourExt : MonoBehaviour
{
    public static MonoBehaviourExt extensions { get; private set; }
    //public delegate TResult[] Loop<T>(Func<T, TResult> func, object arg, int n);
    public delegate TResult Funcf<in T, out TResult>(T arg);
    private void Awake() => extensions = this;
    public void Log<T>(params T[] args)
    {
        Debug.Log(string.Join(" ", args));
    }
    public void LogColor(string color, params object[] args)
    {
        Debug.Log($"<color={color}>{string.Join(" ", args)}</color>");
    }
    public IEnumerable<TResult> Loop<T, TResult>(Funcf<T, TResult> func, T arg, int n)
    {
        for (int i = 0; i < n; i++) yield return func(arg);
    }
    public void Loop<T>(Action<T> func, T arg, int n)
    {
        for (int i = 0; i < n; i++) func(arg);
    }
}
