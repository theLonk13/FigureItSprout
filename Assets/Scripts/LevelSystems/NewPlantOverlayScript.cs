using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewPlantOverlayScript : MonoBehaviour
{
    [SerializeField] PlantCard plant;

    void OnMouseUp()
    {
        BookController book = GameObject.Find("BookPages").GetComponent<BookController>();
        if(book.getBookToggle() < 0)
        {
            book.OpenToPlant(plant.getPlantID());
            Destroy(this.gameObject);
        }
    }
}
