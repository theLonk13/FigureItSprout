using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PhoneMessageScript : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI messageText;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public TextMeshProUGUI GetMessageBox()
    {
        return messageText;
    }
}
