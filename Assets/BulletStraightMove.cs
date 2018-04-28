using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletStraightMove : MonoBehaviour {

    private Rigidbody2D rb;
    private GameObject player;
    private Vector2 velocity;
    private float speed;
    private Vector2 force;
    private float acceleration;
    private bool aimPlayer;
    private bool homePlayer;

    public void Initialize(GameObject player, Vector2 velocity, float speed, Vector2 force, float acceleration, bool aimPlayer, bool homePlayer) {
        this.rb = GetComponent<Rigidbody2D>();
        this.player = player;
        this.velocity = velocity;
        this.speed = speed;
        this.force = force;
        this.acceleration = acceleration;
        this.aimPlayer = aimPlayer;
        this.homePlayer = homePlayer;

        Vector2 velocityTarget = velocity;
        if (aimPlayer && (player != null)) {
            velocityTarget = player.transform.position - transform.position;
        }

        rb.velocity = velocityTarget.normalized * speed;
    }

    // Use this for initialization
    void Start () {
        
    }
	
	// Update is called once per frame
	void FixedUpdate () {
        Vector2 forceTarget = force;
        if (homePlayer && (player != null)) {
            forceTarget = player.transform.position - transform.position; 
        }
        rb.AddForce(forceTarget.normalized * acceleration);
    }
}
