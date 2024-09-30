using UnityEngine;

public class MyPoolUnit : MonoBehaviour, IPoolable
{
    public bool isActiveInPool { get; set; }

    public void EnableInPool()
    {
        isActiveInPool = true;
        gameObject.SetActive(true);
    }
    public void DisableInPool()
    {
        isActiveInPool = false;
        gameObject.SetActive(false);
    }
}