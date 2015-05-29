using UnityEngine;
using System.Collections;

namespace a.spritestudio.attribute
{
    /// <summary>
    /// 反転状態の更新
    /// </summary>
    public class Flipper
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="isVisible"></param>
        public static AttributeBase Create( bool isFlip, bool isHorizontal )
        {
            var self = new AttributeBase(
                    isHorizontal ? AttributeBase.Target.kFliperH : AttributeBase.Target.kFliperV,
                    null, null,
                    new bool[] { isFlip } );
            return self;
        }

        /// <summary>
        /// 処理
        /// 横反転
        /// </summary>
        /// <param name="part"></param>
        public static void OnUpdateH( SpritePart part, AttributeBase attribute )
        {
            bool isFlip = attribute.@bool( 0 );
            var scale = part.transform.localScale;
            if ( isFlip ) {
                scale.x = -Mathf.Abs( scale.x );
            } else {
                scale.x = Mathf.Abs( scale.x );
            }
            part.transform.localScale = scale;
        }

        /// <summary>
        /// 処理
        /// 縦反転
        /// </summary>
        /// <param name="part"></param>
        /// <param name="attribute"></param>
        public static void OnUpdateV( SpritePart part, AttributeBase attribute )
        {
            bool isFlip = attribute.@bool( 0 );
            var scale = part.transform.localScale;
            if ( isFlip ) {
                scale.y = -Mathf.Abs( scale.y );
            } else {
                scale.y = Mathf.Abs( scale.y );
            }
            part.transform.localScale = scale;
        }
    }
}
