using UnityEngine;
using System.Collections;

namespace a.spritestudio.attribute
{
    /// <summary>
    /// 拡縮の変更
    /// </summary>
    public class ScalingUpdater
        : AttributeBase
    {
        public const int kTargetX = 0;
        public const int kTargetY = 1;

        /// <summary>
        /// 対象
        /// </summary>
        [SerializeField]
        private int target_;

        /// <summary>
        /// 値
        /// </summary>
        [SerializeField]
        private float value_;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="target"></param>
        /// <param name="value"></param>
        public static ScalingUpdater Create( int target, float value )
        {
            var self = ScriptableObject.CreateInstance<ScalingUpdater>();
            self.target_ = target;
            self.value_ = value;
            return self;
        }

        /// <summary>
        /// 処理
        /// </summary>
        /// <param name="part"></param>
        protected override void OnUpdate( SpritePart part )
        {
            var scaling = part.transform.localScale;
            float old = scaling[target_];
            float v = old < 0 ? -value_ : value_;
            scaling[target_] = v;
            //scaling.z = v;
            part.transform.localScale = scaling;
        }
    }
}
