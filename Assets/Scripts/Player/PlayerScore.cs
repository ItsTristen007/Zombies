using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScore : MonoBehaviour
{
    float playerScore;

    public float GetScore()
    {
        return playerScore;
    }

    void Awake()
    {
        playerScore = 0f;
    }

    public void ChangeScore(float value)
    {
        playerScore += value;
    }

}
