using System.Collections;
using UnityEngine;

class PerfTest : MonoBehaviour
{
    private void Start()
    {
        StartCoroutine(Run());
    }

    private IEnumerator Run()
    {
        yield return new WaitForSeconds(3);

        Vector2 vec = new Vector2(-0.113f, 3.82f);
        for (int i = 0; i < 1_000_000; i++)
        {
            Vector3 vec2 = new Vector3(vec.x, vec.y, 0); //HexVectorUtils.ConvertTo3D(vec);
        }
    }
}