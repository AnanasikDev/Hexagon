using UnityEngine;

public class Main : MonoBehaviour
{
    void Start()
    {
        //for (int i = 0; i < 100; i++) print(RandomExtensions.NormalDistribution(0, 10));
        print(Scale01(2, -5, 5));
        
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R)) transform.Reset();
    }
    float Scale01(float n, float min, float max)
    {
        float m = n / (max - min);
        return m;
    }
}