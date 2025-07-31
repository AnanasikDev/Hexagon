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

    Pool<GameObject> pool = Pool<GameObject>.Create(
        factory: () =>
        {
            GameObject result = new GameObject("Gameobject!!!");
            result.SetActive(false);
            return result;
        },
        onGet: (GameObject item) => item.SetActive(true),
        onRelease: (GameObject item) => item.SetActive(false),
        isActive: (GameObject item) => item.activeSelf
    );

    private void Start()
    {
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
            bool found = pool.ViewExistingActive(out GameObject go);
            if (found)
            {
                pool.Get(go);
            }
            //pool.Get();
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            bool found = pool.ViewExistingActive(out GameObject go);
            if (found)
            {
                pool.Release(go);
            }
        }

        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            pool.ReleaseAll();
        }
    }
}