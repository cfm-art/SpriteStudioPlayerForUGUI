using UnityEngine;
using System.Collections.Generic;

namespace a.spritestudio
{
    /// <summary>
    /// キーフレームの集合
    /// </summary>
    [System.Serializable]
    public class KeyFrames
    {
        /// <summary>
        /// キーフレームたち
        /// </summary>
        [SerializeField]
        private List<KeyFrame> keyFrames_;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="frames"></param>
        internal KeyFrames( IEnumerable<KeyFrame> frames )
        {
            keyFrames_ = new List<KeyFrame>( frames );
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        internal KeyFrame[] ToArray()
        {
            return keyFrames_.ToArray();
        }
    }

    /// <summary>
    /// キーフレームを別で保存して動的にモーションを変更する用
    /// </summary>
    public class KeyFrameResource
        : ScriptableObject
    {
        /// <summary>
        /// パーツ名
        /// </summary>
        [SerializeField]
        private List<string> partsNames_;

        /// <summary>
        /// キーフレーム
        /// </summary>
        [SerializeField]
        private List<KeyFrames> keyFrames_;

        /// <summary>
        /// 総フレーム数
        /// </summary>
        [SerializeField]
        private int totalFrames_;

        /// <summary>
        /// キーフレーム生成
        /// </summary>
        /// <param name="totalFrames"></param>
        /// <param name="frames"></param>
        /// <returns></returns>
        public static KeyFrameResource Create( int totalFrames, Dictionary<string, KeyFrame[]> frames )
        {
            var self = ScriptableObject.CreateInstance<KeyFrameResource>();
            self.partsNames_ = new List<string>( frames.Count );
            self.keyFrames_ = new List<KeyFrames>( frames.Count );
            self.totalFrames_ = totalFrames;

            foreach ( var frame in frames ) {
                if ( frame.Value != null ) {
                    self.partsNames_.Add( frame.Key );
                    self.keyFrames_.Add( new KeyFrames( frame.Value ) );
                }
            }
            return self;
        }

        /// <summary>
        /// キーフレーム取得
        /// </summary>
        /// <param name="part"></param>
        /// <returns></returns>
        public KeyFrame[] GetKeyFrames( string part )
        {
            int index = partsNames_.IndexOf( part );
            return keyFrames_[index].ToArray();
        }

        /// <summary>
        /// 総フレーム数取得
        /// </summary>
        public int TotalFrames
        {
            get { return totalFrames_; }
        }

        /// <summary>
        /// 内容をコピーする
        /// </summary>
        /// <param name="other"></param>
        public void CopyTo( KeyFrameResource other )
        {
            other.totalFrames_ = totalFrames_;
            other.keyFrames_ = keyFrames_;
            other.partsNames_ = partsNames_;
        }
    }
}
