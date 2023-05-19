using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroyLoad : MonoBehaviour
{
    public static GameObject PersistData;

    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(this.gameObject);

        if (PersistData != null && PersistData != this.gameObject)
        {
            Destroy(this.gameObject);
        }
        else
        {
            PersistData = this.gameObject;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
