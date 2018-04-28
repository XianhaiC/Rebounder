using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteEnemy : MonoBehaviour {
    

    private LineRenderer lr;
    private Vector3[] positions;
    private float rotationSpeed;
    // Use this for initialization
    void Start() {
    }

    public void Initialize() {
        this.lr = GetComponent<LineRenderer>();
        this.rotationSpeed = 2.0f;
    }

    // Update is called once per frame
    void FixedUpdate() {
        transform.Rotate(Vector3.forward, rotationSpeed);
        /*
        for (int i = 0; i < positions.Length; i++) {
            positions[i] = Rotate(positions[i], 1, -2.0f);
        }
        lr.SetPositions(positions);*/
    }

    public Vector3 Rotate(Vector3 vector, int axis, float aDegree) {
        if (axis == 0) return Quaternion.Euler(aDegree, 0, 0) * vector;
        else if (axis == 1) return Quaternion.Euler(0, aDegree, 0) * vector;
        else return Quaternion.Euler(0, 0, aDegree) * vector;
    }

    public void SetSides(int sides) {
        Vector3[] newPositions = new Vector3[sides];
        for (int i = 0; i < sides; i++) {
            float radians = Mathf.PI * 2.0f * i / sides;
            newPositions[i] = new Vector3(Mathf.Cos(radians), Mathf.Sin(radians));
        }
        lr.positionCount = sides;
        lr.SetPositions(newPositions);
        lr.loop = true;
    }

    public IEnumerator InvulnerableFrames(float invulnDuration) {
        float invulnEnd = Time.time + invulnDuration;
        float startTime = Time.time;
        Vector3 originalScale = transform.localScale;
        while (Time.time < invulnEnd) {
            float timeElapsed = Time.time - startTime;
            float radian = Mathf.PI * 2.0f * timeElapsed / invulnDuration;
            transform.localScale = originalScale * (0.25f * Mathf.Sin(radian) + 1.0f);
            yield return new WaitForFixedUpdate();
        }
        transform.localScale = originalScale;
    }

    public IEnumerator Reset(int sides) {
        SetSides(sides);
        Vector3 originalScale = transform.localScale;
        transform.localScale = Vector3.one * 30.0f;
        while (Vector3.Distance(transform.localScale, originalScale) > 100 * Vector3.kEpsilon) {
            transform.localScale = Vector3.Lerp(transform.localScale, originalScale, 0.1f);
            yield return new WaitForFixedUpdate();
        }
    }
}
