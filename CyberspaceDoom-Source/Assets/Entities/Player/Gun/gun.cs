using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour {

    Animator anim;
    public Transform muzzleOpening;
    public Transform chargeBar;

    public GameObject bulletPrefab;

    public float bulletSpeed = 5f;

    public float pistolReloadTime = 2f;
    bool canShootPistol = true;

    public float shotgunReloadTime = 2f;
    public float shotgunSpread = .1f;
    public float shotgunRecoil = 20;
    bool canShootShotgun = true;

    void Awake() {
        anim = GetComponent<Animator>();
        Cursor.lockState = CursorLockMode.Locked;
    }


    void Update() {
        if (Input.GetMouseButton(0) && canShootPistol) {
            ShootBullet(false);
            StartCoroutine(ReloadPistol());

        }
        if (Input.GetMouseButtonDown(1) && canShootShotgun) {
            for (int i = 0; i < 20; i++) {
                ShootBullet(true);
            }
            transform.root.GetComponent<Rigidbody>().velocity += (-muzzleOpening.forward * shotgunRecoil);
            StartCoroutine(ReloadShotgun());
        }
    }

    void ShootBullet(bool useRecoil) {
        anim.SetTrigger("fire");
        GameObject bullet = GameObject.Instantiate(bulletPrefab);
        bullet.transform.position = muzzleOpening.position;
        bullet.transform.rotation = muzzleOpening.rotation;
        Rigidbody bulletRB = bullet.GetComponent<Rigidbody>();
        Vector3 direction = muzzleOpening.forward;
        if (useRecoil)
            direction = (direction + Random.insideUnitSphere * shotgunSpread).normalized;
        bulletRB.velocity = direction * bulletSpeed;
    }

    IEnumerator ReloadPistol() {
        canShootPistol = false;
        yield return new WaitForSeconds(pistolReloadTime);
        canShootPistol = true;
    }

    IEnumerator ReloadShotgun() {
        canShootShotgun = false;
        for (float t = 0; t < shotgunReloadTime; t += Time.deltaTime) {
            chargeBar.localScale = new Vector3(1, 1, t / shotgunReloadTime);
            yield return null;
        }
        chargeBar.localScale = Vector3.one;
        canShootShotgun = true;

    }
}