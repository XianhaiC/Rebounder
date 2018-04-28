using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackSpin : MonoBehaviour {
    public GameObject pf_BulletStraight;

    private GameObject player;
    private float angle;
    private float rotationSpeed;
    private float speed;
    private Vector2 force;
    private float acceleration;
    private int fireRate;
    private int damage;
    private float durationEnd;
    private bool reboundable;
    private bool aimPlayer;
    private bool homePlayer;
    private bool local;

    private float nextFire;
    private bool initialized = false;
    

    public void Initialize(
        GameObject player,
        float angle,
        float rotationSpeed,
        float speed,
        Vector2 force,
        float acceleration,
        int fireRate,
        int damage,
        float duration,
        bool reboundable,
        bool aimPlayer,
        bool homePlayer,
        bool local) {

        this.player = player;
        this.angle = angle;
        this.rotationSpeed = rotationSpeed;
        this.speed = speed;
        this.force = force;
        this.acceleration = acceleration;
        this.fireRate = fireRate;
        this.damage = damage;
        this.durationEnd = Time.time + duration;
        this.reboundable = reboundable;
        this.aimPlayer = aimPlayer;
        this.homePlayer = homePlayer;
        this.local = local;
      
        this.initialized = true;
    }

    void Update() {
        if (!initialized) return;

        if (Time.time > durationEnd) Destroy(gameObject);
    }

    private void FixedUpdate() {
        if (Time.time > nextFire) {
            nextFire = Time.time + 1.0f / fireRate;
            angle = (angle + (rotationSpeed / fireRate)) % 360.0f;
            GameObject bullet = Instantiate(pf_BulletStraight, transform.position, transform.rotation) as GameObject;
            if (local) {
                var emptyObject = new GameObject();
                emptyObject.transform.parent = transform;
                bullet.transform.parent = emptyObject.transform;
            }
            bullet.GetComponent<Bullet>().Initialize(this.damage, this.reboundable);
            Vector2 direction = new Vector2(Mathf.Cos(angle * Mathf.Deg2Rad), Mathf.Sin(angle * Mathf.Deg2Rad));
            bullet.GetComponent<BulletStraightMove>().Initialize(player, direction, speed, force, acceleration, aimPlayer, homePlayer);
        }
    }
}
