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
    public float Invincible;
    public float IntervalBetweenInvincible = 0.2f;
    float IntervalTimer;
    int round;
    public SpriteRenderer Face;
    Color originalColor;
    public Text HPShowText;
    public float PushBack = 700;

    public int Score = 0;
    public Text ScoreShowText;

    public string FieldTag = "Field";

    public GameObject GameOverMenu;

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
        originalColor = Face.color;
    }
    void Update()
    {
        clock += Time.deltaTime;
        if (Invincible > 0)
        {
            Invincible -= Time.deltaTime;
            IntervalTimer += Time.deltaTime;
            if (IntervalTimer >= IntervalBetweenInvincible)
            {
                if (round == 0)
                {
                    round = 1;
                    Face.color = new Color(Face.color.r - 0.2f, Face.color.g - 0.2f, Face.color.b - 0.2f);
                }
                else
                {
                    round = 0;
                    Face.color = originalColor;
                }
                IntervalTimer = 0;
            }
        }
        else
        {
            Face.color = originalColor;
            Invincible = 0;
        }

        if (clock >= Interval)
        {
            clock = 0;
            GiveHand();
            FindObjectOfType<Spawn>().ChangeColor();
        }
    }
    void OnTriggerEnter2D(Collider2D cold)
    {
        if(cold.gameObject.tag != FieldTag)
        {
            if (cold.gameObject.tag == "Rock" && hand == "Paper" || cold.gameObject.tag == "Paper" && hand == "Scissor" || cold.gameObject.tag == "Scissor" && hand == "Rock")
            {
                //Destroy(cold.gameObject);
                cold.GetComponent<Animator>().SetTrigger("Change");
                cold.gameObject.tag = FieldTag;

                Instantiate(CollectedPointParticleEffect, cold.transform.position, Quaternion.identity);
                Score++;
                ScoreShowText.text = "Score: " + Score.ToString();
            }
            else if (cold.gameObject.tag == "Rock" && hand == "Scissor" || cold.gameObject.tag == "Paper" && hand == "Rock" || cold.gameObject.tag == "Scissor" && hand == "Paper" || cold.gameObject.tag == "NoGo")
            {
                if(Invincible <= 0)
                {
                    cold.GetComponent<Animator>().SetTrigger("Change");
                    cold.gameObject.tag = "NoGo";

                    foreach (GameObject ob in Resources.FindObjectsOfTypeAll(typeof(GameObject)))
                    {
                        if (ob.transform.position == cold.transform.position - new Vector3(cold.transform.localScale.x * 2, 0, 0) ||
                            ob.transform.position == cold.transform.position + new Vector3(cold.transform.localScale.x * 2, 0, 0) ||
                            ob.transform.position == cold.transform.position - new Vector3(0, cold.transform.localScale.y * 2, 0) ||
                            ob.transform.position == cold.transform.position + new Vector3(0, cold.transform.localScale.y * 2, 0))
                        {
                            if (ob.tag == "Rock" || ob.tag == "Paper" || ob.tag == "Scissor" || ob.tag == FieldTag)
                                ob.GetComponent<Animator>().SetTrigger("Change");
                            ob.tag = "NoGo";
                        }
                    }

                }
                if(cold.gameObject.tag == "NoGo") DoDamage(1, cold.gameObject);

                Invincible = 2;
            }
            else if(cold.gameObject.tag == hand)
            {
                cold.GetComponent<Animator>().SetTrigger("Change");
                cold.gameObject.tag = FieldTag;
                Instantiate(CollectedPointParticleEffect, cold.transform.position, Quaternion.identity);
            }
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
        if(Invincible <= 0)
        {
            HP -= DamageTaken;
            HPShowText.text = "HP: " + HP.ToString();
        }
        GameObject spawned = Instantiate(HurtParticleEffect, transform.position, Quaternion.identity);
        rb.velocity = Vector2.zero;
        rb.AddForce(Vector3.Normalize(transform.position - DidIt.transform.position) * PushBack);
        FindObjectOfType<AudioManager>().PlayOneShot("slaphitV3", 1);
        Invoke("Reactivate", 0.4f);
        GetComponent<movement>().enabled = false;

        if(HP <= 0)
        {
            Invoke("Die", 2);
            Time.timeScale = 0.3f;
            GameOverMenu.SetActive(true);
        }
    }
    void Die()
    {
        Time.timeScale = 1f;
    }
    void Reactivate()
    {
        GetComponent<movement>().enabled = true;
    }
}