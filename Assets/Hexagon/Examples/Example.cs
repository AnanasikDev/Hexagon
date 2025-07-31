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

    //ObjectPool<GameObject> pool = new ObjectPool<GameObject>(
    //    createFunc: () => new GameObject("new GO!"),
    //    actionOnGet: (GameObject go) => go.SetActive(true),
    //    actionOnRelease: (GameObject go) => go.SetActive(false)
    //); 

    Pool<GameObject> pool = Pool<GameObject>.Create(
        factory: () => new GameObject("Gameobject!!!"),
        onGet: (GameObject item) => item.SetActive(true),
        isActive: (GameObject item) => item.activeSelf,
        onRelease: (GameObject item) => item.SetActive(false)
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
            pool.Get();
            //pool.TakeInactiveOrCreate();
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            using (var lease = pool.LeaseActive())
            {
                if (lease.HasValue && lease.GetReadOnlyItem().name != string.Empty)
                {
                    var obj = lease.ConfirmAndRelease();
                }
            }
            //if (pool.TryPeekActive(out GameObject go))
            //{
            //    pool.Release(go);
            //}
        }

        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            pool.ReleaseAll();
        }
    }
}