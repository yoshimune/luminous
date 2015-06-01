using UnityEngine;
using System.Collections;
using MyLive2D;

public class MouthAnimation : Photon.MonoBehaviour {
	
	
	public SimpleModel simpleModel;

    //口パク、モーション中にムリヤリブレンドするため配列で用意
	// JSONで用意するべきかもしれない…
    private int mouseFormNum;
	private int mouseOpenNum;
    private readonly float[] PARAM_MOUTH_FORM = {0,0.09f,0.27f,0.36f,0.19f,-0.1f,-0.27f,-0.19f,-0.06f,0.07f,0.16f,0.19f,0.06f,-0.15f,-0.29f,-0.34f,-0.336f,-0.31f,-0.27f,-0.2f,0.01f,0.27f,0.4f,0.29f,0.11f,0};
    private readonly float[] PARAM_MOUTH_OPEN_Y = {0,0.24f,0.64f,0.91f,1,0.82f,0.48f,0.3f,0.39f,0.54f,0.64f,0.67f,0.62f,0.5f,0.33f,0.17f,0.05f,0,0.17f,0.47f,0.64f,0.53f,0.34f,0.17f,0.05f,0};
	

	// Use this for initialization
	void Start () {
		mouseFormNum = PARAM_MOUTH_FORM.Length;
		mouseOpenNum = PARAM_MOUTH_OPEN_Y.Length;
	}
	
	public void startMouth() {
		if(photonView.isMine) photonView.RPC("startMouthRPC", PhotonTargets.All);
	}
	[RPC]
	public void startMouthRPC() {
		mouseFormNum = 0;
		mouseOpenNum = 0;
	}
	
	public void updateMotion() {
		if (mouseFormNum < PARAM_MOUTH_FORM.Length){
			simpleModel.Live2DModel.setParamFloat("PARAM_MOUTH_FORM", PARAM_MOUTH_FORM[mouseFormNum]);
			mouseFormNum++;
		}
		
		if (mouseOpenNum < PARAM_MOUTH_OPEN_Y.Length){
			simpleModel.Live2DModel.setParamFloat("PARAM_MOUTH_OPEN_Y", PARAM_MOUTH_OPEN_Y[mouseOpenNum]);
			mouseOpenNum++;
		}
		simpleModel.Live2DModel.update();
	}
}
