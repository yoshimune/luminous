using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BalloonManager : MonoBehaviour {
	
	public List<GameObject> balloons;

	// Use this for initialization
	void Start () {
		balloons = new List<GameObject>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	public void SetBalloon(GameObject balloon) {
		this.balloons.Add(balloon);
	}
}
