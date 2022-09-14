using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Storage : MonoBehaviour
{

    public static int minerals = 0;

    public void TakeMinerals(int amount)
    {
        minerals += amount;
    }

    public int GetAmountMinerals()
    {
        return minerals;
    }

    public void TakeCost(int price)
    {
        minerals -= price;
    } 

}
