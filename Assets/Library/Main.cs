using UnityEngine;

public class Main : MonoBehaviour
{
    void Start()
    {
        MonoBehaviourExt.extensions.Log(1, 2, "helo");
        MonoBehaviourExt.extensions.LogColor("#00ffffff", "{0}hello {1}world");
    }
}