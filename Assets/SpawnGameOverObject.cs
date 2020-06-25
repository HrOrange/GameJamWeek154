using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnGameOverObject : MonoBehaviour
{
    int nextCount = 0;

    public void Next()
    {
        nextCount += 2;
        transform.GetChild(nextCount).gameObject.SetActive(true);
        transform.GetChild(nextCount + 1).gameObject.SetActive(true);
    }
}
