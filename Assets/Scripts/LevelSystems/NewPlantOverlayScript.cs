using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewPlantOverlayScript : MonoBehaviour
{
    void OnMouseUp()
    {
        Destroy(this.gameObject);
    }
}
