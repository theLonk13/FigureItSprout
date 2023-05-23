using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class NewPlantOverlayScript : MonoBehaviour
{
    [SerializeField] PlantCard plant;
    [SerializeField] GameObject nextScreen;
    [SerializeField] bool OpenBook = true;

    void OnMouseUp()
    {
        BookController book = GameObject.Find("BookPages").GetComponent<BookController>();
        if(book.getBookToggle() < 0)
        {
            try
            {
                if (nextScreen != null)
                {
                    nextScreen.SetActive(true);
                }
                else if(OpenBook)
                {
                    book.OpenToPlant(plant.getPlantID());
                }
            }
            catch(NullReferenceException e)
            {

            }
            Destroy(this.gameObject);
        }
    }
}
