using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BookToggle : MonoBehaviour
{
    GameObject book;
    BookController bookController = null;
    int bookOpen = -1;

    Button button;
    GameObject lvUICanvas;
    LevelManager lvMan = null;

    //Sprites
    [SerializeField] Image bookToggleImage;
    [SerializeField] Sprite bookOpenSprite;
    [SerializeField] Sprite bookClosedSprite;

    // Start is called before the first frame update
    void Awake()
    {
        button = GetComponent<Button>();
        book = GameObject.FindWithTag("Book");
        bookController = book.GetComponent<BookController>();
        if(bookController != null)
        {
            button.onClick.AddListener(bookController.ToggleBook);
        }

        /*
        GameObject lvUICanvas = GameObject.Find("LevelUICanvas");
        if (lvUICanvas != null)
        {
            lvMan = lvUICanvas.GetComponent<LevelManager>();
        }
        if(lvMan != null)
        {
            button.onClick.AddListener(lvMan.ToggleUIBook); 
        }
        //*/
    }

    // Update is called once per frame
    void Update()
    {
        bookOpen = bookController.getBookToggle();

        if(bookOpen < 0)
        {
            bookToggleImage.sprite = bookClosedSprite;
        }
        else
        {
            bookToggleImage.sprite = bookOpenSprite;
        }
    }
}
