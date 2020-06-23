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

    Color paperColor;
    Color scissorColor;
    Color rockColor;
    List<Color> DifferentRockColors = new List<Color> { new Color(255, 120, 0), new Color(255, 50, 0), new Color(160, 100, 70)};
    List<Color> DifferentPaperColors = new List<Color> { new Color(255, 255, 255), new Color(220, 255, 255), new Color(255, 220, 255) };
    List<Color> DifferentScissorColors = new List<Color> { new Color(255, 0, 255), new Color(50, 0, 255), new Color(220, 140, 255) };

    List<GameObject> fields = new List<GameObject>();
    void Start()
    {
        for (float k = -a / 2 + 0.5f; k < a / 2 + 0.5f; k++)
        {
            for (float i = -a / 2 + 0.5f; i < a / 2 + 0.5f; i++)
            {
                fields.Add(Instantiate(field, new Vector2(k * scalex, i * scaley), Quaternion.identity, transform));
            }
        }
        rockColor = DifferentRockColors[Random.Range(0, DifferentRockColors.Count - 1)];
        paperColor = DifferentPaperColors[Random.Range(0, DifferentPaperColors.Count - 1)];
        scissorColor = DifferentScissorColors[Random.Range(0, DifferentScissorColors.Count - 1)];
        Invoke("ChangeColor", 1.8f);
    }
    public void ChangeColor()
    {
        if(FindObjectOfType<Health>().hand == "Paper")
        {
            foreach (GameObject f in fields) f.transform.GetChild(0).GetComponent<SpriteRenderer>().color = paperColor;
        }
        else if (FindObjectOfType<Health>().hand == "Scissor")
        {
            foreach (GameObject f in fields) f.transform.GetChild(0).GetComponent<SpriteRenderer>().color = scissorColor;
        }
        else
        {
            foreach (GameObject f in fields) f.transform.GetChild(0).GetComponent<SpriteRenderer>().color = rockColor;
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
