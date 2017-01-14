using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gun : MonoBehaviour {

	public enum modes {
		pistol,
		shotgun,
		lazer
	}

	Animator anim;
	public Transform muzzleOpening;
	public float bulletForce;

	public GameObject bullet;
	public GameObject[] bullets;
	public int pooledBullets;
	List<GameObject> bulletList;



	void Awake() {
		anim = GetComponent<Animator>();

		PoolBullets();
	}

	void Update() {
		if (Input.GetButton("Fire1")) {
			anim.SetTrigger("fire");
			GrabBullet();
		}
	}

	void PoolBullets() {
		bulletList = new List<GameObject>();
		for (int i = 0; i < pooledBullets; ++i) {
			GameObject pomf = Instantiate(bullets[0]) as GameObject;
			GameObject desu = Instantiate(bullets[1]) as GameObject;
			GameObject loli = Instantiate(bullets[2]) as GameObject;
			GameObject nani = Instantiate(bullets[3]) as GameObject;
			GameObject onii = Instantiate(bullets[4]) as GameObject;
			GameObject chan = Instantiate(bullets[5]) as GameObject;

			pomf.SetActive(false);
			desu.SetActive(false);
			loli.SetActive(false);
			nani.SetActive(false);
			onii.SetActive(false);
			chan.SetActive(false);

			bulletList.Add(pomf);
			bulletList.Add(desu);
			bulletList.Add(loli);
			bulletList.Add(nani);
			bulletList.Add(onii);
			bulletList.Add(chan);
		}
	}

	void GrabBullet() {
		for (int i = 0; i < bulletList.Count; ++i) {
			if (bulletList[i] != null && !bulletList[i].activeInHierarchy) {
				bulletList[i].transform.position = muzzleOpening.position;
				bulletList[i].transform.rotation = muzzleOpening.rotation;
				bulletList[i].GetComponent<deactivator>().timeTillDeactivate = 3;
				bulletList[i].SetActive(true);

				//fire
				Rigidbody fam = bulletList[i].GetComponent<Rigidbody>();
				fam.AddForce(-muzzleOpening.forward * bulletForce);


				break;
			}
		}
	}
}
