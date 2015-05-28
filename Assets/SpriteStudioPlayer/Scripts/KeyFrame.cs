using UnityEngine;
using System.Collections.Generic;

namespace a.spritestudio
{
    /// <summary>
    /// キーフレーム
    /// </summary>
    [System.Serializable]
    public class KeyFrame
    {
        /// <summary>
        /// 
        /// </summary>
        private List<attribute.AttributeBase> attributes_;

        /// <summary>
        /// 
        /// </summary>
        public KeyFrame()
        {
            attributes_ = new List<attribute.AttributeBase>();
        }

        /// <summary>
        /// 追加
        /// </summary>
        /// <param name="attribute"></param>
        public void Add( attribute.AttributeBase attribute )
        {
            attributes_.Add( attribute );
        }

        /// <summary>
        /// for-each用
        /// </summary>
        /// <returns></returns>
        public IEnumerator<attribute.AttributeBase> GetEnumerator()
        {
            return attributes_.GetEnumerator();
        }
    }
}
