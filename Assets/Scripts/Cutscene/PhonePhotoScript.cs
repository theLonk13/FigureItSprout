using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PhonePhotoScript : MonoBehaviour
{
    [SerializeField] Animator messageAnim;
    [SerializeField] Image messageBG;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public Animator GetMsgAnimator()
    {
        return messageAnim;
    }

    public void SetSprite(Sprite photo)
    {
        messageBG.sprite = photo;
    }
}
