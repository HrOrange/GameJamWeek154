using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointSystem : MonoBehaviour
{
    private float clock;
    public string hand;
    void Start()
    {
        int r = Random.Range(0, 3);
        if (r == 0)
        {
            hand = "Rock";
        }
        else if (r == 1)
        {
            hand = "Paper";
        }
        else if (r == 2)
        {
            hand = "Scissor";
        }
    }

    // Update is called once per frame
    void Update()
    {
        clock += Time.deltaTime;
    }

}
