using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuyableBarrier : MonoBehaviour
{
    [SerializeField] int barrierCost = 50;

    public int GetBarrierCost()
    {
        return barrierCost;
    }
}
