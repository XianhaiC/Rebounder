using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour {

    public float speed;
    public float speedSlow;
    public float boundXMin;
    public float boundXMax;
    public float boundYMin;
    public float boundYMax;

    private Rigidbody2D rb;

	// Use this for initialization
	void Start () {
        this.rb = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {
        float currentSpeed;
        if (Input.GetButton("Slow")) currentSpeed = speedSlow;
        else currentSpeed = speed;

        Vector2 velocityNew = new Vector2(0.0f, 0.0f);

	    if (IsLeftPressed()) {
            velocityNew += new Vector2(-currentSpeed, 0.0f);
        }
        if (IsRightPressed()) {
            velocityNew += new Vector2(currentSpeed, 0.0f);
        }
        if (IsDownPressed()) {
            velocityNew += new Vector2(0.0f, -currentSpeed);
        }
        if (IsUpPressed()) {
            velocityNew += new Vector2(0.0f, currentSpeed);
        }
        

        rb.position += velocityNew * Time.deltaTime;
     
      
      
        rb.position = new Vector2(Mathf.Clamp(rb.position.x, boundXMin, boundXMax), Mathf.Clamp(rb.position.y, boundYMin, boundYMax));
      

    }

    private bool IsLeftPressed() {
        if (Input.GetAxis("Horizontal") < 0) return true;
        else return false;
    }

    private bool IsRightPressed() {
        if (Input.GetAxis("Horizontal") > 0) return true;
        else return false;
    }

    private bool IsDownPressed() {
        if (Input.GetAxis("Vertical") < 0) return true;
        else return false;
    }

    private bool IsUpPressed()
    {
        if (Input.GetAxis("Vertical") > 0) return true;
        else return false;
    }
}
