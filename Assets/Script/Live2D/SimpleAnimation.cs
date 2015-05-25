using UnityEngine;
using System.Collections;
using live2d;

namespace MyLive2D
{
	public class SimpleAnimation : MonoBehaviour {
		
		public SimpleModel simpleModel;
        public string idleMotionKind;             // アイドリングモーション
        public string actionMotionKind;           // アクションモーション
        private Matrix4x4 live2DCanvasPos;
        private Live2DMotion motion;                // モーションクラス
        private MotionQueueManager motionManager;     // モーション管理クラス
		
		
		// Use this for initialization
		void Start () {
			if (simpleModel == null) {
				Debug.LogError("simpleModel is null.");
				return;
			}
			else if (idleMotionKind == null || idleMotionKind.Length <= 0) {
				Debug.LogError("idleMotionKind is Empty.");
				return;
			}
			
			// モデル初期化
			simpleModel.init();
            
			// モーション管理クラスのインスタンス作成
            motionManager = new MotionQueueManager();
			
            // モーションのインスタンス作成
            var motionParam = DictionaryUtil<string, MotionParam>.getRandom(simpleModel.MtnFiles[idleMotionKind]);
            motion = Live2DMotion.loadMotion(motionParam.Mtn.bytes);
			
			// モーションの再生
            motionManager.startMotion(motion, false);
    
            // Live2Dモデルの表示位置計算
            float modelWidth = simpleModel.Live2DModel.getCanvasWidth();
            live2DCanvasPos = Matrix4x4.Ortho(0, modelWidth, modelWidth, 0, -50.0f, 50.0f);
		}
		
		// Update is called once per frame
		void Update () {
			// モーション再生が終了した場合（idleモーションからランダムで再生）
            if (motionManager != null && motionManager.isFinished())
            {
                // モーションをロードする
                var motionParam = DictionaryUtil<string, MotionParam>.getRandom(simpleModel.MtnFiles[idleMotionKind]);
                motion = Live2DMotion.loadMotion(motionParam.Mtn.bytes);
                // フェードインの設定
                motion.setFadeIn(motionParam.FadeIn);
                // フェードアウトの設定
                motion.setFadeOut(motionParam.FadeIn);
                // モーション再生
                motionManager.startMotion(motion, false);
            }
		}
		
		/// <summary>
        /// カメラシーンにレンダリング時呼ばれる
        /// </summary>
        void OnRenderObject()
        {
            if (simpleModel.Live2DModel == null) return;
            simpleModel.Live2DModel.setMatrix(transform.localToWorldMatrix * live2DCanvasPos);
            // アプリが終了していた場合
            if (!Application.isPlaying)
            {
                simpleModel.Live2DModel.update();
                simpleModel.Live2DModel.draw();
                return;
            }
    
            // 再生中のモーションからモデルパラメータを更新
            motionManager.updateParam(simpleModel.Live2DModel);
            // 頂点の更新
            simpleModel.Live2DModel.update();
            // モデルの描画
            simpleModel.Live2DModel.draw();
        }
        
        public void ActionChangeRandom(){
            var motionParam = DictionaryUtil<string, MotionParam>.getRandom(simpleModel.MtnFiles[actionMotionKind]);
            Motion_change(motionParam.Mtn.name);
            Debug.Log("actionMotionKind:" + actionMotionKind + "  name:" + motionParam.Mtn.name);
        }
		
		/// <summary>
        /// モーションチェンジ
        /// </summary>
        /// <param name="filenm"></param>
        void Motion_change(string filenm) 
        {
            Debug.Log("0");
            // 指定ファイルがない場合はreturn
            if (!simpleModel.MtnFiles.ContainsKey(actionMotionKind, filenm)) return;
            Debug.Log("1");
            // モーションのロードをする
            motion = Live2DMotion.loadMotion(simpleModel.MtnFiles[actionMotionKind][filenm].Mtn.bytes);
            Debug.Log("2");
            // フェードインの設定
            motion.setFadeIn(simpleModel.MtnFiles[actionMotionKind][filenm].FadeIn);
            Debug.Log("3");
            // フェードアウトの設定
            motion.setFadeOut(simpleModel.MtnFiles[actionMotionKind][filenm].FadeOut);
            Debug.Log("4");
            // モーション再生
            motionManager.startMotion(motion, false);
            Debug.Log("5");
            // 音声再生
            if (simpleModel.MtnFiles[actionMotionKind][filenm].Sound != null) {
                Debug.Log("6");
                GetComponent<AudioSource>().clip = simpleModel.MtnFiles[actionMotionKind][filenm].Sound;
                GetComponent<AudioSource>().Play();
            } 
        }
	}
}