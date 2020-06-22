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
        for (float k = -a / 2 + 0.5f; k < a / 2 + 0.5f; k++)
        {
            for (float i = -a / 2 + 0.5f; i < a / 2 + 0.5f; i++)
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

            Vector2 SpawnPos = new Vector2((Random.Range(-a / 2, a / 2) + 0.5f) * scalex, (Random.Range(-a / 2, a / 2) + 0.5f) * scaley);
            bool there = false;
            foreach(GameObject ob in Resources.FindObjectsOfTypeAll(typeof(GameObject)))
            {
                if ((Vector2)ob.transform.position == SpawnPos && ob.gameObject.tag != field.tag)
                {
                    there = true;
                    break;
                } 
            }
            while (there)
            {
                SpawnPos = new Vector2((Random.Range(-a / 2, a / 2) + 0.5f) * scalex, (Random.Range(-a / 2, a / 2) + 0.5f) * scaley);
                there = false;
                foreach (GameObject ob in Resources.FindObjectsOfTypeAll(typeof(GameObject)))
                {
                    if ((Vector2)ob.transform.position == SpawnPos && ob.gameObject.tag != field.tag)
                    {
                        there = true;
                        break;
                    }
                }
            }

            if (r == 0)
            {
                Instantiate(ScissorField, SpawnPos, Quaternion.identity, transform);
            }
            else if(r == 1)
            {
                Instantiate(RockField, SpawnPos, Quaternion.identity, transform);
            }
            else if(r == 2)
            {
                Instantiate(PaperField, SpawnPos, Quaternion.identity, transform);
            }
        }
    }

}
