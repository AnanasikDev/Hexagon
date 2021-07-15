using UnityEngine;

public class Main : MonoBehaviour
{
    [SerializeField] GameObject g;
    void Start()
    {
        for (int i = 0; i < 1000; i++)
        {
            GameObject gg = Instantiate(g, TransformExtensions.GetCirclePosition(10), Quaternion.identity);
            gg.SetActive(true);
        }
    }
}