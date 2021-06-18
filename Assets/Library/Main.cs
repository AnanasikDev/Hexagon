using UnityEngine;

public class Main : MonoBehaviour
{
    void Start()
    {
        MonoBehaviourExt.extensions.Log(1, 2, "helo");
    }
}