using UnityEngine;
using System.Collections;

public class SetController : Photon.MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	public void MyDestroy(){
		if(photonView.isMine) PhotonNetwork.Destroy(gameObject);
	}
}
