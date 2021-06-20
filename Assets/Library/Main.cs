using UnityEngine;

public class Main : MonoBehaviour
{
    void Start()
    {
        for (int i = 0; i < 100; i++) print(RandomExtensions.NormalDistribution(0, 10));
    }
}