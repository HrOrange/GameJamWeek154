using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    public int HP = 35;
    public Text HPSHowText;

    void Start()
    {
        //HPSHowText.text = "HP: " + HP.ToString();
    }
    public void DoDamage(int DamageTaken)
    {
        HP -= DamageTaken;
        print(HP);
        HPSHowText.text = "HP: " + HP.ToString();
    }
}
