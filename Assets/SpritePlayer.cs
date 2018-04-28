using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpritePlayer : MonoBehaviour {
    private Vector3 defaultScale;
    private LineRenderer lr;
    private bool spawning;
    private Vector3[] positions;
	// Use this for initialization
	void Start () {
        this.defaultScale = new Vector3(2.0f, 2.0f, 2.0f);
        this.spawning = true;
        this.lr = GetComponent<LineRenderer>();
        positions = new Vector3[lr.positionCount];
        lr.GetPositions(positions);
        transform.localScale = new Vector3(40, 40, 40);
    }
	
	// Update is called once per frame
	void FixedUpdate () {
        if (spawning) {
            if (Vector3.Distance(transform.localScale, defaultScale) > Vector3.kEpsilon) {
                transform.localScale = Vector3.Lerp(transform.localScale, defaultScale, 0.2f);
            }
            else spawning = false;
        }
        for (int i = 0; i < positions.Length; i++) {
            positions[i] = Rotate(positions[i], 2, 2.0f);
            positions[i] = Rotate(positions[i], 1, 2.0f);
        }
        lr.SetPositions(positions);
        /*
        Vector3[] positions2D = new Vector3[lr.positionCount];

        System.Array.Copy(positions, positions2D, lr.positionCount);

        for (int i = 0; i < positions2D.Length; i++) positions2D[i] = (Vector2)positions2D[i];

        lr.SetPositions(positions2D);*/
    }

    public Vector3 Rotate(Vector3 vector, int axis, float aDegree) {
        if (axis == 0) return Quaternion.Euler(aDegree, 0, 0) * vector;
        else if (axis == 1) return Quaternion.Euler(0, aDegree, 0) * vector;
        else return Quaternion.Euler(0, 0, aDegree) * vector;
    }
}
