using UnityEngine;

[RequireComponent(typeof(HexCoroutineRunner))]
public class Example : MonoBehaviour
{
    Pool<MyPoolUnit> pool = new Pool<MyPoolUnit>(x => x.gameObject.activeSelf, () => MyPoolUnit.Create());

    private void Start()
    {
        /*var go1 = new GameObject();
        var u1 = go1.AddComponent<MyPoolUnit>();
        pool.RecordNew(u1);

        var go2 = new GameObject();
        var u2 = go2.AddComponent<MyPoolUnit>();
        pool.RecordNew(u2);

        var go3 = new GameObject();
        var u3 = go3.AddComponent<MyPoolUnit>();
        pool.RecordNew(u3);*/

        Vector3 v1 = new Vector3(0, 0, 1);
        Vector3 axis = new Vector3(1, 1, 0);
        Vector3 v1r = v1.Rotate(axis, 45);
        Debug.Log($"{v1} | {v1r}");
        Debug.DrawRay(Vector3.zero, axis, Color.blue, 10);
        Debug.DrawRay(Vector3.zero, v1, Color.white, 10);
        Debug.DrawRay(Vector3.zero, v1r, Color.red, 10);

        Debug.Log(new Vector2(1, 2).SignedAngleBetween2D(new Vector2(-1, 2)));
        Debug.Log(new Vector2(1, 2).SignedAngleBetween2D(new Vector2(-2, 1)));
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            pool.TakeInactiveOrCreate().gameObject.SetActive(true);
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            if (pool.TryTakeActive(out var go))
                go.gameObject.SetActive(false);
        }

        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            if (pool.TryTakeActive(out var go)){
                pool.Unrecord(go);
            }
        }
    }
}