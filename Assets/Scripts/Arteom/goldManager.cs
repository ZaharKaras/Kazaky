using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class goldManager : MonoBehaviour
{

    public Text goldCounter;
    public int StartGold;

    public static goldManager Instance;
    private Storage Bank = new Storage();
    // Start is called before the first frame update

    private void Start()
    {
        Bank.TakeCost(-StartGold);
    }

    public bool GetPrice(int price)
    {
        if (price > Bank.GetAmountMinerals()) return false;

        Bank.TakeCost(price);
        return true;

    }
    // Update is called once per frame
    void Update()
    {
        goldCounter.text = Bank.GetAmountMinerals().ToString() + " G ";
    }
}
