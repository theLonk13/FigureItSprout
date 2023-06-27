using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class NewPlantOverlayScript : MonoBehaviour
{
    [SerializeField] PlantCard plant;
    [SerializeField] GameObject nextScreen;
    [SerializeField] bool OpenBook = true;

    [SerializeField] GameObject glow;

    void Update()
    {
        if(glow != null)
        {
            glow.transform.Rotate(new Vector3(0f, 0f, 100f * Time.deltaTime));
        }
    }

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
