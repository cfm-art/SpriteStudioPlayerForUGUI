using UnityEngine;
using System.Collections;

namespace a.spritestudio.attribute
{
    /// <summary>
    /// 拡縮の変更
    /// </summary>
    public class ScalingUpdater
    {
        public const int kTargetX = 0;
        public const int kTargetY = 1;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="target"></param>
        /// <param name="value"></param>
        public static AttributeBase Create( int target, float value )
        {
            var self = new AttributeBase( 
                target == kTargetX
                    ? AttributeBase.Target.kScalingX
                    : AttributeBase.Target.kScalingY,
               null, new float[] { value }, null );
            return self;
        }

        /// <summary>
        /// 処理
        /// </summary>
        /// <param name="part"></param>
        public static void OnUpdateX( SpritePart part, AttributeBase attribute )
        {
            float value = attribute.@float( 0 );
            var scaling = part.transform.localScale;
            float old = scaling.x;
            float v = old < 0 ? -value : value;
            scaling.x = v;
            //scaling.z = v;
            part.transform.localScale = scaling;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="part"></param>
        /// <param name="attribute"></param>
        public static void OnUpdateY( SpritePart part, AttributeBase attribute )
        {
            float value = attribute.@float( 0 );
            var scaling = part.transform.localScale;
            float old = scaling.y;
            float v = old < 0 ? -value : value;
            scaling.y = v;
            //scaling.z = v;
            part.transform.localScale = scaling;
        }
    }
}
