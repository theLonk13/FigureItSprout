using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDataPersistance
{
    void LoadData(SaveData saveData);
    void SaveData(ref SaveData saveData);
}
