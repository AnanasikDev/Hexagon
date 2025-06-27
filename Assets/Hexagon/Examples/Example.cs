using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(HexCoroutineRunner))]
public class Example : MonoBehaviour
{
    Pool<MyPoolUnit> pool = new Pool<MyPoolUnit>(x => x.gameObject.activeSelf, () => MyPoolUnit.Create());
    private Image image;

    private void Start()
    {
        float[] segs = new float[] { 0.1f, 0.4f, 0.5f };
        for (int i = 0; i < 10000; i++)
        {
            Debug.Log(HexRandom.GetWeightedIndex(segs, 1.0f));
        }

        //List<int> a = new List<int> { 1, 2, 3 };
        //List<int> b = new List<int>(a);
        //int[] c = new int[] { 10, 11, 12 };
        //int[] d = (int[])c.Clone();
        //a[0] = 5;
        //c[0] = 200;
        //Debug.Log(b[0]);
        //Debug.Log(d[0]);
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