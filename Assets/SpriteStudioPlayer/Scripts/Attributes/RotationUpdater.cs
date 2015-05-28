using UnityEngine;
using System.Collections;

namespace a.spritestudio.attribute
{
    /// <summary>
    /// 回転の変更
    /// </summary>
    public class RotationUpdater
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
            var self = new AttributeBase( AttributeBase.Target.kRotation, new int[] { target }, new float[] { value }, null );
            return self;
        }

        /// <summary>
        /// 処理
        /// </summary>
        /// <param name="part"></param>
        public static void OnUpdate( SpritePart part, AttributeBase attribute )
        {
            var rotation = part.transform.localRotation;
            var angles = rotation.eulerAngles;
            angles[attribute.@int( 0 )] = attribute.@float( 0 );
            rotation.eulerAngles = angles;
            part.transform.localRotation = rotation;
        }
    }
}
