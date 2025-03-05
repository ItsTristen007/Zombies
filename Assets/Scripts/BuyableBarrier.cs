using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuyableBarrier : MonoBehaviour
{
    [SerializeField] int barrierCost = 50;
    [SerializeField] GameObject[] spawners;

    public int GetBarrierCost()
    {
        return barrierCost;
    }

    void OnDestroy()
    {
        if (spawners != null)
        {
            foreach (GameObject s in spawners)
            {
                s.SetActive(true);
            }
        }
    }
}
