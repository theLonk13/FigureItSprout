using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlowRotate : MonoBehaviour
{
    [SerializeField] GameObject glow;

    // Update is called once per frame
    void Update()
    {
        if (glow != null)
        {
            glow.transform.Rotate(new Vector3(0f, 0f, 100f * Time.deltaTime));
        }
    }
}
