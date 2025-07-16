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
    [Range(0, 2)][SerializeField] float brightness;

    private void Start()
    {
        float[] segs = new float[] { 0.1f, 0.4f, 0.5f };
        for (int i = 0; i < 10000; i++)
        {
            Debug.Log(HexRandom.GetWeightedIndex(segs, 1.0f));
        }
        
        pool = new ObjectPool<GameObject>(
            () => new GameObject("HEEE"),
            (GameObject go) => go.SetActive(true),
            (GameObject go) => go.SetActive(false)
            );

        Debug.Log(color);

        segs.Shuffle();
    }

    private void Update()
    {
        point.position = point.position.RotateAround3D(pivot.position, new Vector3(0.5f, 0.5f, 0.5f), 1);
        outcolor = color.WithBrightness(brightness);
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