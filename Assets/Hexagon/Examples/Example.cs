using UnityEngine;

[RequireComponent(typeof(HexCoroutineRunner))]
public class Example : MonoBehaviour
{
    void Start()
    {
        HexTime.InvokeOnCondition(() => Input.GetKeyDown(KeyCode.N), () => Debug.Log("N Pressed"));

        Vector2 a = new Vector2(96.59f, 25.88f);
        a = a.WithRotation(-15);
        Debug.Log((Vector3)(new Vector3Int()));
    }
}