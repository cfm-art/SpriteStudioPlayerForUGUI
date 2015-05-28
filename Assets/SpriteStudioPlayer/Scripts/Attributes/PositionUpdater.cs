using UnityEngine;
using System.Collections;

namespace a.spritestudio.attribute
{
    /// <summary>
    /// 座標の変更
    /// </summary>
    public class PositionUpdater
        : AttributeBase
    {
        public const int kTargetX = 0;
        public const int kTargetY = 1;
        public const int kTargetZ = 2;

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
        public static PositionUpdater Create( int target, float value )
        {
            var self = ScriptableObject.CreateInstance<PositionUpdater>();
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
            var position = part.transform.localPosition;
            position[target_] = value_;
            part.transform.localPosition = position;
        }
    }
}
