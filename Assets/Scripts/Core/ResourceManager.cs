using System;
using System.Collections.Generic;
using UnityEngine;

namespace Core
{
    public class ResourceManager : Core.MonoBehaviourGod, IResourceManager
    {
        // Instantiate GameObject from prefab path and return reference to GameObject instance
        public GameObject Instantiate(string prefabPath)
        {
            GameObject prefab = LoadPrefab<GameObject>(prefabPath);

            return (this.Instantiate(prefab) as GameObject);
        }

        // Instantiate GameObject from prefab path and return reference to script on instance
        public T Instantiate<T>(string prefabPath) where T :  MonoBehaviour
        {
            GameObject prefab = LoadPrefab<GameObject>(prefabPath);

            Core.Dbg.Assert(prefab != null, "ResourceManager.Instantiate() prefab in path, prefab not found " + prefabPath);

            GameObject instance = this.Instantiate(prefab) as GameObject;

            return instance.GetComponent<T>();
        }

        // Instantiate GameObject from prefab
        public UnityEngine.Object Instantiate(GameObject prefab)
        {
            Core.Dbg.Log("ResourceManager.Instantiate() from prefab " + prefab.name, Dbg.MessageType.Info);
            return GameObject.Instantiate(prefab);
        }

        // Load prefab
        public T LoadPrefab<T>(string prefabPath) where T : UnityEngine.Object
        {
            Core.Dbg.Log("ResourceManager.LoadPrefab() loading " + prefabPath, Dbg.MessageType.Info);
            return Resources.Load<T>(prefabPath);
        }

        // Create Instance
        public static ResourceManager CreateInstance()
        {
            var prefab = Resources.Load<GameObject>("Prefabs/Core/ResourceManager");
        
            return (UnityEngine.GameObject.Instantiate(prefab) as GameObject).GetComponent<ResourceManager>();
        }

    }
}
