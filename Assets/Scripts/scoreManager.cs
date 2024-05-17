using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scoreManager : MonoBehaviour
{
    public static scoreManager Instance { private set; get; }
    public float score { get; private set; }

    private void Awake()
    {
        Instance = this;
    }

    public void IncreaseScore(float amount)
    {
        score += amount;
    }
}
