using UnityEngine;
using System.Collections;
using MyLive2D;

public class CharactorController : Photon.MonoBehaviour {
	
	public GameObject Charactor;
	private SimpleModel MySimpleModel;
	private SimpleAnimation MySimpleAnimation;
	public string CurrentMotion;
	public bool isMotionFinish = false;
	public bool mouthAnimFlg = false;

	// Use this for initialization
	void Start () {
		init();
	}
	
	private void init (){
		MySimpleAnimation = Charactor.GetComponent<SimpleAnimation>();
		MySimpleModel = Charactor.GetComponent<SimpleModel>();
	}
	
	void Update () {
		if (photonView.isMine) updateCharactor();
		MotionChangeMotionFinish();
	}
	
	private void updateCharactor(){
		// モーションの再生
		if (MySimpleAnimation.isMotionFinished()){
			//Debug.Log("CharactorController.CurrentMotion01:" + CurrentMotion);
			CurrentMotion = MySimpleAnimation.GetRandomIdleMotionName();
			MotionChangeMotionFinish();
		}
	}
	
	private void MotionChangeMotionFinish() {
		if(isMotionFinish || MySimpleAnimation.isMotionFinished()){
			/*
			//Debug.Log("CharactorController.CurrentMotion02:" + CurrentMotion);
			MySimpleAnimation.IdleMotionChange(CurrentMotion);
			
			if(photonView.isMine) {
				photonView.RPC("SetParam", PhotonTargets.All, CurrentMotion, true);
			}
			else if(MySimpleAnimation.isMotionFinished()){
				// アニメーションしていない場合、適当なアニメーションを再生
				MySimpleAnimation.IdleMotionChange(MySimpleAnimation.GetRandomIdleMotionName());
			}
			
			isMotionFinish = false;
			*/
			motionChange();
		}
	}
	
	private void motionChange() {
		//Debug.Log("CharactorController.CurrentMotion02:" + CurrentMotion);
			MySimpleAnimation.IdleMotionChange(CurrentMotion);
			
			if(photonView.isMine) {
				photonView.RPC("SetParam", PhotonTargets.All, CurrentMotion, true);
			}
			else if(MySimpleAnimation.isMotionFinished()){
				// アニメーションしていない場合、適当なアニメーションを再生
				MySimpleAnimation.IdleMotionChange(MySimpleAnimation.GetRandomIdleMotionName());
			}
			
			isMotionFinish = false;
	}
	
	[RPC]
	void SetParam(string motionName, bool isFinished)
	{
		Debug.Log("RPC motionname:" + motionName + " isFinished:" + isFinished);
		CurrentMotion = motionName;
		this.isMotionFinish = isFinished;
	}
	
	// 他者が入室した時
	void OnPhotonPlayerConnected(PhotonPlayer player){
		Debug.Log("CharactorController.RPCMotionChange");
		photonView.RPC("RPCMotionChange", PhotonTargets.All, "action05.mtn", true);
	}
	
	[RPC]
	void RPCMotionChange(string motionName, bool isFinished){
		Debug.Log("CharactorController.RPCMotionChange motionName:" + motionName);
		MySimpleAnimation.ActionMotionChange(motionName);
		//this.isMotionFinish = isFinished;
	}
}
