using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Light))]
public class TorchOscillate : MonoBehaviour {

    public AnimationCurve intensityCurve;
    public float intensityCurveDuration;

    private float intensityTime;
    private Light torchLight;

	// Use this for initialization
	void Start () {
        torchLight = GetComponent<Light>();
	}
	
	// Update is called once per frame
	void Update () {
        intensityTime += Time.deltaTime / intensityCurveDuration;
        torchLight.intensity = intensityCurve.Evaluate(intensityTime);
	}
}
