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
            var self = new AttributeBase( AttributeBase.Target.kScaling, new int[] { target }, new float[] { value }, null );
            return self;
        }

        /// <summary>
        /// 処理
        /// </summary>
        /// <param name="part"></param>
        public static void OnUpdate( SpritePart part, AttributeBase attribute )
        {
            int target = attribute.@int( 0 );
            float value = attribute.@float( 0 );
            var scaling = part.transform.localScale;
            float old = scaling[target];
            float v = old < 0 ? -value : value;
            scaling[target] = v;
            //scaling.z = v;
            part.transform.localScale = scaling;
        }
    }
}
