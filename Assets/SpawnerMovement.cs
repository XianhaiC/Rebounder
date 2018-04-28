using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerMovement : MonoBehaviour {

	// Use this for initialization
	void Start () {
        /*if (Input.GetKeyDown("f")) {
            IEnumerator moveCoroutine = MoveOrbit(new Vector2(-6, 0), new Vector2(0, 0), 60, 20);
            StartCoroutine(moveCoroutine);
        }*/
    }
	
	// Update is called once per frame
	void Update () {
    }

    public IEnumerator MoveStraight(Vector2 start, Vector2 velocity, float speed, float duration) {
        transform.position = start;
        float durationEnd = Time.time + duration;
        while (Time.time < durationEnd) {
            transform.Translate(velocity.normalized * speed * Time.deltaTime);
            yield return new WaitForFixedUpdate();
        }
    }

    public IEnumerator MoveOrbit(Vector2 start, Vector2 point, float rotationSpeed, float duration) {
        transform.position = start;
        float durationEnd = Time.time + duration;
        while (Time.time < durationEnd) {
            transform.RotateAround(point, Vector3.forward, rotationSpeed * Time.deltaTime);
            yield return new WaitForFixedUpdate();
        }
    }

    public IEnumerator MoveRotate(float angle, float rotationSpeed, float duration) {
        transform.eulerAngles = Vector3.forward * angle;
        float durationEnd = Time.time + duration;
        while (Time.time < durationEnd) {
            transform.Rotate(Vector3.forward * rotationSpeed * Time.deltaTime);
            Debug.Log(transform.eulerAngles);
            yield return new WaitForFixedUpdate();
        }
    }
}
