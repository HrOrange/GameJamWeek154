using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    private float clock;
    public float Interval = 5f;

    public int HP = 35;
    public Text HPShowText;

    public int Score = 0;
    public Text ScoreShowText;

    public string hand;
    public Sprite ScissorImage;
    public Sprite PaperImage;
    public Sprite RockImage;
    public GameObject Shown;

    public GameObject HurtParticleEffect;
    public GameObject CollectedPointParticleEffect;

    void Start()
    {
        HPShowText.text = "HP: " + HP.ToString();
        GiveHand();
    }
    void Update()
    {
        clock += Time.deltaTime;
        if(clock >= Interval)
        {
            clock = 0;
            GiveHand();
        }
    }
    void OnTriggerEnter2D(Collider2D cold)
    {
        if (cold.gameObject.tag == "Rock" && hand == "Paper" || cold.gameObject.tag == "Paper" && hand == "Scissor" || cold.gameObject.tag == "Scissor" && hand == "Rock")
        {
            Destroy(cold.gameObject);
            Instantiate(CollectedPointParticleEffect, transform.position, Quaternion.identity, transform);
            Score++;
            ScoreShowText.text = "Score: " + Score.ToString();
        }


        else if (cold.gameObject.tag == "Rock" && hand == "Scissor" || cold.gameObject.tag == "Paper" && hand == "Rock" || cold.gameObject.tag == "Scissor" && hand == "Paper")
        {
            DoDamage(1);
        }
    }

    void GiveHand()
    {
        int r = Random.Range(0, 3);
        if (r == 0)
        {
            hand = "Rock";
            Shown.GetComponent<Image>().sprite = RockImage;
            Shown.GetComponent<RectTransform>().localScale = new Vector3(0.75f, 0.75f, 0);
        }
        else if (r == 1)
        {
            hand = "Paper";
            Shown.GetComponent<Image>().sprite = PaperImage;
            Shown.GetComponent<RectTransform>().localScale = new Vector3(0.75f, 0.75f, 0);
        }
        else if (r == 2)
        {
            hand = "Scissor";
            Shown.GetComponent<Image>().sprite = ScissorImage;
            Shown.GetComponent<RectTransform>().localScale = new Vector3(0.8f, 0.6f, 0);
        }
    }
    public void DoDamage(int DamageTaken)
    {
        HP -= DamageTaken;
        HPShowText.text = "HP: " + HP.ToString();
        Instantiate(HurtParticleEffect, transform.position, Quaternion.identity, transform);
    }
}