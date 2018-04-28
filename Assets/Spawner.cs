using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour {
    public GameObject enemy;
    public GameObject pf_AttackStraight;
    public GameObject pf_AttackSpin;
    public GameObject pf_AttackRandom;
    public GameObject pf_AttackBurst;

    private Rigidbody2D rb;

	// Use this for initialization
	void Start () {
        this.rb = GetComponent<Rigidbody2D>();
    }
	
    public void Initialize(GameObject enemy) {
        this.enemy = enemy;
    }
	// Update is called once per frame
	void Update () {
        
	}

    public void Destroy(float delay, bool keepChildren) {
        if (keepChildren) transform.DetachChildren();
        Destroy(gameObject, delay);
    }

    public void DestroyAttacks(float delay) {
        foreach(Transform child in transform) { 
            if (child.tag.Equals("Attack")) Destroy(child.gameObject, delay);
        }
    }
    
    public void AttackStraight(
        GameObject player,
        Vector2 velocity,
        float speed,
        Vector2 force,
        float acceleration,
        int fireRate,
        int damage,
        float duration,
        bool reboundable,
        bool aimPlayer,
        bool homePlayer,
        bool local) {
        GameObject attack = Instantiate(pf_AttackStraight, transform);
        attack.GetComponent<AttackStraight>().Initialize(player, velocity, speed, force, acceleration, fireRate, damage, duration, reboundable, aimPlayer, homePlayer, local);
    }

    public void AttackSpin(
        GameObject player,
        float angle,
        float rotationSpeed,
        float speed,
        Vector2 force,
        float acceleration,
        int fireRate,
        int damage,
        float duration,
        bool reboundable,
        bool aimPlayer,
        bool homePlayer,
        bool local) {
        GameObject attack = Instantiate(pf_AttackSpin, transform);
        attack.GetComponent<AttackSpin>().Initialize(player, angle, rotationSpeed, speed, force, acceleration, fireRate, damage, duration, reboundable, aimPlayer, homePlayer, local);
    }

    public void AttackRandom(
        GameObject player,
        float spread,
        bool randomSpeed,
        Vector2 velocity,
        float speed,
        Vector2 force,
        float acceleration,
        int fireRate,
        int damage,
        float duration,
        bool reboundable,
        bool aimPlayer,
        bool homePlayer,
        bool local) {
        GameObject attack = Instantiate(pf_AttackRandom, transform);
        attack.GetComponent<AttackRandom>().Initialize(player, spread, randomSpeed, velocity, speed, force, acceleration, fireRate, damage, duration, reboundable, aimPlayer, homePlayer, local);
    }

    public void AttackBurst(
        GameObject player,
        int bullets,
        Vector2 velocity,
        float speed,
        Vector2 force,
        float acceleration,
        int fireRate,
        int damage,
        float duration,
        bool reboundable,
        bool aimPlayer,
        bool homePlayer,
        bool local) {
        GameObject attack = Instantiate(pf_AttackBurst, transform);
        attack.GetComponent<AttackBurst>().Initialize(player, bullets, velocity, speed, force, acceleration, fireRate, damage, duration, reboundable, aimPlayer, homePlayer, local);
    }

    public IEnumerator AttackStraightBurstRandom(
        GameObject player,
        GameObject boundary,
        float boundaryPadding,
        float durationBurst,
        Vector2 velocity,
        float speed,
        Vector2 force,
        float acceleration,
        int fireRate,
        int damage,
        float duration,
        bool reboundable,
        bool aimPlayer,
        bool homePlayer,
        bool local) {

        float durationEnd = Time.time + durationBurst;
        while (Time.time <= durationEnd) {
            yield return StartCoroutine(MoveTo(new Vector2(
                Random.Range(-boundary.transform.localScale.x / 2 + boundaryPadding,
                    boundary.transform.localScale.x / 2 - boundaryPadding),
                Random.Range(boundary.transform.localScale.y / 4,
                    boundary.transform.localScale.y / 2 - boundaryPadding)), 15.0f, true, true));

            AttackStraight(player, velocity, speed, force, acceleration, fireRate, damage, duration, reboundable, aimPlayer, homePlayer, local);
            yield return new WaitForSeconds(duration);
        }
    }

    public IEnumerator AttackCircularBurstRandom(
        GameObject player,
        GameObject boundary,
        float boundaryPadding,
        float durationBurst,
        int bullets,
        Vector2 velocity,
        float speed,
        Vector2 force,
        float acceleration,
        int fireRate,
        int damage,
        float duration,
        bool reboundable,
        bool aimPlayer,
        bool homePlayer,
        bool local) {

        float durationEnd = Time.time + durationBurst;
        while (Time.time <= durationEnd) {
            yield return StartCoroutine(MoveTo(new Vector2(
                Random.Range(-boundary.transform.localScale.x / 2 + boundaryPadding,
                    boundary.transform.localScale.x / 2 - boundaryPadding),
                Random.Range(boundary.transform.localScale.y / 4,
                    boundary.transform.localScale.y / 2 - boundaryPadding)), 20.0f, true, true));

            AttackBurst(player, bullets, velocity, speed, force, acceleration, fireRate, damage, duration, reboundable, aimPlayer, homePlayer, local);
            yield return new WaitForSeconds(duration);
        }
    }

    public IEnumerator AttackShotgunBlast(
        GameObject player,
        GameObject boundary,
        float durationBurst,
        float spread,
        bool randomSpeed,
        Vector2 position,
        Vector2 velocity,
        float speed,
        Vector2 force,
        float acceleration,
        int fireRate,
        int damage,
        float duration,
        float durationDelay,
        bool reboundable,
        bool aimPlayer,
        bool homePlayer,
        bool local) {

        float durationEnd = Time.time + durationBurst;
        transform.position = position;
        while (Time.time <= durationEnd) {
            
            AttackRandom(player, spread, randomSpeed, velocity, speed, force, acceleration, fireRate, damage, duration, reboundable, aimPlayer, homePlayer, local);
            yield return new WaitForSeconds(duration + durationDelay);
        }
    }

    public IEnumerator MoveTo(Vector2 destination, float speed, bool lerp, bool enemyBreak) {
        while (Vector2.Distance(destination, (Vector2)transform.position) >= Vector2.kEpsilon * 1000) {
            if (enemyBreak && !enemy.GetComponent<EnemyController>().IsAlive()) yield break;
            if (lerp) {
                float step = Mathf.Clamp(speed * Time.fixedDeltaTime, 0.0f, 1.0f);
                transform.position = Vector2.Lerp(transform.position, destination, step);
            }
            else {
                float step = speed * Time.fixedDeltaTime;
                Debug.Log(transform.position + " time: " + Time.fixedDeltaTime);
                transform.position = Vector2.MoveTowards(transform.position, destination, step);
            }
            
            yield return new WaitForFixedUpdate();
        }
        
        yield return null;
    }

    public void MoveToStart(Vector2 destination, float speed, bool lerp, bool enemyBreak) {
        StartCoroutine(MoveTo(destination, speed, lerp, enemyBreak));
    }

    public IEnumerator MoveStraight(Vector2 start, Vector2 velocity, float speed, float duration, bool enemyBreak) {
        transform.position = start;
        float durationEnd = Time.time + duration;
        while (Time.time < durationEnd) {
            if (enemyBreak && !enemy.GetComponent<EnemyController>().IsAlive()) yield break;
            transform.Translate(velocity.normalized * speed * Time.fixedDeltaTime);
            yield return new WaitForFixedUpdate();
        }
    }

    public void MoveStraightStart(Vector2 start, Vector2 velocity, float speed, float duration, bool enemyBreak) {
        StartCoroutine(MoveStraight(start, velocity, speed, duration, enemyBreak));
    }

    public IEnumerator MoveOrbit(Vector2 start, Vector2 point, float rotationSpeed, float duration, bool enemyBreak) {
        transform.position = start;
        float durationEnd = Time.time + duration;
        while (Time.time < durationEnd) {
            if (enemyBreak && !enemy.GetComponent<EnemyController>().IsAlive()) yield break;
            transform.RotateAround(point, Vector3.forward, rotationSpeed * Time.fixedDeltaTime);
            yield return new WaitForFixedUpdate();
        }
    }

    public void MoveOrbitStart(Vector2 start, Vector2 point, float rotationSpeed, float duration, bool enemyBreak) {
        StartCoroutine(MoveOrbit(start, point, rotationSpeed, duration, enemyBreak));
    }

    public IEnumerator MoveRotate(float angle, float rotationSpeed, float duration, bool enemyBreak) {
        transform.eulerAngles = Vector3.forward * angle;
        float durationEnd = Time.time + duration;
        while (Time.time < durationEnd) {
            if (enemyBreak && !enemy.GetComponent<EnemyController>().IsAlive()) yield break;
            transform.Rotate(Vector3.forward * rotationSpeed * Time.fixedDeltaTime);
            yield return new WaitForFixedUpdate();
        }
    }

    public void MoveRotateStart(float angle, float rotationSpeed, float duration, bool enemyBreak) {
        StartCoroutine(MoveRotate(angle, rotationSpeed, duration, enemyBreak));
    }

    public IEnumerator MoveRotateInterval(float angle, float angleWidth, float cycleSpeed, float duration, bool enemyBreak) {
        transform.eulerAngles = Vector3.forward * angle;
        float durationEnd = Time.time + duration;
        float counter = 0;
        Quaternion initialRotation = transform.rotation;
        while (Time.time < durationEnd) {
            if (enemyBreak && !enemy.GetComponent<EnemyController>().IsAlive()) yield break;
            transform.rotation = initialRotation;
            float deltaRot = angleWidth * Mathf.Sin(Mathf.Deg2Rad * counter);
            transform.Rotate(Vector3.forward * deltaRot);
            counter = (counter + cycleSpeed * Time.fixedDeltaTime) % 360.0f;
            yield return new WaitForFixedUpdate();
        }
    }

    public IEnumerator MoveSlide(float slideWidth, float cycleSpeed, bool horizontal, bool smooth, float duration, bool enemyBreak) {
        float durationEnd = Time.time + duration;
        float counter = 0;
        Vector3 initialPos = transform.position;
        while (Time.time < durationEnd) {
            if (enemyBreak && !enemy.GetComponent<EnemyController>().IsAlive()) yield break;
            float deltaPos = slideWidth * Mathf.Sin(Mathf.Deg2Rad * counter);
            if (horizontal) transform.position = initialPos + new Vector3(deltaPos, 0.0f, 0.0f);
            else transform.position = initialPos + new Vector3(0.0f, deltaPos, 0.0f);

            counter = (counter + cycleSpeed * Time.fixedDeltaTime) % 360.0f;
            yield return new WaitForFixedUpdate();
        }
    }
}
