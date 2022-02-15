using UnityEngine;
using System.Linq;

public class Main : MonoBehaviour
{
    void Start()
    {
        //Vector3 scale = transform.forward * 2 + Vector3.one;
        Vector3 scale = Vector3.forward + Vector3.one;
        transform.ScaleForward(scale);
        
        //transform.position += Vector3.forward / 2;
        //transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y, transform.localScale.z * 2);
    }
}