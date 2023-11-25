using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Dialogue
{

    public string name; //name of NPC speaking

    [TextArea(3, 10)]
    public string[] sentences; //sentences in dialogue
    public int[] senderIDs;

}
