using UnityEngine;
using System.Collections;
using System.Collections.Generic;


// ManagerはPhotonで同期させる
// キャラの動きは全部管理する
public class CharactorManager : Photon.MonoBehaviour {

	public GameObject[] charactorModels;
	public string[] charactorModelsPath;
	private GameObject myCharactor;
	private int charNum;
	//private Vector3 myCharactorPos;
	
	private List<GameObject> MyCharactors;
	
	void Start(){
		charNum = 0;
		//myCharactorPos = Vector3.zero;
	}
	
	public void createCharactor(){
		createCharactor(MyCharactors.Count);
	}
	
	public void createCharactor(int charNum){
		//if (charactorModels.Length <= charNum) return;
		//if (charactorModels[charNum] == null) return;
		
		this.charNum = charNum;
		 Vector3 pos = new Vector3(0, -0.5f, 0);
		
		if (myCharactor != null) {
			pos = myCharactor.transform.position;
			//photonView.RPC("Destroy", PhotonTargets.All);
			Destroy();
		}
		var charactor = PhotonNetwork.Instantiate(
			charactorModelsPath[charNum],
			pos,
			Quaternion.identity,
			0);
		//if(photonView.isMine) myCharactor = charactor;
		myCharactor = charactor;
	}
	
	[RPC]
	void Destroy()
	{
		//if(photonView.isMine) PhotonNetwork.Destroy(myCharactor);
		//PhotonNetwork.Destroy(myCharactor);
		SetController myController = myCharactor.GetComponent<SetController>();
		if (myController != null) myController.MyDestroy();
	}
	
	public void MouthAnimationStart(){
		foreach(GameObject target in GameObject.FindGameObjectsWithTag("Charactor")){
			var mouthAnimation = target.transform.GetComponent<MouthAnimation>();
			if (mouthAnimation == null) continue;
			mouthAnimation.startMouth();
		}
	}
	
	public void changeCharactor(){
		charNum += 1;
		Debug.Log("this.charNum:" + this.charNum);
		if (charactorModelsPath.Length <= charNum) {
			charNum = 0;
		}
		Debug.Log("this.charNum:" + this.charNum);
		createCharactor(charNum);
	}
}
