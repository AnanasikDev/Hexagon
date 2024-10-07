using UnityEngine;

[RequireComponent(typeof(HexCoroutineRunner))]
public class Example : MonoBehaviour
{
    Pool<MyPoolUnit> pool = new Pool<MyPoolUnit>();
    void Start()
    {
        pool.isActiveFunc = x => x.gameObject.activeSelf;
        pool.createFunc = () => MyPoolUnit.Create();

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
            pool.TakeInactiveOrCreate().gameObject.SetActive(true);
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            if (pool.TryTakeActive(out var go))
                go.gameObject.SetActive(false);
        }
    }
}