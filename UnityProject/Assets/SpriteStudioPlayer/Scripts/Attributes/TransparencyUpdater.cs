using UnityEngine;
using System.Collections;

namespace a.spritestudio.attribute
{
    /// <summary>
    /// 不透明度の変更
    /// </summary>
    public class TransparencyUpdater
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        public static AttributeBase Create( float value )
        {
            var self = new AttributeBase( AttributeBase.Target.kTransparency, null, new float[] { value }, null );
            return self;
        }

        /// <summary>
        /// 処理
        /// </summary>
        /// <param name="part"></param>
        public static void OnUpdate( SpritePart part, AttributeBase attribute )
        {
            part.Renderer.canvasRenderer.SetAlpha( attribute.@float( 0 ) );
        }
    }
}
