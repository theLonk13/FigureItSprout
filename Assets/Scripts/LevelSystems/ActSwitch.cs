using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActSwitch : MonoBehaviour
{
    [SerializeField] Transform acts;
    int currAct = 1;

    void Start()
    {
        //currAct = 1;
    }

    void Update()
    {
        placeActs();
    }

    void placeActs()
    {
        acts.localPosition = new Vector3(-1000*(currAct-1), 0, 0);
    }

    public void changeAct(int actNum)
    {
        currAct = actNum;
    }
}
