using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CharactorManager : MonoBehaviour {

	public GameObject[] charactorModels;
	//private List<GameObject> charactors;
	private GameObject myCharactor;
	
	void Start(){
	}
	
	public void createCharactor(int charNum){
		if (charactorModels.Length <= charNum) return;
		if (charactorModels[charNum] == null) return;
		
		if (myCharactor != null) Destroy(myCharactor);
		myCharactor = Instantiate(charactorModels[charNum]);
	}
}
