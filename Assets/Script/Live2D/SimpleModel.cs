using UnityEngine;
using System;
using System.Collections;
using System.Text.RegularExpressions;
using live2d;
using System.Collections.Generic;


namespace MyLive2D
{
    [ExecuteInEditMode]
    public class SimpleModel : MonoBehaviour
    {
        public string path;                       // モデルパス
        public TextAsset modelJson;                 // model.json
        private string modelpath;                  // モデルパス
        private TextAsset mocFile;                  // モデルファイル
        private Texture2D[] textures;               // テクスチャファイル
        private MultiDictionary<string, string, MotionParam> mtnFiles;
        public MultiDictionary<string, string, MotionParam> MtnFiles
        {
            get { return this.mtnFiles; }
        }
        private Live2DModelUnity live2DModel;
        public Live2DModelUnity Live2DModel
        {
            get { return this.live2DModel; }
        }
    
        /// <summary>
        /// 初期処理
        /// </summary>
        public void init () 
        {
            // Live2D初期化
            Live2D.init();
            // model.jsonを読み込む
            Json_Read();
            
            // デバッグ
            foreach (var item in live2DModel.getModelImpl().getParamDefSet().getParamDefFloatList()) {
                // モデルのパラメータIDを取得
                Debug.Log(item.getParamID().ToString());
            }
        }
    
    
        /// <summary>
        /// model.jsonを読み込む
        /// </summary>
        void Json_Read()
        {
            // モーション情報を初期化
            mtnFiles = new MultiDictionary<string, string, MotionParam>();
            
            // model.jsonを読み込む
            char[] buf = modelJson.text.ToCharArray();
            Value json = Json.parseFromBytes(buf);
    
            modelpath = path + "/";
    
            // モデルを読み込む
            mocFile = new TextAsset();
            mocFile = (Resources.Load(modelpath + json.get("model").toString(), typeof(TextAsset)) as TextAsset);
            live2DModel = Live2DModelUnity.loadModel(mocFile.bytes);
    
            // テクスチャを読み込む
            int texture_num = json.get("textures").getVector(null).Count;
            textures = new Texture2D[texture_num];
    
            for (int i = 0; i < texture_num; i++)
            {
                // 不要な拡張子を削除
                string texturenm = Regex.Replace(modelpath + json.get("textures").get(i).toString(), ".png$", "");
                textures[i] = (Resources.Load(texturenm, typeof(Texture2D)) as Texture2D);
                live2DModel.setTexture(i, textures[i]);
            }
            // モーションとサウンドを読み込む(motions配下のタグを読み込む)
            ReadMotion(json);
        }
        
        /// <summary> 
        /// motions配下のタグを読み込む
        /// <summary>
        private void ReadMotion(Value json)
        {
            // "motions"以下のmotion情報を読み込み
            foreach( string kind in json.get("motions").getMap(null).Keys )
            {
                Debug.Log("kind:" + kind);
                
                Value mtnpath = json.get("motions").get(kind);
                int mtn_num = mtnpath.getVector(null).Count;
                for (int n = 0; n < mtn_num; n++)
                {   
                    var mtn = Resources.Load(modelpath + mtnpath.get(n).get("file").toString()) as TextAsset;
                    AudioClip sound = null;
                    int fadeIn = 0;
                    int fadeOut = 0;
                    
                    // サウンドファイルがあれば入れる            
                    if (mtnpath.get(n).getMap(null).ContainsKey("sound"))
                    {
                        // 不要な拡張子を削除
                        string soundnm = Regex.Replace(Regex.Replace(modelpath + mtnpath.get(n).get("sound").toString(), ".mp3$", ""), ".wav$", "");
                        sound = (Resources.Load(soundnm, typeof(AudioClip)) as AudioClip);
                    }
                    //フェードイン
                    if (mtnpath.get(n).getMap(null).ContainsKey("fade_in"))
                    {
                        fadeIn = int.Parse(mtnpath.get(n).get("fade_in").toString());
                    }
                    //フェードアウト
                    if (mtnpath.get(n).getMap(null).ContainsKey("fade_out"))
                    {
                        fadeOut = int.Parse(mtnpath.get(n).get("fade_out").toString());
                    }
                    Debug.Log("mtn name:" + mtn.name);
                    mtnFiles.Add(kind, mtn.name, new MotionParam(mtn, sound, fadeIn, fadeOut));
                }
            }
        }
    }
}