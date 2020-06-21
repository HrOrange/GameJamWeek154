using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn : MonoBehaviour
{
    public float Timer;
    public float Interval;
    public GameObject ScissorField;
    public GameObject PaperField;
    public GameObject RockField;
    public float scaley;
    public float scalex;
    public int a;
    public GameObject field;
    // Start is called before the first frame update
    void Start()
    {
        for (int k = 0; k < a; k++)
        {
            for (int i = 0; i < a; i++)
            {
                Instantiate(field, new Vector2(k * scalex, i * scaley), Quaternion.identity, transform);
            }
        }
    }
    void Update()
    {
        Timer += Time.deltaTime;
        if (Timer >= Interval)
        {
            Timer = 0;
            int r = Random.Range(0, 3);
            print(r);
            if (r == 0)
            {
                Instantiate(ScissorField, new Vector2(Random.Range(0, a) * scalex, Random.Range(0, a) * scaley), Quaternion.identity, transform);
            }
            else if(r == 1)
            {
                Instantiate(RockField, new Vector2(Random.Range(0, a) * scalex, Random.Range(0, a) * scaley), Quaternion.identity, transform);
            }
            else if(r == 2)
            {
                Instantiate(PaperField, new Vector2(Random.Range(0, a) * scalex, Random.Range(0, a) * scaley), Quaternion.identity, transform);
            }
        }
    }

}
