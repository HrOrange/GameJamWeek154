using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    private float clock;
    public string hand;
    public int HP = 35;
    public Text HPSHowText;
    public Image Scissorcard;
    public Image Rockcard;
    public Image Papercard;
    private GameObject Shown;
    void Start()
    {
        HPSHowText.text = "HP: " + HP.ToString();
        int r = Random.Range(0, 3);
        if (r == 0)
        {
            hand = "Rock";
            Shown = Rockcard;
        }
        else if (r == 1)
        {
            hand = "Paper";
            Shown = Papercard;
        }
        else if (r == 2)
        {
            hand = "Scissor";
            Shown = Scissorcard;
        }
    }
    void Update()
    {
        clock += Time.deltaTime;
    }
    void OnTriggerEnter2D(Collider2D cold)
    {
        if (cold.gameObject.tag == "Rock" && hand == "Paper")
        {
            Destroy(cold.gameObject);
        }
        else if (cold.gameObject.tag == "Paper" && hand == "Scissor")
        {
            Destroy(cold.gameObject);
        }
        else if (cold.gameObject.tag == "Scissor" && hand == "Rock")
        {
            Destroy(cold.gameObject);
        }

    }
    public void DoDamage(int DamageTaken)
    {
        HP -= DamageTaken;
        print(HP);
        HPSHowText.text = "HP: " + HP.ToString();
    }
}