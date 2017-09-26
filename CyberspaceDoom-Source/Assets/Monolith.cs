using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monolith : MonoBehaviour {

	bool monolithActivated = false;
	MeshRenderer mr;
	public float spawnRate = 14;
	public float increasedSpawnRate = 4;
    public int startSpawn = 2;
	bool useIncreasedSpawn = false;
	public List<GameObject> enemies;

	void Awake() {
		mr = this.GetComponent<MeshRenderer>();
		StartCoroutine(CountDownToIncreased());
		StartCoroutine(SpawnEnemies());

	}

    void Update() {
        if (!monolithActivated && transform.childCount == 0) {
            mr.material = mr.materials[1];
            Gate.s.MonolithActivated();
            monolithActivated = true;
        }
    }

	//void OnCollisionEnter(Collision coll) {
	//	if (!monolithActivated &&
	//		(coll.collider.tag == "Player" || coll.collider.tag == "Bullet") &&
	//		transform.childCount == 0) {
 //           mr.material = mr.materials[1];
 //           Gate.s.MonolithActivated();
 //           monolithActivated = true;

 //       }
	//}

	IEnumerator SpawnEnemies() {
        for (int i = 0; i < startSpawn; i++) {
            SpawnRandomEnemy();
        }
		while (true) {
            yield return new WaitForSeconds(GetSpawnRate());
            SpawnRandomEnemy();
		}
	}

	void SpawnRandomEnemy() {
        if (enemies.Count == 0)
            return;
		int enemyIndex = Random.Range(0, enemies.Count);
		Vector2 circle = Random.insideUnitCircle * 10f;
		Vector3 position = this.transform.position + transform.TransformVector(Vector3.up * 10f + new Vector3(circle.x, 0, circle.y));
		Instantiate(enemies[enemyIndex], position, Quaternion.identity);
	}

	IEnumerator CountDownToIncreased() {
		yield return new WaitForSeconds(60);
	}

	float GetSpawnRate() {
		if (useIncreasedSpawn)
			return increasedSpawnRate;
		else
			return spawnRate;
	}
}
