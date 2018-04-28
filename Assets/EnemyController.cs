using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour {
    public int healthInitial = 20;
    public float invulnDuration = 0.5f;

    private SpriteEnemy spriteEnemy;
    private BulletClear bulletClear;
    private bool invulnerable;
    private bool alive;

    private int healthCurrent;
	// Use this for initialization
	void Start () {
        Debug.Log("controller call");
        this.spriteEnemy = GetComponentInChildren<SpriteEnemy>();
        this.bulletClear = GetComponentInChildren<BulletClear>();
        this.invulnerable = false;
        this.healthInitial = 7;
        spriteEnemy.Initialize();
	}
	
	// Update is called once per frame
	void Update () {
		if (healthCurrent < 1) {
            spriteEnemy.gameObject.SetActive(false);
            alive = false;
        }
        //if (Input.GetKeyDown("f") && !invulnerable) StartCoroutine(DecreaseHealth(1));
	}

    private void OnTriggerEnter2D(Collider2D collision) { 
        if (!invulnerable && collision.gameObject.tag.Equals("Bullet")) {
            Bullet bullet = collision.gameObject.GetComponent<Bullet>();
            if (bullet.GetPlayerControl()) {
                StartCoroutine(DecreaseHealth(bullet.GetDamage()));
                Destroy(collision.gameObject);
            }
            
        }
    }


    public IEnumerator DecreaseHealth(int damage) {
        if (healthCurrent <= damage) {
            healthCurrent = 0;
            invulnerable = true;
            yield return StartCoroutine(bulletClear.Clear());
        }
        else {
            healthCurrent -= damage;
            spriteEnemy.SetSides(healthCurrent + 2);
            invulnerable = true;
            yield return StartCoroutine(spriteEnemy.InvulnerableFrames(invulnDuration));
            invulnerable = false;
        }
        yield return null;
    }

    public IEnumerator Reset(int health) {
        healthCurrent = health;
        alive = true;
        spriteEnemy.gameObject.SetActive(true);
        invulnerable = true;
        yield return StartCoroutine(spriteEnemy.Reset(health + 2));
        invulnerable = false;
    }

    public bool IsAlive() {
        return alive;
    }
}
