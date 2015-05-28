using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CharactorManager : Photon.MonoBehaviour {

	public GameObject[] charactorModels;
	public string[] charactorModelsPath;
	//private List<GameObject> charactors;
	private GameObject myCharactor;
	
	void Start(){
	}
	
	public void createCharactor(int charNum){
		if (charactorModels.Length <= charNum) return;
		if (charactorModels[charNum] == null) return;
		
		if (myCharactor != null) Destroy(myCharactor);
		//myCharactor = Instantiate(charactorModels[charNum]);
		myCharactor = PhotonNetwork.Instantiate(charactorModelsPath[charNum], Vector3.zero, Quaternion.identity, 0);
	}
}
