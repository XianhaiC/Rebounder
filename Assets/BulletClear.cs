using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletClear : MonoBehaviour {

    private LineRenderer lr;
    private int sides = 6;
    private bool clearing;
	// Use this for initialization
	void Start () {
        this.lr = GetComponent<LineRenderer>();
        Vector3[] newPositions = new Vector3[sides];
        for (int i = 0; i < sides; i++) {
            float radians = Mathf.PI * 2.0f * i / sides;
            newPositions[i] = new Vector3(Mathf.Cos(radians), Mathf.Sin(radians));
        }
        this.clearing = false;
        lr.positionCount = sides;
        lr.SetPositions(newPositions);
        lr.loop = true;
        transform.localScale = Vector3.zero;
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public IEnumerator Clear() {
        clearing = true;
        transform.localScale = Vector3.zero;
        Vector3 finalScale = new Vector3(10.0f, 10.0f, 10.0f);
        while (Vector3.Distance(transform.localScale, finalScale) > 1000 * Vector3.kEpsilon) {
            transform.localScale = Vector3.Lerp(transform.localScale, finalScale, 0.05f);
            yield return new WaitForEndOfFrame();
        }
        transform.localScale = Vector3.zero;
        clearing = false;
    }

    private void OnTriggerStay2D(Collider2D collision) {
        if (clearing && collision.gameObject.tag.Equals("Bullet")) {
            Destroy(collision.gameObject);

        }
    }
}
