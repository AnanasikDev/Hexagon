using UnityEngine;

public class Main : MonoBehaviour
{
    void Start()
    {
        //MonoBehaviourExt.extensions.Log(1, 2, "helo");
        MonoBehaviourExt.extensions.LogColor("#00ffffff", "hello world");
        MonoBehaviourExt.extensions.Log(MonoBehaviourExt.extensions.Loop<int[], int>(RandomExtensions.GetChance, new int[4] { 2, 4, 8, 16}, 100).ToStringFormat());
    }
}