using UnityEngine;
using System;
using System.Collections;
using live2d;
using live2d.framework;
using MyLive2D;

public class MouseAnimation : MonoBehaviour {

	//public TextAsset mocFile;
    //public TextAsset physicsFile;
    //public Texture2D[] textureFiles;
	
	public SimpleModel simpleModel;
    //private EyeBlinkMotion eyeBlink = new EyeBlinkMotion();
    private L2DTargetPoint dragMgr = new L2DTargetPoint();
    private bool mouseFlg = false;
	
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		var pos = Input.mousePosition;
        if (Input.GetMouseButtonDown(0))
        {
            //
        }
        else if (Input.GetMouseButton(0))
        {
            dragMgr.Set(pos.x / Screen.width*2-1, pos.y/Screen.height*2-1);
            mouseFlg = true;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            dragMgr.Set(0, 0);
            mouseFlg = false;
        }
	}
	
	void OnRenderObject()
	{
        if (simpleModel.Live2DModel == null)
        {
            Debug.LogError("live2DModel is null");
			return;
        }

        //live2DModel.setMatrix(transform.localToWorldMatrix * live2DCanvasPos);


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
    

/*
        double timeSec = UtSystem.getUserTimeMSec() / 1000.0;
        double t = timeSec * 2 * Math.PI;
        live2DModel.setParamFloat("PARAM_BREATH", (float)(0.5f + 0.5f * Math.Sin(t / 3.0)));
*/
        //eyeBlink.setParam(live2DModel);

        //if (physics != null) physics.updateParam(live2DModel);

		simpleModel.Live2DModel.update();
		simpleModel.Live2DModel.draw();
	}
}
