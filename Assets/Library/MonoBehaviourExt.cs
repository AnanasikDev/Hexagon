using UnityEngine;
using System;
public class MonoBehaviourExt : MonoBehaviour
{
    public static MonoBehaviourExt extensions { get; private set; }
    private void Awake() => extensions = this;
    public void Log(params object[] args)
    {
        Debug.Log(string.Join(" ", args));
    }
    public void LogColor(string color, params object[] args)
    {
        Debug.Log($"<color={color}>{string.Join(" ", args)}</color>");
    }
    /// <summary>
    /// exmaple: LogColor("#FF00FFFF", "hello {0} world {1}")
    /// </summary>
    /// <param name="color"></param>
    /// <param name="format"></param>
    public void LogColor(string color, string format)
    {
        Debug.Log(string.Format(format, $"<color={color}>", "</color>"));
    }
}
