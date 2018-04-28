using UnityEngine;
using System.Collections;

public class AttackStraight : MonoBehaviour {
    public GameObject pf_BulletStraight;
    
    private GameObject player;
    private Vector2 velocity;
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
        Vector2 velocity,
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
        this.velocity = velocity;
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
        //FU_Spawn.Update();
    }
    
    private void FixedUpdate() {
        if (Time.time > nextFire) {
            nextFire = Time.time + 1.0f / fireRate;
            GameObject bullet = Instantiate(pf_BulletStraight, transform.position, transform.rotation) as GameObject;
            if (local) {
                var emptyObject = new GameObject();
                emptyObject.transform.parent = transform;
                bullet.transform.parent = emptyObject.transform;
            }
            bullet.GetComponent<Bullet>().Initialize(this.damage, this.reboundable);
            bullet.GetComponent<BulletStraightMove>().Initialize(player, velocity, speed, force, acceleration, aimPlayer, homePlayer);
        }
    }
}
