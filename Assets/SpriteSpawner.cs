using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteSpawner : MonoBehaviour {
    public int sides;

    private LineRenderer lr;
    private Vector3[] positions;
    // Use this for initialization
	void Start () {
        this.lr = GetComponent<LineRenderer>();
        this.positions = new Vector3[sides];
        for (int i = 0; i < sides; i++) {
            float radian = Mathf.PI * 2.0f * i / sides;
            positions[i] = new Vector3(Mathf.Cos(radian), Mathf.Sin(radian));
        }
        lr.positionCount = sides;
        lr.SetPositions(positions);
        lr.loop = true;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        for (int i = 0; i < positions.Length; i++) {
            positions[i] = Rotate(positions[i], 2, 3.0f);
        }

        Vector3[] positions2D = new Vector3[lr.positionCount];

        System.Array.Copy(positions, positions2D, lr.positionCount);

        for (int i = 0; i < positions2D.Length; i++) positions2D[i] = (Vector2)positions2D[i];

        lr.SetPositions(positions2D);
    }

    public Vector3 Rotate(Vector3 vector, int axis, float aDegree) {
        if (axis == 0) return Quaternion.Euler(aDegree, 0, 0) * vector;
        else if (axis == 1) return Quaternion.Euler(0, aDegree, 0) * vector;
        else return Quaternion.Euler(0, 0, aDegree) * vector;
    }
}
