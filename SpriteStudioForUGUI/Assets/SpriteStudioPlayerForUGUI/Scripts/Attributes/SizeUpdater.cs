using UnityEngine;
using System.Collections;

namespace a.spritestudio.attribute
{
    /// <summary>
    /// 大きさの変更
    /// </summary>
    public class SizeUpdater
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
                    ? AttributeBase.Target.kSizeX
                    : AttributeBase.Target.kSizeY,
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
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="part"></param>
        /// <param name="attribute"></param>
        public static void OnUpdateY( SpritePart part, AttributeBase attribute )
        {
            float value = attribute.@float( 0 );
        }
    }
}
