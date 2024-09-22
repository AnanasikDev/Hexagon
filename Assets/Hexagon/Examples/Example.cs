using UnityEngine;
using System.Linq;

// Example script using Hexagon
public class Example : MonoBehaviour
{
    public Canvas canvas;
    void Start()
    {
        Vector3 v1 = new Vector3(1, 2, 3);
        v1 = v1.WithX(99);
        Debug.Log(v1);

        Vector3 v2 = new Vector3(1, 2, 3);
        v2.SetX(123);
        v2.SetY(321);
        v2.SetZ(213);
        Debug.Log(v2);
    }

    private void Update()
    {
        Debug.Log(canvas.GetCanvasSize());
    }
}