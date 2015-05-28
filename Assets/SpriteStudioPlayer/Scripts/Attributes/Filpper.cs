using UnityEngine;
using System.Collections;

namespace a.spritestudio.attribute
{
    /// <summary>
    /// 反転状態の更新
    /// </summary>
    public class Flipper
        : AttributeBase
    {
        /// <summary>
        /// 縦か横か
        /// </summary>
        [SerializeField]
        private bool isHorizontal_;

        /// <summary>
        /// 反転
        /// </summary>
        [SerializeField]
        private bool isFlip_;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="isVisible"></param>
        public static Flipper Create( bool isFlip, bool isHorizontal )
        {
            var self = ScriptableObject.CreateInstance<Flipper>();
            self.isFlip_ = isFlip;
            self.isHorizontal_ = isHorizontal;
            return self;
        }

        /// <summary>
        /// 処理
        /// </summary>
        /// <param name="part"></param>
        protected override void OnUpdate( SpritePart part )
        {
            var scale = part.transform.localScale;
            if ( isHorizontal_ ) {
                if ( isFlip_ ) {
                    scale.x = -Mathf.Abs( scale.x );
                } else {
                    scale.x = Mathf.Abs( scale.x );
                }
            } else {
                if ( isFlip_ ) {
                    scale.y = -Mathf.Abs( scale.y );
                } else {
                    scale.y = Mathf.Abs( scale.y );
                }
            }
            part.transform.localScale = scale;
        }
    }
}
