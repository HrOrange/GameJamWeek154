using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    private float clock;
    public float Interval = 5f;

    Rigidbody2D rb;

    public int HP = 5;
    public Text HPShowText;
    public float PushBack = 700;

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
        rb = GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        clock += Time.deltaTime;
        if(clock >= Interval)
        {
            clock = 0;
            GiveHand();
            FindObjectOfType<Spawn>().ChangeColor();
        }
    }
    void OnTriggerEnter2D(Collider2D cold)
    {
        if (cold.gameObject.tag == "Rock" && hand == "Paper" || cold.gameObject.tag == "Paper" && hand == "Scissor" || cold.gameObject.tag == "Scissor" && hand == "Rock")
        {
            Destroy(cold.gameObject);
            Instantiate(CollectedPointParticleEffect, transform.position, Quaternion.identity);
            Score++;
            ScoreShowText.text = "Score: " + Score.ToString();
        }


        else if (cold.gameObject.tag == "Rock" && hand == "Scissor" || cold.gameObject.tag == "Paper" && hand == "Rock" || cold.gameObject.tag == "Scissor" && hand == "Paper")
        {
            DoDamage(1, cold.gameObject);
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
    public void DoDamage(int DamageTaken, GameObject DidIt)
    {
        HP -= DamageTaken;
        HPShowText.text = "HP: " + HP.ToString();
        GameObject spawned = Instantiate(HurtParticleEffect, transform.position, Quaternion.identity);
        rb.velocity = Vector2.zero;
        rb.AddForce(Vector3.Normalize(transform.position - DidIt.transform.position) * PushBack);
        print(FindObjectOfType<AudioManager>());
        FindObjectOfType<AudioManager>().PlayOneShot("slaphitV3", 1);
        Invoke("Reactivate", 0.6f);
        GetComponent<movement>().enabled = false;
    }
    void Reactivate()
    {
        GetComponent<movement>().enabled = true;
    }
}