using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LowerLightOverTime : MonoBehaviour {

	// Use this for initialization
	void Start () {
        StartCoroutine(LowerLight());	
	}

    IEnumerator LowerLight() {

        float origAmbientIntensity = RenderSettings.ambientIntensity;
        float origLightIntensity = RenderSettings.sun.intensity;

        for (float t = 60; t >= 0; t -= Time.deltaTime) {
            float p = .2f + .8f * (t / 60);
            RenderSettings.ambientIntensity = origAmbientIntensity * p;
            RenderSettings.sun.intensity = origLightIntensity * p;
            yield return null;
        }
    }
}
