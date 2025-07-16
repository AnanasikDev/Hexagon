using UnityEngine;
using UnityEngine.Pool;

[RequireComponent(typeof(HexCoroutineRunner))]
public class Example : MonoBehaviour
{
    ObjectPool<GameObject> pool;

    [SerializeField] private Transform pivot;
    [SerializeField] private Transform point;
    [SerializeField] private Color color;
    private Color outcolor;

    private void Start()
    {
        Debug.Log(color);
    }

    private void Update()
    {
        outcolor = color.NormalizeRGB();
        point.GetComponent<MeshRenderer>().material.color = outcolor;
        Debug.Log(outcolor.WrapInHTMLTagRGB("color"));
        Debug.Log(color.GetRGBMagnitude());

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            pool.Get();
        }

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            pool.Get().gameObject.SetActive(true);
        }
    }
}