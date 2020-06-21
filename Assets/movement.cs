using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movement : MonoBehaviour {
    public float speed = 10.0f;
    public Rigidbody2D rb;
    private Vector2 move;
    // Start is called before the first frame update
    void Start() {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
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
