using UnityEngine;
using System;
using System.Collections;
using live2d;
using live2d.framework;
using MyLive2D;

public class MouseAnimation : Photon.MonoBehaviour {
	
	public SimpleModel simpleModel;
    private L2DTargetPoint dragMgr = new L2DTargetPoint();
    //private bool mouseFlg = false;
    
    private Vector2 mouthPos;
	
	void Start () {
        mouthPos = Vector2.zero;
	}
	
	// Update is called once per frame
	void Update () {
        if (photonView.isMine) {
    		var pos = Input.mousePosition;
            if (Input.GetMouseButtonDown(0))
            {
                if (InputController.GetGameObjectByMousePoint() == gameObject) changeMotion();
            }
            else if (Input.GetMouseButton(0))
            {
                mouthPos = new Vector2(pos.x / Screen.width*2-1, pos.y/Screen.height*2-1);
            }
            else if (Input.GetMouseButtonUp(0))
            {
                mouthPos = Vector2.zero;
            }
        }
        SetMouthPos();
	}
	
	//void OnRenderObject()
    
	public void updateMotion()
	{
        if (simpleModel.Live2DModel == null)
        {
            Debug.LogError("live2DModel is null");
			return;
        }

        if ( ! Application.isPlaying)
        {
            simpleModel.Live2DModel.update();
            simpleModel.Live2DModel.draw();
            return;
        }

        dragMgr.update();
        simpleModel.Live2DModel.setParamFloat("PARAM_ANGLE_X" , 
          simpleModel.Live2DModel.getParamFloat("PARAM_ANGLE_X") + dragMgr.getX()*30 ) ;
        simpleModel.Live2DModel.setParamFloat("PARAM_ANGLE_Y", 
          simpleModel.Live2DModel.getParamFloat("PARAM_ANGLE_Y") + dragMgr.getY()*30);
        simpleModel.Live2DModel.setParamFloat("PARAM_ANGLE_Z", 
          simpleModel.Live2DModel.getParamFloat("PARAM_ANGLE_Z") + ((dragMgr.getX() * dragMgr.getY())*20*-1) );
          
        if (simpleModel.Live2DModel.getParamFloat("PARAM_ANGLE_Z") > 20){
           
            Debug.Log("PARAM_ANGLE_Z" + simpleModel.Live2DModel.getParamFloat("PARAM_ANGLE_Z") +
            "  dragMgr.getX():" + dragMgr.getX() + "  dragMgr.getY():" + dragMgr.getY()); 
        }  

        simpleModel.Live2DModel.setParamFloat("PARAM_EYE_BALL_X", dragMgr.getX());
        simpleModel.Live2DModel.setParamFloat("PARAM_EYE_BALL_Y", dragMgr.getY());

		simpleModel.Live2DModel.update();
		//simpleModel.Live2DModel.draw();
	}
    
    private void SetMouthPos(){
        dragMgr.Set(mouthPos.x, mouthPos.y);
    }
    
    void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info) {
        if (stream.isWriting) {
            //データの送信
            stream.SendNext(mouthPos);
        } else {
            //データの受信
            mouthPos = (Vector2)stream.ReceiveNext();
        }
    }
    
    private void changeMotion() {
        
    }
}
