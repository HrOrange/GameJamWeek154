using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Invoke("ChangeS", 2);
    }
    void ChangeS()
    {
        SceneManager.LoadScene("Game", LoadSceneMode.Single);
    }
}
