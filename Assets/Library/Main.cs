using UnityEngine;
using System.Linq;

public class Main : MonoBehaviour
{
    void Start()
    {
        Debug.Log(transform.DeepChildrenCount());
        transform.DeepChildren().ForEach(obj => Debug.Log(obj.name));
    }
}