using UnityEngine;

[RequireComponent(typeof(HexCoroutineRunner))]
public class Example : MonoBehaviour
{
    Pool<MyPoolUnit> pool = new Pool<MyPoolUnit>(x => x.gameObject.activeSelf, () => MyPoolUnit.Create());

    private void Start()
    {
        Vector2 v1 = Vector3.one.ConvertTo2D();
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