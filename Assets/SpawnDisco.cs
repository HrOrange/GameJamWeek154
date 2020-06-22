using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnDisco : MonoBehaviour
{
    public GameObject DiscoField;
    public int SizeX = 80;
    public int SizeY = 80;

    void Start()
    {
        for(int k = 0; k <= 1920 / SizeX; k++)
        {
            for (int i = 0; i <= 1080 / SizeY; i++)
            {
                GameObject spawned = Instantiate(DiscoField, Vector3.zero, Quaternion.identity, transform);
                spawned.transform.position = new Vector2(k * SizeX, i * SizeY);
            }
        }
    }
}
