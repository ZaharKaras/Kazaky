using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Storage : MonoBehaviour
{
    static public int minerals = 0;

    public void TakeMinerals(int amount)
    {
        minerals += amount;
    }

}
