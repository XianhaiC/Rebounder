using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {
    public int damage = 1;
    public bool reboundable;

    private bool playerControl;

    public void Initialize(int damage, bool reboundable) {
        this.damage = damage;
        this.reboundable = reboundable;
        if (reboundable) gameObject.layer = LayerMask.NameToLayer("BulletRebound");
        else gameObject.layer = LayerMask.NameToLayer("Bullet");
    }

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.tag.Equals("Barrier")) {
            this.SetPlayerControl(true);
            GetComponent<MeshRenderer>().material = Resources.Load("Materials/Player", typeof(Material)) as Material;
        }
    }

    public bool GetPlayerControl() {
        return this.playerControl;
    }

    public void SetPlayerControl(bool playerControl) {
        this.playerControl = playerControl;
    }

    public int GetDamage() {
        return this.damage;
    }

    public void SetDamage(int damage) {
        this.damage = damage;
    }

    public bool GetReboundable() {
        return this.reboundable;
    }

    public void SetReboundable(bool reboundable) {
        this.reboundable = reboundable;
    }
}
