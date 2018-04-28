using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatControl : MonoBehaviour {
    public float radius;
    public float dampening;
    public float speedMax;

    private Rigidbody2D rb;
	// Use this for initialization
	void Start () {
        this.rb = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 offset = (mousePos - transform.parent.position).normalized * radius;
        Vector3 newPos = offset + transform.parent.position;
        //Debug.DrawLine(Vector3.zero, newPos);
        //Debug.DrawLine(Vector3.zero, transform.position);
        rb.MovePosition(new Vector2(newPos.x, newPos.y));
        rb.MoveRotation(Quaternion.FromToRotation(Vector3.right, -transform.localPosition).eulerAngles.z);
        
    }
}
