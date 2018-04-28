using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrierControl : MonoBehaviour {
    public float radius;
    public float dampening;

    private Rigidbody2D rb;

    // Use this for initialization
    void Start() {
        this.rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update() {

        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 offset = Vector3.Normalize(mousePos - transform.parent.position) * radius;
        Vector3 newPos = offset + transform.parent.position;
        rb.MovePosition(Vector2.Lerp(rb.position, new Vector2(newPos.x, newPos.y), 0.8f));
        //rb.MovePosition(Vector2.Lerp(rb.position, new Vector2(mousePos.x, mousePos.y), 0.5f));

        //float rotz = Mathf.Atan2(transform.localPosition.y, transform.localPosition.x) * Mathf.Rad2Deg;
        //rb.MoveRotation(Mathf.LerpAngle(rb.rotation, rotz + 90.0f, 0.8f));


        rb.MoveRotation(Quaternion.FromToRotation(Vector3.down, -transform.localPosition).eulerAngles.z);
        /*      
                //transform.rotation = Quaternion.LookRotation(transform.localPosition, Vector3.down);
                Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.parent.position;
                //transform.localPosition = new Vector3(mousePos.x, mousePos.y, 0.0f);
                transform.localPosition = Vector3.Lerp(transform.localPosition, new Vector3(mousePos.x, mousePos.y, 0.0f), 0.5f);
                Debug.Log(transform.localPosition);
                transform.rotation = Quaternion.FromToRotation(Vector3.right, -transform.localPosition);*/
    }
}
