using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movement : MonoBehaviour {
    public float speed = 10.0f;
    public Rigidbody2D rb;
    private Vector2 move;
    public GameObject PauseMenu;
    public GameObject SettingsMenu;

    // Start is called before the first frame update
    void Start() {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("p"))
        {
            if (PauseMenu.activeSelf && !SettingsMenu.activeSelf)
            {
                Time.timeScale = 1;
                PauseMenu.SetActive(false);
            }
            else if (!PauseMenu.activeSelf && SettingsMenu.activeSelf)
            {
                PauseMenu.SetActive(true);
                SettingsMenu.SetActive(false);
            }
            else
            {
                PauseMenu.SetActive(true);
                Time.timeScale = 0;
            }
        }

        move = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
    }
    void FixedUpdate()
    {
        moveCharacter(move);
    }

    void moveCharacter(Vector2 direction)
    {
        rb.AddForce(direction * speed);
    }
}
