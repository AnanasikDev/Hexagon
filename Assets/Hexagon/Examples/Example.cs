using UnityEngine;
using System.Linq;

[RequireComponent(typeof(HexCoroutineRunner))]
public class Example : MonoBehaviour
{
    void Start()
    {
        HexTime.InvokeOnCondition(() => Input.GetKeyDown(KeyCode.N), () => Debug.Log("N Pressed"));
    }
}