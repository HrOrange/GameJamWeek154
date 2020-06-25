using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    private float clock;
    public float Interval = 5f;

    Rigidbody2D rb;
    public string GetCoinSoundEffectName = "CoinDrop2";

    public int HP = 5;
    public float Invincible;
    public float IntervalBetweenInvincible = 0.2f;
    float IntervalTimer;
    int round;
    public SpriteRenderer Face;
    Color originalColor;
    public TextMeshProUGUI HPShowText;
    public float PushBack = 700;

    public int Score = 0;
    public TextMeshProUGUI ScoreShowText;

    public string FieldTag = "Field";

    public GameObject GameOverMenu;

    public string hand;
    public Sprite ScissorImage;
    public Sprite PaperImage;
    public Sprite RockImage;
    public GameObject Shown;

    public GameObject HurtParticleEffect;
    public GameObject CollectedPointParticleEffect;

    public string ClockName = "ClockV2";
    bool once = false;
    public TextMeshProUGUI CountDown;

    public GameObject GainMoreHPButton;
    public GameObject GainMoreSpaceButton;
    bool ClickOnField = false;

    public void GainMoreHPClicked()
    {
        Score -= 20;
        HP++;
        HPShowText.text = "HP: " + HP.ToString();
        ScoreShowText.text = "Coins: " + Score.ToString();
        CheckForHPChange();
        CheckForFieldChange();
    }
    public void CheckForHPChange()
    {
        if (Score >= 20 && HP < 3)
        {
            GainMoreHPButton.SetActive(true);
        }
        else GainMoreHPButton.SetActive(false);
    }
    public void GainMoreSpaceClicked()
    {
        ClickOnField = !ClickOnField;
    }
    public void CheckForFieldChange()
    {
        if (Score >= 10)
        {
            bool DidntGetThrough = false;
            foreach (GameObject ob in FindObjectOfType<Spawn>().fields)
            {
                if (ob.tag == "NoGo")
                {
                    DidntGetThrough = true;
                    GainMoreSpaceButton.SetActive(true);
                    break;
                }
            }
            if (!DidntGetThrough)
            {
                GainMoreSpaceButton.SetActive(false);
                ClickOnField = false;
            } 
        }
        else
        {
            GainMoreSpaceButton.SetActive(false);
            ClickOnField = false;
        }
    }

    void Start()
    {
        HPShowText.text = "HP: " + HP.ToString();
        GiveHand();
        rb = GetComponent<Rigidbody2D>();
        originalColor = Face.color;
    }
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (ClickOnField)
            {
                List<GameObject> noGoFields = new List<GameObject>();
                foreach (GameObject ob in FindObjectOfType<Spawn>().fields)
                {
                    if (ob.tag == "NoGo")
                    {
                        noGoFields.Add(ob);
                    }
                }
                if(noGoFields.Count > 0)
                {
                    Vector2 mousePos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
                    mousePos = Camera.main.ScreenToWorldPoint(mousePos);

                    GameObject Closest = noGoFields[0];
                    float ClosestDistance = Vector2.Distance((Vector2)Closest.transform.position, mousePos);
                    foreach (GameObject go in noGoFields)
                    {
                        if(Vector2.Distance((Vector2)go.transform.position, mousePos) < ClosestDistance)
                        {
                            ClosestDistance = Vector2.Distance((Vector2)go.transform.position, mousePos);
                            Closest = go;
                        }
                    }
                    print(ClosestDistance);
                    if(ClosestDistance <= 1.35f)
                    {
                        Closest.tag = "Field";
                        Closest.GetComponent<Animator>().SetTrigger("Change");
                        Score -= 10;
                        ScoreShowText.text = "Coins: " + Score.ToString();
                        CheckForFieldChange();
                        CheckForHPChange();
                    }
                }
            }
        }

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

        if ((clock + 3) >= Interval)
        {
            if (!once)
            {
                once = true;
                FindObjectOfType<AudioManager>().PlayOneShot("ClockV2", 0.4f);
            }
            CountDown.gameObject.SetActive(true);
            CountDown.text = (Interval - clock).ToString("F2");
        }

        if (clock >= Interval)
        {
            once = false;
            CountDown.gameObject.SetActive(false);
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

                FindObjectOfType<AudioManager>().PlayOneShot(GetCoinSoundEffectName, 1f);

                Instantiate(CollectedPointParticleEffect, cold.transform.position, Quaternion.identity);
                Score++;
                CheckForHPChange();
                CheckForFieldChange();

                ScoreShowText.text = "Coins: " + Score.ToString();
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
                    CheckForFieldChange();
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
            if (hand != "Rock")
            {
                hand = "Rock";
                Shown.GetComponent<Image>().sprite = RockImage;
                Shown.GetComponent<RectTransform>().localScale = new Vector3(0.75f, 0.75f, 0);
            }
            else GiveHand();
        }
        else if (r == 1)
        {
            if (hand != "Paper")
            {
                hand = "Paper";
                Shown.GetComponent<Image>().sprite = PaperImage;
                Shown.GetComponent<RectTransform>().localScale = new Vector3(0.75f, 0.75f, 0);
            }
            else GiveHand();
        }
        else if (r == 2)
        {
            if (hand != "Scissor")
            {
                hand = "Scissor";
                Shown.GetComponent<Image>().sprite = ScissorImage;
                Shown.GetComponent<RectTransform>().localScale = new Vector3(0.8f, 0.6f, 0);
            }
            else GiveHand();
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

        CheckForHPChange();
        if (HP <= 0)
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