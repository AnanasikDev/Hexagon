using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.Pool;

[RequireComponent(typeof(HexCoroutineRunner))]
public class Example : MonoBehaviour
{
    [SerializeField] private Transform pivot;
    [SerializeField] private Transform point;
    [SerializeField] private Color color;
    private Color outcolor;
    [Range(0, 2)][SerializeField] float brightness;

    Pool<GameObject> pool = new Pool<GameObject>(
        factoryFunc: () => new GameObject("Gameobject!!!"),
        isAvailable: (GameObject item) => item.activeSelf,
        onGet:       (GameObject item) => item.SetActive(true),
        onRelease:   (GameObject item) => item.SetActive(false)
    );

    private void Start()
    {
        //string str = "Hello my fur friend!";
        //for (int i = 0; i < 1000; i++)
        //{
        //    Debug.Log(HexRandom.GetRandomChar(str));
        //}
        pool.Populate(5);
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
            pool.TakeInactiveOrCreate();
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            if (pool.TryPeekActive(out GameObject go))
            {
                pool.Release(go);
            }
        }
    }
}