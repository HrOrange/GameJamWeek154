using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnGameOverObject : MonoBehaviour
{
    int nextCount = 1;

    private void Start()
    {
        print(transform.childCount - 1);
    }
    public void Next()
    {
        nextCount += 2;
        print(nextCount + 1);
        if(transform.childCount - 1 >= nextCount + 1)
        {
            transform.GetChild(nextCount).gameObject.SetActive(true);
            transform.GetChild(nextCount + 1).gameObject.SetActive(true);
        }
    }
}
