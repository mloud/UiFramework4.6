using System;
using System.Collections.Generic;
using UnityEngine;

namespace Core
{
    public interface IResourceManager
    {
        // Instantiate GameObject from prefab path and return reference to GameObject instance
        GameObject Instantiate(string prefabPath);

        // Instantiate GameObject from prefab path and return reference to script on instance
        T Instantiate<T>(string prefabPath) where T : MonoBehaviour;

        // Instantiate GameObject from prefab
        UnityEngine.Object Instantiate(GameObject prefab);

        // Load prefab
        T LoadPrefab<T>(string prefabPath) where T : UnityEngine.Object;
    }
}
