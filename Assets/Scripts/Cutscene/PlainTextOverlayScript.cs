using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlainTextOverlayScript : MonoBehaviour
{

    bool plainTextOpen = false;
    [SerializeField] Animator plainTextAnim;

    void Start()
    {
        if(plainTextAnim != null)
        {
            plainTextAnim.SetBool("ShowPlainText", plainTextOpen);
        }
    }

    public void TogglePlainText()
    {
        plainTextOpen = !plainTextOpen;
        plainTextAnim.SetBool("ShowPlainText", plainTextOpen);
    }

}
