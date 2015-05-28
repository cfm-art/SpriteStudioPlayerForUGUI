using UnityEngine;
using System.Collections;

namespace a.spritestudio.attribute
{
    /// <summary>
    /// 回転の変更
    /// </summary>
    public class RotationUpdater
        : AttributeBase
    {
        public const int kTargetX = 0;
        public const int kTargetY = 1;
        public const int kTargetZ = 2;

        /// <summary>
        /// 対象
        /// </summary>
        private int target_;

        /// <summary>
        /// 値
        /// </summary>
        private float value_;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="target"></param>
        /// <param name="value"></param>
        public RotationUpdater( int target, float value )
        {
            target_ = target;
            value_ = value;
        }

        /// <summary>
        /// 処理
        /// </summary>
        /// <param name="part"></param>
        protected override void OnUpdate( SpritePart part )
        {
            var rotation = part.transform.localRotation;
            var angles = rotation.eulerAngles;
            angles[target_] = value_;
            rotation.eulerAngles = angles;
            part.transform.localRotation = rotation;
        }
    }
}
