using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Light))]
public class TorchFlicker : MonoBehaviour {

    public Light torchLight;

    public AnimationCurve flickerBrightnessProbabilityCurve;
    public float minIntensity;
    public float intensityDecayRate;

	// Use this for initialization
	void Start () {
        torchLight = GetComponent<Light>();
	}
	
	// Update is called once per frame
	void Update () {
	    if(DecayLight())
        {
            JumpBrightness();
        }
	}

    bool DecayLight()
    {
        torchLight.intensity -= intensityDecayRate*Time.deltaTime;
        if(torchLight.intensity <= minIntensity + 0.05)
        {
            return true;
        }
        return false;
    }

    void JumpBrightness()
    {
        torchLight.intensity = flickerBrightnessProbabilityCurve.Evaluate(Random.value);
    }
}
