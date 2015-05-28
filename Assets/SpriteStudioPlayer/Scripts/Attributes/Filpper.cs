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
            var self = new AttributeBase( AttributeBase.Target.kFliper, null, null,
                    new bool[] { isFlip, isHorizontal } );
            return self;
        }

        /// <summary>
        /// 処理
        /// </summary>
        /// <param name="part"></param>
        public static void OnUpdate( SpritePart part, AttributeBase attribute )
        {
            bool isFlip = attribute.@bool( 0 );
            bool isHorizontal = attribute.@bool( 1 );
            var scale = part.transform.localScale;
            if ( isHorizontal ) {
                if ( isFlip ) {
                    scale.x = -Mathf.Abs( scale.x );
                } else {
                    scale.x = Mathf.Abs( scale.x );
                }
            } else {
                if ( isFlip ) {
                    scale.y = -Mathf.Abs( scale.y );
                } else {
                    scale.y = Mathf.Abs( scale.y );
                }
            }
            part.transform.localScale = scale;
        }
    }
}
