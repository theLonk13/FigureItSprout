using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BookController : MonoBehaviour
{
    //page objects
    [SerializeField] GameObject pagesObject;
    RectTransform pgTransform;
    GameObject[] pages;

    //book transform
    RectTransform bookTransform;
    //locations for the book being toggled or not
    Vector3 showPos = new Vector3(0, 0, 0);
    Vector3 hidePos = new Vector3(0, -1000, 0);
    [SerializeField] int speed;

    int curr_pg = 0;
    int book_toggle = -1;

    int[] plant_unlocks;

    //Singleton book
    //public static GameObject bookInstance;

    // Start is called before the first frame update
    void Awake()
    {
        //Put book in initial hiding position
        pgTransform = pagesObject.GetComponent<RectTransform>();
        pages = GameObject.FindGameObjectsWithTag("BookPage");
        bookTransform = GetComponent<RectTransform>();
        bookTransform.localPosition = hidePos;

        /*
        if (bookInstance != null && bookInstance != this.gameObject)
        {
            Destroy(this.gameObject);
        }
        else
        {
            bookInstance = this.gameObject;
        }
        //*/

        //Disable all pages
        //*
        foreach (GameObject page in pages)
        {
            page.SetActive(false);
        }
        //*/
    }

    // Update is called once per frame
    void Update()
    {
        if(pgTransform != null)
        {
            pgTransform.localPosition = new Vector3(
                -50 * curr_pg,
                0,
                0
            ) ;
        }

        //move book to position based on hide or toggle
        if(book_toggle > 0)
        {
            bookTransform.localPosition = Vector3.MoveTowards(bookTransform.localPosition, showPos, Time.deltaTime * speed);
        }
        else
        {
            bookTransform.localPosition = Vector3.MoveTowards(bookTransform.localPosition, hidePos, Time.deltaTime * speed);
        }

        ActivatePages();
    }

    public void PgLeft()
    {
        if(curr_pg > 0)
        {
            curr_pg--;
        }
    }

    public void PgRight()
    {
        if(curr_pg < Mathf.Ceil(pages.Length / 2))
        {
            curr_pg++;
        }
    }

    void ActivatePage(int plantID)
    {
        if(plantID <= 0)
        {
            return;
        }
        else
        {
            foreach(GameObject page in pages)
            {
                //page.SetActive(true);
                BookPageData pgData = page.GetComponent<BookPageData>();
                if(pgData != null && pgData.getPlantID() == plantID)
                {
                    page.SetActive(true);
                }
            }
        }
    }

    void ActivatePages()
    {
        //show pages that the player has unlocked
        GameObject PlantUnlocks = GameObject.FindWithTag("PlantUnlocks");
        UnlockedPlants unlocks = PlantUnlocks.GetComponent<UnlockedPlants>();
        int[] plant_unlocks = unlocks.get_plant_unlocks();
        //Activate pages in book
        for (int i = 0; i < plant_unlocks.Length; i++)
        {
            ActivatePage(plant_unlocks[i]);
        }
    }

    public void ToggleBook()
    {
        Debug.Log("Toggling book");
        book_toggle = -1*book_toggle;
    }
}
