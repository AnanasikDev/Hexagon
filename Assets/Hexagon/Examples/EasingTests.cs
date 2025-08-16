using System;
using System.Collections.Generic;
using UnityEngine;
using Hexagon;

public class EasingTests : MonoBehaviour
{
    [SerializeField] private float distance = 1.5f;
    [SerializeField] private float duration = 1.5f;
    [SerializeField] private float magnitude = 2.0f;

    private List<GameObject> gameObjects_1 = new List<GameObject>();
    private List<HexEasing.Function> movers_1 = new List<HexEasing.Function>();

    private List<GameObject> gameObjects_2 = new List<GameObject>();
    private List<HexEasing.Function> movers_2 = new List<HexEasing.Function>();

    private void Start()
    {
        int i = 0;
        foreach (HexEasing.Ease v in Enum.GetValues(typeof(HexEasing.Ease)))
        {
            string name_1 = $"{Enum.GetName(typeof(HexEasing.Ease), v)} - {i}";
            HexEasing.Function function_1 = HexEasing.GetEasingFunction(v);
            GameObject gameObject_1 = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            gameObject_1.name = name_1;
            gameObject_1.transform.position = new Vector3(i * distance, 0, 0);
            gameObjects_1.Add(gameObject_1);
            movers_1.Add(function_1);

            string name_2 = $"{name_1} Delta";
            HexEasing.Function function_2 = HexEasing.GetEasingFunctionDerivative(v);
            GameObject gameObject_2 = GameObject.CreatePrimitive(PrimitiveType.Cube);
            gameObject_2.name = name_2;
            gameObject_2.transform.position = new Vector3(i * distance, 0, -4 * magnitude * 2);
            gameObjects_2.Add(gameObject_2);
            movers_2.Add(function_2);

            i++;
        }
    }

    private void Update()
    {
        for (int i = 0; i < gameObjects_1.Count; i++)
        {
            float time = Mathf.PingPong(Time.time, duration); // Loop time between 0 and 1

            GameObject go_1 = gameObjects_1[i];
            HexEasing.Function mover_1 = movers_1[i];
            float value_1 = mover_1(0.0f, 1.0f, time);
            go_1.transform.position = new Vector3(go_1.transform.position.x, go_1.transform.position.y, value_1 * magnitude); // Move along the x-axis

            float sign = Mathf.PingPong(Time.time, 2.0f * duration) < duration ? -1.0f : 1.0f; // Alternate between -1 and 1

            GameObject go_2 = gameObjects_2[i];
            HexEasing.Function mover_2 = movers_2[i];
            float value_2 = mover_2(0.0f, 1.0f, sign == -1 ? 1 - time : time);
            Debug.Log(value_2);
            go_2.transform.Translate(new Vector3(0, 0, value_2 * magnitude * Time.deltaTime * sign)); // Move along the x-axis
        }
    }
}