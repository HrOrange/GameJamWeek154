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
        HPSHowText.text = "HP: " + HP.ToString();
        ANN pet = new ANN(new int[] { 3, 2, 1 });
        ANN pet2 = new ANN(new int[] { 3, 2, 1 });
        pet.RandomizeWeights();
        pet2.RandomizeWeights();
        ANN[] children = pet.CreateChildren(pet, pet2, 4, 0.1f);
        print(children[0].weights[0][0][0]);
        print(pet.weights[0][0][0]);
        print(pet2.weights[0][0][0]);


        float[][] output = pet.Calculate(new float[] { 1, 1, 1 });
    }
    public void DoDamage(int DamageTaken)
    {
        HP -= DamageTaken;
        print(HP);
        HPSHowText.text = "HP: " + HP.ToString();
    }
}
