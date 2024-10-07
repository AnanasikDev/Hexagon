using UnityEngine;

public class MyPoolUnit : MonoBehaviour
{
    public static MyPoolUnit Create()
    {
        return new GameObject().AddComponent<MyPoolUnit>();
    }
}