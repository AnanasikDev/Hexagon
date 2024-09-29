using UnityEngine;

[RequireComponent(typeof(HexCoroutineRunner))]
public class Example : MonoBehaviour
{
    void Start()
    {
        Debug.Log(1);
        HexDebug.Log(2,3,4);
    }
}