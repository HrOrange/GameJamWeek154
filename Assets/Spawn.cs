using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Spawn : MonoBehaviour
{
    public float Timer;
    public float OveralTimer;
    public float Interval;
    public GameObject ScissorField;
    public GameObject PaperField;
    public GameObject RockField;
    public float scaley;
    public float scalex;
    public int a;
    public GameObject field;
    public float DistanceToPlayerMin = 3;

    GameObject player;
    public GameObject GameOverMenu;

    Color paperColor;
    Color scissorColor;
    Color rockColor;
    List<Color> DifferentRockColors = new List<Color> { new Color(255, 120, 0), new Color(255, 50, 0), new Color(160, 100, 70)};
    List<Color> DifferentPaperColors = new List<Color> { new Color(255, 255, 220), new Color(220, 255, 255), new Color(255, 220, 255) };
    List<Color> DifferentScissorColors = new List<Color> { new Color(255, 0, 255), new Color(50, 0, 255), new Color(220, 140, 255) };

    [HideInInspector] public List<GameObject> fields = new List<GameObject>();
    void Start()
    {
        player = FindObjectOfType<Health>().gameObject;

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
        Invoke("ChangeColor", 2.2f);
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
        OveralTimer += Time.deltaTime;

        if (Timer >= Interval)
        {
            Timer = 0;
            int r = Random.Range(0, 3);

            List<GameObject> SortedFields = new List<GameObject>();
            foreach (GameObject ob in fields)
            {
                if (ob.tag == field.tag && Vector3.Distance(ob.transform.position, player.transform.position) >= DistanceToPlayerMin) SortedFields.Add(ob);
            }
            if(SortedFields.Count > 0)
            {
                int number = Random.Range(0, SortedFields.Count - 1);

                if (r == 0) SortedFields[number].tag = "Scissor";
                else if (r == 1) SortedFields[number].tag = "Rock";
                else SortedFields[number].tag = "Paper";
                SortedFields[number].GetComponent<Animator>().SetTrigger("Change");
            }
            else
            {
                Invoke("Die", 2);
                Time.timeScale = 0.3f;
                GameOverMenu.SetActive(true);
            }
        }
    }
    void Die()
    {
        Time.timeScale = 1f;
    }

}
