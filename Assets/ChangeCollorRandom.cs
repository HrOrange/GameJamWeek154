using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeCollorRandom : MonoBehaviour
{
    float clock;
    public float Interval = 0.5f;
    Image img;
    List<Color> colors = new List<Color> { new Color(100, 0, 0), new Color(0, 100, 0), new Color(0, 0, 100),
        new Color(0, 100, 100), new Color(100, 0, 100), new Color(50, 0, 100)};
    int lengthOfList;

    private void Start()
    {
        img = GetComponent<Image>();
        lengthOfList = colors.Count;
        img.color = colors[Random.Range(0, lengthOfList)];
    }
    void Update()
    {
        clock += Time.deltaTime;
        if(clock >= Interval)
        {
            clock = 0;
            img.color = colors[Random.Range(0, lengthOfList)];
        }
    }
}
