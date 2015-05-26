using UnityEngine;
using System.Collections;

namespace a.spritestudio.attribute
{
    /// <summary>
    /// 優先度の設定
    /// </summary>
    public class PriorityUpdater
        : AttributeBase
    {
        /// <summary>
        /// 値
        /// </summary>
        private int value_;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="target"></param>
        /// <param name="value"></param>
        public PriorityUpdater( int value )
        {
            value_ = value;
        }

        /// <summary>
        /// 処理
        /// </summary>
        /// <param name="part"></param>
        protected override void OnUpdate( SpritePart part )
        {
            part.Priority = value_;
        }
    }
}
