using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeplacementJoueurScript : MonoBehaviour {

    public float normalSpeed = 5;
    public float sneakSpeed = 3;
    public float slowSpeed = 2;

    private float speed;

    public Vector2 velocity;
    Rigidbody2D rigiBoy;

    public bool isSneaky;
    public bool makeNoise;
    public bool isSlow;

	// Use this for initialization
	void Start () {
        rigiBoy = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {
        if(Input.GetButtonDown("X"))
        {
            isSneaky = !isSneaky;
            //Debug.Log("AlloTest");
        }

        if(isSlow)
        {
            speed = slowSpeed;
        }
        else if(isSneaky)
        {
            speed = sneakSpeed;
        }
        else
        {
            speed = normalSpeed;
        }
        if (Mathf.Abs(Input.GetAxisRaw("Horizontal")) > 0.2f || (Mathf.Abs(Input.GetAxisRaw("Vertical")) > 0.05f))
        {
            velocity = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")) * speed;
        }
        else
        {
            velocity = Vector2.zero;
        }

        if(velocity != Vector2.zero && (!isSneaky || isSlow))
        {
            makeNoise = true;
        }
        else
        {
            makeNoise = false;
        }
	}

    private void FixedUpdate()
    {
        rigiBoy.MovePosition(rigiBoy.position + velocity*Time.fixedDeltaTime);
    }
}
