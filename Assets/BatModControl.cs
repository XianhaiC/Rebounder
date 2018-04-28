using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatModControl : MonoBehaviour {
    public float radius;
    public float dampening;
    public float speedMax;

    private Rigidbody2D rb;
    private Vector3 mousePosPrev;
    private Vector3 barrierPosLocal;
    // Use this for initialization
    void Start() {
        this.rb = GetComponent<Rigidbody2D>();
        barrierPosLocal = transform.localPosition;
    }

    // Update is called once per frame
    void FixedUpdate() {
        Vector3 mousePosNew = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 mousePosDelta = mousePosNew - mousePosPrev;
        barrierPosLocal = (barrierPosLocal + mousePosDelta * 0.05f).normalized;
        Vector3 barrierPosNew = barrierPosLocal * radius + transform.parent.position;
        rb.MovePosition(new Vector2(barrierPosNew.x, barrierPosNew.y));
        rb.MoveRotation(Quaternion.FromToRotation(Vector3.right, -transform.localPosition).eulerAngles.z);
        mousePosPrev = mousePosNew;
    }
}
