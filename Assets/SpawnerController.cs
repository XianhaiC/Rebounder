using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerController : MonoBehaviour {
    private static readonly float BoundaryPadding = 1.0f;

    public GameObject playerController;
    public GameObject enemy;
    public GameObject boundary;
    public GameObject pf_Spawner;

    private EnemyController enemyController;
    private List<GameObject> spawners;
    private List<IEnumerator> coroutines;
    private List<IEnumerator> attacks;
    // Use this for initialization
    void Start () {
        this.enemyController = enemy.GetComponent<EnemyController>();
        spawners = new List<GameObject>();
        for (int i = 0; i < 10; i++) {
            spawners.Add(Instantiate(pf_Spawner, transform.position, transform.rotation));
            spawners[i].GetComponent<Spawner>().Initialize(enemy);
        }
        this.coroutines = new List<IEnumerator>();
        this.attacks = new List<IEnumerator>();
        StartCoroutine(Phase2());
    }

    // Update is called once per frame
    void Update () {
		
	}

    IEnumerator Phase1() {
        attacks.Add(SequenceAttackStraighDownRandom());
        attacks.Add(SequenceAttackStraightHorizontalRandom());
        attacks.Add(SequenceAttackStraightTargetRandom());
        attacks.Add(SequenceAttackBurstRandom());
        attacks.Add(SequenceAttackRandomShotgunPulse());
        attacks.Add(SequenceAttackRandomStreakDown());
        attacks.Add(SequenceAttackPopcorn());

        for (int i = 0; i < attacks.Count; i++) {
            yield return StartCoroutine(enemyController.Reset(enemyController.healthInitial));
            yield return StartCoroutine(attacks[i]);
            DestroySpawnerAttacks();
            yield return StartCoroutine(MoveSpawnersEnemyCenterSlow());
        }
    }

    IEnumerator Phase2() {
        attacks.Add(SequenceAttackOrbitSpin());
        attacks.Add(SequenceAttackBurstSwirlSlideVertical());
        attacks.Add(SequenceAttackSwirl());
        attacks.Add(SequenceAttackSwirlDoubleSlideHorizontal());
        attacks.Add(SequenceAttackSwirlDoubleSlideVertical());
        attacks.Add(SequenceAttackSlideStraight());
        
        attacks.Add(SequenceAttackBurstSwirl());

        for (int i = 0; i < attacks.Count; i++) {
            yield return StartCoroutine(enemyController.Reset(enemyController.healthInitial));
            yield return StartCoroutine(attacks[i]);
            DestroySpawnerAttacks();
            yield return StartCoroutine(MoveSpawnersEnemyCenterSlow());
        }
    }

    // PHASE 1 //

    IEnumerator SequenceAttackStraighDownRandom() {
        CoroutinesClear();
        MoveSpawnersEnemyCenter();
        float durationAttack = 10.0f;
        float durationEnd = Time.time + durationAttack;
        while (enemyController.IsAlive()) {
            CoroutinesClear();
            Debug.Log(coroutines.Count);
		    for (int i = 0; i < 4; i++) {
			    coroutines.Add(spawners[i].GetComponent<Spawner>().MoveTo(new Vector2(
		            Random.Range(-boundary.transform.localScale.x / 2 + BoundaryPadding,
				        boundary.transform.localScale.x / 2 - BoundaryPadding),
				    Random.Range(boundary.transform.localScale.y / 4,
					    boundary.transform.localScale.y / 2 - BoundaryPadding)), 15.0f, true, true));
                StartCoroutine(coroutines[i]);
		    }

            yield return StartCoroutine(WaitForCoroutines(coroutines));

		    for (int i = 0; i < 4; i++) {
			    spawners[i].GetComponent<Spawner>().AttackStraight(playerController, Vector2.down, 20.0f, Vector2.zero, 0.0f, 10, 1, 1.0f, false, false, false, false);
		    }

            yield return StartCoroutine(WaitForSecondsCheck(1.0f));
        }
    }

    IEnumerator SequenceAttackStraightTargetRandom() {
        CoroutinesClear();
        MoveSpawnersEnemyCenter();
        float durationAttack = 10.0f;
        float durationEnd = Time.time + durationAttack;
        while (enemyController.IsAlive()) {
            CoroutinesClear();
            for (int i = 0; i < 4; i++) {
                coroutines.Add(spawners[i].GetComponent<Spawner>().MoveTo(new Vector2(
                    Random.Range(-boundary.transform.localScale.x / 2 + BoundaryPadding,
                        boundary.transform.localScale.x / 2 - BoundaryPadding),
                    Random.Range(boundary.transform.localScale.y / 4,
                        boundary.transform.localScale.y / 2 - BoundaryPadding)), 15.0f, true, true));
                StartCoroutine(coroutines[i]);
            }

            yield return StartCoroutine(WaitForCoroutines(coroutines));

            for (int i = 0; i < 4; i++) {
                spawners[i].GetComponent<Spawner>().AttackStraight(playerController, Vector2.down, 20.0f, Vector2.zero, 0.0f, 10, 1, 1.0f, false, true, false, false);
            }

            yield return StartCoroutine(WaitForSecondsCheck(1.0f));
        }
    }

    IEnumerator SequenceAttackBurstRandom() {
        CoroutinesClear();
        MoveSpawnersEnemyCenter();
        float durationAttack = 10.0f;
        float durationEnd = Time.time + durationAttack;
        while (enemyController.IsAlive()) {
            CoroutinesClear();
            for (int i = 0; i < 4; i++) {
                coroutines.Add(spawners[i].GetComponent<Spawner>().MoveTo(new Vector2(
                    Random.Range(-boundary.transform.localScale.x / 2 + BoundaryPadding,
                        boundary.transform.localScale.x / 2 - BoundaryPadding),
                    Random.Range(boundary.transform.localScale.y / 4,
                        boundary.transform.localScale.y / 2 - BoundaryPadding)), 15.0f, true, true));
                StartCoroutine(coroutines[i]);
            }

            yield return StartCoroutine(WaitForCoroutines(coroutines));

            for (int i = 0; i < 4; i++) {
                spawners[i].GetComponent<Spawner>().AttackBurst(playerController, 20, Vector2.down, 20.0f, Vector2.zero, 0.0f, 1, 1, 1.0f, false, false, false, false);
            }
            yield return StartCoroutine(WaitForSecondsCheck(1.0f));
        }
    }

    IEnumerator SequenceAttackRandomShotgunPulse() {
        CoroutinesClear();
        MoveSpawnersEnemyCenter();
        float durationAttack = 10.0f;
        float durationEnd = Time.time + durationAttack;
        coroutines.Add(spawners[0].GetComponent<Spawner>().MoveTo(new Vector2(-30, 40), 15.0f, true, true));
        coroutines.Add(spawners[1].GetComponent<Spawner>().MoveTo(new Vector2(30, 40), 15.0f, true, true));
        StartCoroutine(coroutines[0]);
        StartCoroutine(coroutines[1]);

        yield return StartCoroutine(WaitForCoroutines(coroutines));

        while (enemyController.IsAlive()) {
            spawners[0].GetComponent<Spawner>().AttackRandom(playerController, 45.0f, false, new Vector2(1, -2), 20.0f, Vector2.zero, 0.0f, 60, 1, 0.5f, false, false, false, false);
            spawners[1].GetComponent<Spawner>().AttackRandom(playerController, 45.0f, false, new Vector2(-1, -2), 20.0f, Vector2.zero, 0.0f, 60, 1, 0.5f, false, false, false, false);
            yield return StartCoroutine(WaitForSecondsCheck(1.5f));
        }
    }

    IEnumerator SequenceAttackRandomStreakDown() {
        while (enemyController.IsAlive()) {
            CoroutinesClear();
            MoveSpawnersEnemyCenter();

            coroutines.Add(spawners[0].GetComponent<Spawner>().MoveTo(new Vector2(-30, 40), 15.0f, true, true));
            coroutines.Add(spawners[1].GetComponent<Spawner>().MoveTo(new Vector2(30, 40), 15.0f, true, true));


            StartCoroutine(coroutines[0]);
            StartCoroutine(coroutines[1]);

            yield return StartCoroutine(WaitForCoroutines(coroutines));
            CoroutinesClear();

            float target = -boundary.transform.localScale.y / 2;

            coroutines.Add(spawners[0].GetComponent<Spawner>().MoveTo(new Vector2(-30, target), 10.0f, false, true));
            coroutines.Add(spawners[1].GetComponent<Spawner>().MoveTo(new Vector2(30, target), 10.0f, false, true));
            
            StartCoroutine(coroutines[0]);
            StartCoroutine(coroutines[1]);

            spawners[0].GetComponent<Spawner>().AttackRandom(playerController, 360.0f, false, Vector2.down, 8.0f, Vector2.zero, 0.0f, 60, 1, 60.0f, false, false, false, false);
            spawners[1].GetComponent<Spawner>().AttackRandom(playerController, 360.0f, false, Vector2.down, 8.0f, Vector2.zero, 0.0f, 60, 1, 60.0f, false, false, false, false);

            yield return StartCoroutine(WaitForCoroutines(coroutines));
        }
    }

    IEnumerator SequenceAttackPopcorn() {
        CoroutinesClear();
        MoveSpawnersEnemyCenter();
        float durationAttack = 10.0f;
        float durationEnd = Time.time + durationAttack;

        coroutines.Add(spawners[0].GetComponent<Spawner>().MoveTo(new Vector2(0, 30), 8.0f, true, true));

        StartCoroutine(coroutines[0]);

        yield return StartCoroutine(WaitForCoroutines(coroutines));

        spawners[0].GetComponent<Spawner>().AttackRandom(playerController, 45.0f, false, Vector2.up, 15.0f, Vector2.down, 9.0f, 60, 1, 60.0f, false, false, false, false);

        while (enemyController.IsAlive()) {
            yield return null;
        }
        DestroySpawnerAttacks();
    }

    IEnumerator SequenceAttackRandomBurstOrbit() {
        CoroutinesClear();
        MoveSpawnersEnemyCenter();
        float durationAttack = 10.0f;
        float durationEnd = Time.time + durationAttack;

        coroutines.Add(spawners[0].GetComponent<Spawner>().MoveTo(new Vector2(-30, -10), 7.0f, true, true));
        coroutines.Add(spawners[1].GetComponent<Spawner>().MoveTo(new Vector2(30, -10), 7.0f, true, true));

        StartCoroutine(coroutines[0]);
        StartCoroutine(coroutines[1]);

        while (CoroutinesUnfinished(coroutines)) {
            yield return null;
        }
        
        coroutines.Add(spawners[0].GetComponent<Spawner>().MoveOrbit(spawners[0].GetComponent<Transform>().position, new Vector2(0, -10), 90.0f, durationAttack, true));
        coroutines.Add(spawners[1].GetComponent<Spawner>().MoveOrbit(spawners[1].GetComponent<Transform>().position, new Vector2(0, -10), 90.0f, durationAttack, true));

        StartCoroutine(coroutines[2]);
        StartCoroutine(coroutines[3]);
        
        while (CoroutinesUnfinished(coroutines)) {
            spawners[0].GetComponent<Spawner>().AttackRandom(playerController, 360.0f, false, Vector2.down, 10.0f, Vector2.zero, 0.0f, 60, 1, 0.5f, false, false, false, false);
            spawners[1].GetComponent<Spawner>().AttackRandom(playerController, 360.0f, false, Vector2.down, 10.0f, Vector2.zero, 0.0f, 60, 1, 0.5f, false, false, false, false);
            yield return new WaitForSeconds(0.5f + 1.0f);
        }
    }

    IEnumerator SequenceAttackStraightHorizontalRandom() {
        CoroutinesClear();
        MoveSpawnersEnemyCenter();
        float durationAttack = 10.0f;
        float durationEnd = Time.time + durationAttack;
        while (enemyController.IsAlive()) {
            CoroutinesClear();
            for (int i = 0; i < 8; i++) {
                if (i < 4) { 
                    coroutines.Add(spawners[i].GetComponent<Spawner>().MoveTo(new Vector2(
                        -boundary.transform.localScale.x / 2 + BoundaryPadding,
                        Random.Range(-boundary.transform.localScale.y / 2 + BoundaryPadding,
                            boundary.transform.localScale.y / 2 - BoundaryPadding)), 15.0f, true, true));
                }
                else {
                    coroutines.Add(spawners[i].GetComponent<Spawner>().MoveTo(new Vector2(
                        boundary.transform.localScale.x / 2 - BoundaryPadding,
                        Random.Range(-boundary.transform.localScale.y / 2 + BoundaryPadding,
                            boundary.transform.localScale.y / 2 - BoundaryPadding)), 15.0f, true, true));
                }
                StartCoroutine(coroutines[i]);
            }

            yield return StartCoroutine(WaitForCoroutines(coroutines));

            for (int i = 0; i < 8; i++) {
                if (i < 4) {
                    spawners[i].GetComponent<Spawner>().AttackStraight(playerController, Vector2.right, 15.0f, Vector2.zero, 0.0f, 10, 1, 1.0f, false, false, false, false);
                }
                else {
                    spawners[i].GetComponent<Spawner>().AttackStraight(playerController, Vector2.left, 15.0f, Vector2.zero, 0.0f, 10, 1, 1.0f, false, false, false, false);
                }
            }
            yield return StartCoroutine(WaitForSecondsCheck(1.0f));
        }
    }

    // PHASE 2 //

    IEnumerator SequenceAttackSwirl() {
        CoroutinesClear();
        MoveSpawnersEnemyCenter();
        coroutines.Add(spawners[0].GetComponent<Spawner>().MoveTo(Vector2.zero, 15.0f, true, false));
        StartCoroutine(coroutines[0]);

        yield return StartCoroutine(WaitForCoroutines(coroutines));

        spawners[0].GetComponent<Spawner>().AttackSpin(playerController, 0.0f, 361.0f, 10.0f, Vector2.zero, 10.0f, 40, 1, 1000.0f, false, false, false, false);

        yield return StartCoroutine(WaitForSecondsCheck(60.0f));
    }

    IEnumerator SequenceAttackSwirlDoubleSlideHorizontal() {
        CoroutinesClear();
        MoveSpawnersEnemyCenter();
        coroutines.Add(spawners[0].GetComponent<Spawner>().MoveTo(new Vector2(0.0f, 20.0f), 15.0f, true, false));
        coroutines.Add(spawners[1].GetComponent<Spawner>().MoveTo(new Vector2(0.0f, 20.0f), 15.0f, true, false));
        StartCoroutine(coroutines[0]);
        StartCoroutine(coroutines[1]);

        yield return StartCoroutine(WaitForCoroutines(coroutines));

        spawners[0].GetComponent<Spawner>().AttackSpin(playerController, 0.0f, 361.0f, 10.0f, Vector2.zero, 10.0f, 60, 1, 10000.0f, false, false, false, false);
        spawners[1].GetComponent<Spawner>().AttackSpin(playerController, 180.0f, -361.0f, 10.0f, Vector2.zero, 10.0f, 60, 1, 10000.0f, false, false, false, false);

        CoroutinesClear();

        coroutines.Add(spawners[0].GetComponent<Spawner>().MoveSlide(20.0f, 50.0f, true, true, 10000.0f, true));
        coroutines.Add(spawners[1].GetComponent<Spawner>().MoveSlide(20.0f, -50.0f, true, true, 10000.0f, true));
        StartCoroutine(coroutines[0]);
        StartCoroutine(coroutines[1]);

        yield return StartCoroutine(WaitForSecondsCheck(10000.0f));
    }

    IEnumerator SequenceAttackSwirlDoubleSlideVertical() {
        CoroutinesClear();
        MoveSpawnersEnemyCenter();
        coroutines.Add(spawners[0].GetComponent<Spawner>().MoveTo(new Vector2(-20.0f, 0.0f), 15.0f, true, false));
        coroutines.Add(spawners[1].GetComponent<Spawner>().MoveTo(new Vector2(20.0f, 0.0f), 15.0f, true, false));
        StartCoroutine(coroutines[0]);
        StartCoroutine(coroutines[1]);

        yield return StartCoroutine(WaitForCoroutines(coroutines));

        spawners[0].GetComponent<Spawner>().AttackSpin(playerController, 0.0f, 361.0f, 10.0f, Vector2.zero, 10.0f, 60, 1, 10000.0f, false, false, false, false);
        spawners[1].GetComponent<Spawner>().AttackSpin(playerController, 180.0f, -361.0f, 10.0f, Vector2.zero, 10.0f, 60, 1, 10000.0f, false, false, false, false);

        CoroutinesClear();

        coroutines.Add(spawners[0].GetComponent<Spawner>().MoveSlide(30.0f, 50.0f, false, true, 10000.0f, true));
        coroutines.Add(spawners[1].GetComponent<Spawner>().MoveSlide(30.0f, 50.0f, false, true, 10000.0f, true));
        StartCoroutine(coroutines[0]);
        StartCoroutine(coroutines[1]);

        yield return StartCoroutine(WaitForSecondsCheck(10000.0f));
    }

    IEnumerator SequenceAttackSlideStraight() {
        CoroutinesClear();
        MoveSpawnersEnemyCenter();
        for (int i = 0; i < 6; i++) {
            if (i < 3) coroutines.Add(spawners[i].GetComponent<Spawner>().MoveTo(new Vector2(-25.0f, 0.0f), 15.0f, true, false));
            else coroutines.Add(spawners[i].GetComponent<Spawner>().MoveTo(new Vector2(25.0f, 0.0f), 15.0f, true, false));
            StartCoroutine(coroutines[i]);
        }

        yield return StartCoroutine(WaitForCoroutines(coroutines));
        CoroutinesClear();

        for (int i = 0; i < 3; i++) {
            spawners[i].GetComponent<Spawner>().AttackStraight(playerController, Vector2.right, 15.0f, Vector2.zero, 10.0f, 10, 1, 10000.0f, false, false, false, false);
            coroutines.Add(spawners[i].GetComponent<Spawner>().MoveSlide(53.0f, 30.0f, false, true, 10000.0f, true));
            spawners[i + 3].GetComponent<Spawner>().AttackStraight(playerController, Vector2.left, 15.0f, Vector2.zero, 10.0f, 10, 1, 10000.0f, false, false, false, false);
            coroutines.Add(spawners[i + 3].GetComponent<Spawner>().MoveSlide(53.0f, -30.0f, false, true, 10000.0f, true));
            StartCoroutine(coroutines[2 * i]);
            StartCoroutine(coroutines[2 * i + 1]);
            yield return StartCoroutine(WaitForSecondsCheck(1.0f));
        }

        yield return StartCoroutine(WaitForSecondsCheck(10000.0f));
    }

    IEnumerator SequenceAttackOrbitSpin() {
        CoroutinesClear();
        MoveSpawnersEnemyCenter();

        coroutines.Add(spawners[0].GetComponent<Spawner>().MoveTo(new Vector2(0.0f, -30.0f), 5.0f, true, false));
        StartCoroutine(coroutines[0]);

        yield return StartCoroutine(WaitForCoroutines(coroutines));
        CoroutinesClear();

        spawners[0].GetComponent<Spawner>().AttackSpin(playerController, 0.0f, -360.0f, 10.0f, Vector2.zero, 10.0f, 30, 1, 10000.0f, false, false, false, false);
        //spawners[0].GetComponent<Spawner>().AttackBurst(playerController, 20, Vector2.down, 10.0f, Vector2.zero, 10.0f, 5, 1, 1000.0f, false, false, false, false);
        coroutines.Add(spawners[0].GetComponent<Spawner>().MoveOrbit(spawners[0].GetComponent<Transform>().position, Vector2.zero, 30.0f, 10000.0f, true));
        StartCoroutine(coroutines[0]);

        yield return StartCoroutine(WaitForSecondsCheck(10000.0f));
    }

    IEnumerator SequenceAttackBurstSwirl() {
        CoroutinesClear();
        MoveSpawnersEnemyCenter();
        coroutines.Add(spawners[0].GetComponent<Spawner>().MoveTo(Vector2.zero, 15.0f, true, false));
        StartCoroutine(coroutines[0]);

        yield return StartCoroutine(WaitForCoroutines(coroutines));
        CoroutinesClear();

        spawners[0].GetComponent<Spawner>().AttackBurst(playerController, 6, Vector2.down, 15.0f, Vector2.zero, 30.0f, 6, 1, 10000.0f, false, false, false, false);

        coroutines.Add(spawners[0].GetComponent<Spawner>().MoveRotateInterval(0.0f, 360.0f, 30.0f, 10000.0f, true));
        StartCoroutine(coroutines[0]);

        yield return StartCoroutine(WaitForSecondsCheck(10000.0f));
    }

    IEnumerator SequenceAttackBurstSwirlSlideVertical() {
        CoroutinesClear();
        MoveSpawnersEnemyCenter();
        coroutines.Add(spawners[0].GetComponent<Spawner>().MoveTo(new Vector2(-32.0f, -20.0f), 15.0f, true, false));
        coroutines.Add(spawners[1].GetComponent<Spawner>().MoveTo(new Vector2(32.0f, -20.0f), 15.0f, true, false));
        StartCoroutine(coroutines[0]);
        StartCoroutine(coroutines[1]);

        yield return StartCoroutine(WaitForCoroutines(coroutines));


        spawners[0].GetComponent<Spawner>().AttackBurst(playerController, 4, new Vector2(1.0f, -1.0f), 50.0f, Vector2.zero, 30.0f, 10, 1, 10000.0f, false, false, false, false);
        spawners[1].GetComponent<Spawner>().AttackBurst(playerController, 4, new Vector2(1.0f, -1.0f), 50.0f, Vector2.zero, 30.0f, 10, 1, 10000.0f, false, false, false, false);
        
        CoroutinesClear();

        coroutines.Add(spawners[0].GetComponent<Spawner>().MoveSlide(30.0f, -30.0f, false, true, 10000.0f, true));
        coroutines.Add(spawners[1].GetComponent<Spawner>().MoveSlide(30.0f, -30.0f, false, true, 10000.0f, true));
        coroutines.Add(spawners[0].GetComponent<Spawner>().MoveRotateInterval(0.0f, 45.0f, 10.0f, 10000.0f, true));
        coroutines.Add(spawners[1].GetComponent<Spawner>().MoveRotateInterval(0.0f, 45.0f, -10.0f, 10000.0f, true));
        StartCoroutine(coroutines[0]);
        StartCoroutine(coroutines[1]);
        StartCoroutine(coroutines[2]);
        StartCoroutine(coroutines[3]);

        yield return StartCoroutine(WaitForSecondsCheck(10000.0f));
    }

    public void DestroySpawnerAttacks() {
        foreach (GameObject spawner in spawners) {
            spawner.GetComponent<Spawner>().DestroyAttacks(0.0f);
        }
    }

    private void ResetSpawners() {
        foreach (GameObject bulletSpawner in spawners) {
            bulletSpawner.GetComponent<Spawner>().DestroyAttacks(0.0f);
            bulletSpawner.GetComponent<Transform>().localPosition = Vector2.up * 200;
            bulletSpawner.GetComponent<Transform>().rotation = Quaternion.identity;
        }
    }

    public void MoveSpawnersEnemyCenter() {
		foreach (GameObject spawner in spawners) {
			spawner.GetComponent<Transform>().position = enemy.transform.position;
		}
	}

    public IEnumerator MoveSpawnersEnemyCenterSlow() {
        CoroutinesClear();
        for (int i = 0; i < spawners.Count; i++) {
            coroutines.Add(spawners[i].GetComponent<Spawner>().MoveTo(enemy.transform.position, 5.0f, true, false));
            StartCoroutine(coroutines[i]);
        }

        while (CoroutinesUnfinished(coroutines)) {
            yield return null;
        }
    }

    public void CoroutinesClear() {
        foreach (IEnumerator coroutine in coroutines) {
            StopCoroutine(coroutine);
        }
        coroutines.Clear();
    }

    public IEnumerator WaitForSecondsCheck(float seconds) {
        float durationEnd = Time.time + seconds;
        while (Time.time < durationEnd) {
            if (!enemyController.IsAlive()) yield break;
            yield return null;
        }
    }

    public IEnumerator WaitForCoroutines(List<IEnumerator> coroutines) {
        while (CoroutinesUnfinished(coroutines)) {
            if (!enemyController.IsAlive()) yield break;
            yield return null;
        }
    }

    public bool CoroutinesUnfinished(List<IEnumerator> coroutines) {
        bool unfinished = false;
        foreach (IEnumerator coroutine in coroutines) {
            if (coroutine.MoveNext()) unfinished = true;
        }
        return unfinished;
    }
}
