using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public static class HexAssets
{
    public static T[] EDITOR_GetScriptableObjectInstances<T>() where T : Object
    {
#if UNITY_EDITOR
        string[] guids = AssetDatabase.FindAssets($"t:{typeof(T).Name}");
        T[] a = new T[guids.Length];
        for (int i = 0; i < guids.Length; i++)
        {
            string path = AssetDatabase.GUIDToAssetPath(guids[i]);
            a[i] = (T)AssetDatabase.LoadAssetAtPath(path, typeof(T));
        }

        return a;
#else
        return null;
#endif
    }

    public static T[] EDITOR_GetAllPrefabInstances<T>(System.Func<string, bool> filter = null) where T : MonoBehaviour
    {
#if UNITY_EDITOR
        string[] paths = EDITOR_GetAllPrefabs();
        List<T> results = new();
        for (int i = 0; i < paths.Length; i++)
        {
            GameObject go = (GameObject)AssetDatabase.LoadAssetAtPath(paths[i], typeof(GameObject));

            if (go.TryGetComponent<T>(out T comp))
            {
                results.Add(comp);
            }
        }

        return results.ToArray();
#else
        return null;
#endif
    }

    public static string[] EDITOR_GetAllPrefabs(System.Func<string, bool> filter = null)
    {
#if UNITY_EDITOR
        string[] temp = AssetDatabase.GetAllAssetPaths();
        List<string> result = new List<string>();
        if (filter == null) filter = (string token) => true;
        foreach (string s in temp)
        {
            if (s.Contains(".prefab") && filter(s))
                result.Add(s);
        }
        return result.ToArray();
#else
        return null;
#endif
    }
}