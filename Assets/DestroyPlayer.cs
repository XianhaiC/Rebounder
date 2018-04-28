using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyPlayer : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.tag.Equals("Bullet") && collision.gameObject.GetComponent<Bullet>().GetPlayerControl() == false) {
            Destroy(gameObject);
        }   
    }
}
