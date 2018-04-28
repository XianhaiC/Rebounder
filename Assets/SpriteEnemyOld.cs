using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteEnemyOld : MonoBehaviour {

    private float orbitOffset = 2.12f;
    private float orbitRadius = 1.0f;
    private Vector3[] positions;
    private LineRenderer lr;
    private Vector4[] rotationMatrix4D;
    // Use this for initialization
	void Start () {
        this.lr = GetComponent<LineRenderer>();
        this.positions = new Vector3[lr.positionCount];
        /*
        this.rotationMatrix4D = {
            new Vector4(1.0f, 0.0f, 0.0f, 0.0f),
            new Vector4(0.0f, 1.0f, 0.0f, 0.0f),
            new Vector4(0.0f, 0.0f, Mat, 0.0f),
        }*/
        lr.GetPositions(positions);
    }

    // Update is called once per frame
    void FixedUpdate() {
        for (int i = 0; i < positions.Length; i++) {
            Vector3 positionFlat = new Vector3(positions[i].x, 0.0f, positions[i].z);
            Vector3 offsetDirection = positionFlat.normalized;
            Vector3 positionHorizontal = positionFlat - offsetDirection * orbitOffset;
            float angle = Mathf.Atan2(positions[i].y, Vector3.Dot(offsetDirection, positionHorizontal));
            float speed = 0.05f;
            Vector3 newPosition = offsetDirection * Mathf.Cos(angle + speed) * 1.0f + Vector3.up * Mathf.Sin(angle + speed) * 1.1f + offsetDirection * orbitOffset;
            positions[i] = newPosition;

        }
        lr.SetPositions(positions);
	}

    public Vector4 Rotate4D(Vector4 vector, float degree) {
        return new Vector4(
            vector.x,
            vector.y,
            Mathf.Cos(degree * Mathf.Deg2Rad) * vector.z - Mathf.Sin(degree * Mathf.Deg2Rad) * vector.w,
            Mathf.Sin(degree * Mathf.Deg2Rad) * vector.z + Mathf.Cos(degree * Mathf.Deg2Rad) * vector.w);
        
    }

    public Vector3 Project4D(Vector4 vector) {
        float lw = 4.0f;
        return new Vector3(
            vector.x / (lw - vector.w),
            vector.y / (lw - vector.w),
            vector.z / (lw - vector.w));
    }
}
