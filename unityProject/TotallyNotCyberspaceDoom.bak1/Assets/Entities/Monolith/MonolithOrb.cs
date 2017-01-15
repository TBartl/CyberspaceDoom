using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonolithOrb : MonoBehaviour {
	public float returnSpeed = .5f;
	Vector3 originalPosition;

	// Use this for initialization
	void Start () {
		originalPosition = transform.position;
		
	}
	
	// Update is called once per frame
	void Update () {
		this.transform.position += (originalPosition - transform.position).normalized * returnSpeed * Time.deltaTime;
		
	}
}
