using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.N3DS;
public class WinManager : MonoBehaviour {
	public GameObject FiveAM;
    public GameObject SixAM;
	public AudioSource Applause;

	bool triggered;
    // Use this for initialization
    void Start () {
		
	}

	// Update is called once per frame
	void Update () {
		if (SixAM.transform.localPosition.y < -0.1231232f)
		{
            SixAM.transform.localPosition += new Vector3(0f, 0.14f, 0f);
            FiveAM.transform.localPosition += new Vector3(0f, 0.14f, 0f);

        }
		else if (triggered == false)
		{
			triggered = true;
			Applause.Play();
		}

    }
}
