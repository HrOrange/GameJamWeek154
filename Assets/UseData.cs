using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UseData : MonoBehaviour
{
    public Health PointData;
    public Spawn SpawnData;
    int data;
    int myData;
    TextMeshProUGUI MyText;
    public bool ThisIsTime;
    bool DoneMyJob = false;

    public float BetweenAddUp = 0.1f;
    float timer;

    float MyTextSize = 1f;
    public float MyTextSizeChangeSpeed = 0.1f;

    private void Start()
    {
        MyText = GetComponent<TextMeshProUGUI>();
        if (PointData) data = PointData.Score;
        else if (SpawnData) data = (int)(SpawnData.OveralTimer / 60);
    }

    void Update()
    {
        timer += Time.deltaTime;
        if(myData < data)
        {
            if (MyTextSize <= 2f || MyTextSize >= 0.25f) MyTextSizeChangeSpeed = -MyTextSizeChangeSpeed;
            MyTextSize += MyTextSizeChangeSpeed;
            MyText.rectTransform.localScale = new Vector3(MyTextSize, MyTextSize, 1);
        }

        if(timer >= BetweenAddUp)
        {
            timer = 0;
            if (myData < data)
            {
                myData++;
                if (ThisIsTime)
                {
                    int Minutes = myData % 60;
                    int Hours = myData / 60;
                    if (Minutes < 10) MyText.text = Hours.ToString() + ":0" + Minutes.ToString();
                    else MyText.text = Hours.ToString() + ":" + Minutes.ToString();
                }
                else
                {
                    MyText.text = myData.ToString();
                }
            }
            else if (!DoneMyJob)
            {
                MyText.rectTransform.localScale = new Vector3(1, 1, 1);
                transform.parent.GetComponent<SpawnGameOverObject>().Next();
                DoneMyJob = true;
            }
        }
    }
}
