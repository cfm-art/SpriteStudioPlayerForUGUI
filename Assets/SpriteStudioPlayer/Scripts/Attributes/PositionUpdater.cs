using UnityEngine;
using System.Collections;

namespace a.spritestudio.attribute
{
    /// <summary>
    /// 座標の変更
    /// </summary>
    public class PositionUpdater
    {
        public const int kTargetX = 0;
        public const int kTargetY = 1;
        public const int kTargetZ = 2;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="target"></param>
        /// <param name="value"></param>
        public static AttributeBase Create( int target, float value )
        {
            var self = new AttributeBase( AttributeBase.Target.kPosition,
                new int[] { target }, new float[] { value }, null );
            return self;
        }

        /// <summary>
        /// 処理
        /// </summary>
        /// <param name="part"></param>
        public static void OnUpdate( SpritePart part, AttributeBase attribute )
        {
            var position = part.transform.localPosition;
            position[attribute.@int( 0 )] = attribute.@float( 0 );
            part.transform.localPosition = position;
        }
    }
}
