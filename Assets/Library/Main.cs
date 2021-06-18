using UnityEngine;

public class Main : MonoBehaviour
{
    void Start()
    {
        MonoBehaviourExt.extensions.Log(
            MonoBehaviourExt.extensions.Loop<int[], int>(
                RandomExtensions.GetChance, new int[4] { 2, 4, 8, 16}, 100).ToStringFormat(" "));

        MonoBehaviourExt.extensions.Log(0.1f.Whole());
        MonoBehaviourExt.extensions.Log(1.0f.Whole());
        MonoBehaviourExt.extensions.Log(1.1f.Whole());
    }
}