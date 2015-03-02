using UnityEngine;
using System.Collections;
using UnityEngine;

public class Starter : MonoBehaviour 
{

    private void Awake()
    {
        if (GameObject.FindObjectOfType<Core.App>() == null)
            GameObject.Instantiate(Resources.Load<GameObject>("Prefabs/Core/App"));

        Destroy(gameObject);
    }


}
