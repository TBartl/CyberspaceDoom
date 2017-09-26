using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class GravityAttractor : MonoBehaviour {
    public float gravity = 9f;
    Rigidbody rigid;

    void Awake() {
        rigid = this.GetComponent<Rigidbody>();
        rigid.useGravity = false;
        rigid.constraints = RigidbodyConstraints.FreezeRotation;
    }

    void FixedUpdate() {
        Vector3 currentUp = transform.position.normalized;
        Vector3 targetUp = transform.up.normalized;

        transform.rotation = Quaternion.FromToRotation(targetUp, currentUp) * transform.rotation;
        rigid.AddForce(-transform.position.normalized * gravity);
    }
}


/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof(Rigidbody))]
public class GravityAttractor : MonoBehaviour {


	public void GravPull(Transform body, float grav) {
		Vector3 targetDir = (body.position - transform.position).normalized;
		Vector3 bodyUp = body.up;

		Rigidbody rigi = body.GetComponent<Rigidbody>();
		rigi.AddForce(bodyUp * grav);
		body.rotation = Quaternion.FromToRotation(bodyUp, targetDir) * body.rotation;
		rigi.AddForce(targetDir * grav);
	}
}*/
