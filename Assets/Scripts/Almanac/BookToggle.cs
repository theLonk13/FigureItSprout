using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BookToggle : MonoBehaviour
{
    GameObject book;
    Button button;

    // Start is called before the first frame update
    void Awake()
    {
        button = GetComponent<Button>();
        book = GameObject.FindWithTag("Book");
        BookController bookController = book.GetComponent<BookController>();
        if(book != null)
        {
            button.onClick.AddListener(bookController.ToggleBook);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
