using UnityEngine;
using System.Linq;

// Example script using Hexagon
[RequireComponent(typeof(HexCoroutineRunner))]
public class Example : MonoBehaviour
{
    public Canvas canvas;
    void Start()
    {
        Debug.Log(Hexath.SnapNumberToStep(0.123f, 0.1f));
        Debug.Log(Hexath.SnapNumberToStep(0.223f, 0.1f));
        Debug.Log(Hexath.SnapNumberToStep(0.023f, 0.1f));
        Debug.Log(Hexath.SnapNumberToStep(0.023f, 0.01f));

        HexTime.DelayedAction(3, f, 0.1f, 2, (double d) => Debug.Log("Outed with " + d));
    }

    private double f(float a, int b)
    {
        return (double)(a + b);
    }

    private void Update()
    {
        Debug.Log(canvas.GetCanvasSize());
    }
}