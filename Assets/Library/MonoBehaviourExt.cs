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
}
