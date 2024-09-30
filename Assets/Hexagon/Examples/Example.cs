using UnityEngine;

[RequireComponent(typeof(HexCoroutineRunner))]
public class Example : MonoBehaviour
{
    Pool<MyPoolUnit> pool = new Pool<MyPoolUnit>();
    void Start()
    {

        var go1 = new GameObject();
        var u1 = go1.AddComponent<MyPoolUnit>();
        pool.RecordNew(u1);

        var go2 = new GameObject();
        var u2 = go2.AddComponent<MyPoolUnit>();
        pool.RecordNew(u2);

        var go3 = new GameObject();
        var u3 = go3.AddComponent<MyPoolUnit>();
        pool.RecordNew(u3);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            pool.TakeInactive().EnableInPool();
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            pool.TakeActive().DisableInPool();
        }
    }
}