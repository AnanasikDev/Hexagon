using UnityEngine;

public class MonoBehaviourExt : MonoBehaviour
{
    public static MonoBehaviourExt extensions { get; private set; }
    private void Awake() => extensions = this;
    public void Log(params object[] args)
    {
        Debug.Log(string.Join(" ", args));
    }
}
