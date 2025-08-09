using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.Pool;

[RequireComponent(typeof(HexCoroutineRunner))]
public class Example : MonoBehaviour
{
    [SerializeField] private Transform pivot;
    [SerializeField] private Transform point1;
    [SerializeField] private Transform point2;
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

        var data = new Dictionary<string, object>
        {
            { "productName", "Laptop" },
            { "price", 1250.75123f },
            { "discount", 0.15 },
            { "manufactureDate", new DateTime(2025, 6, 15) }
        };

        //string template4 = "product {price:F2} progress={absProgress:F0}";
        //string result4 = template4.FormatVariables("price", 123.234f, "absProgress", 99.4f);
        string template = "Hello, {value:F2}!";
        string result = template.FormatVariables
        (
            new Dictionary<string, object>() 
            {
                { "value", 123.917f }
            }
        );
        Debug.Log(result);

        float a = HexRandom.GetElement(new float[] { 1.2f, 3.4f, 5.6f });

        var v = HexRandom.GetVector3D(0.2f, 0.4f);

        Debug.Log($"substring: {HexRandom.GetSubstringOfLength("123456", 1, 3)}");

        Debug.Log($"Min in enum {nameof(TestEnum)} is {HexEnum.Min<TestEnum>()}");
    }

    private void Update()
    {
        //point.position = point.position.RotateAround3D(pivot.position, new Vector3(0.5f, 0.5f, 0.5f), 1);
        point1.position = point1.position.WithX(HexEasing.EaseInOutQuad(0, 1, Mathf.PingPong(Time.time / 2, 1)) * 10);
        point2.Translate(Vector3.right * (HexEasing.EaseInOutQuadD(-1, 1, Hexath.Remap(Mathf.PingPong(Time.time / 2, 1), 0, 1, -1, 1))) * Time.deltaTime);
        outcolor = color.WithBrightness(brightness);
        point1.GetComponent<MeshRenderer>().material.color = outcolor;
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

    enum TestEnum
    {
        First = 3,
        Second = -2100,
        Third = 100,
        Fourth = 0
    }
}