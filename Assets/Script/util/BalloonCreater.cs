using UnityEngine;
using System.Collections;

public class BalloonCreater : Photon.MonoBehaviour {
	
	BalloonController MyBalloonController;

	// Use this for initialization
	void Start () {
		MyBalloonController = transform.FindChild("BalloonController").GetComponent<BalloonController>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	public void CreateBalloon(string message){
		if(photonView.isMine) photonView.RPC("CreateBalloonRPC", PhotonTargets.All, message);
	}
	
	[RPC]
	public void CreateBalloonRPC(string message){
		MyBalloonController.createBalloon(message);
	}
}
