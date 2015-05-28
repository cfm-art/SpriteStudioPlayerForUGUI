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
        [SerializeField]
        private int value_;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        public static PriorityUpdater Create( int value )
        {
            var self = ScriptableObject.CreateInstance<PriorityUpdater>();
            self.value_ = value;
            return self;
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
