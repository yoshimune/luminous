using UnityEngine;
using System.Collections;
using System.Collections.Generic;


// ManagerはPhotonで同期させる
// キャラの動きは全部管理する
public class CharactorManager : Photon.MonoBehaviour {

	public GameObject[] charactorModels;
	public string[] charactorModelsPath;
	private GameObject myCharactor;
	
	private List<GameObject> MyCharactors;
	
	void Start(){
	}
	
	public void createCharactor(){
		createCharactor(MyCharactors.Count);
	}
	
	public void createCharactor(int charNum){
		if (charactorModels.Length <= charNum) return;
		if (charactorModels[charNum] == null) return;
		
		
		if (myCharactor != null) {
			photonView.RPC("Destroy", PhotonTargets.All);
		}
		var charactor = PhotonNetwork.Instantiate(
			charactorModelsPath[charNum],
			new Vector3(0, -0.5f, 0),
			Quaternion.identity,
			0);
		if(photonView.isMine) myCharactor = charactor;
	}
	
	[RPC]
	void Destroy()
	{
		if(photonView.isMine) PhotonNetwork.Destroy(myCharactor);
	}
	
	public void MouthAnimationStart(){
		foreach(GameObject target in GameObject.FindGameObjectsWithTag("Charactor")){
			var mouthAnimation = target.transform.GetComponent<MouthAnimation>();
			if (mouthAnimation == null) continue;
			mouthAnimation.startMouth();
		}
	}
}
