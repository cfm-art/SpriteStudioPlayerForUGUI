using UnityEngine;
using System.Collections;

namespace a.spritestudio.attribute
{
    /// <summary>
    /// 頂点の更新
    /// </summary>
    public class VertexUpdater
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="lt"></param>
        /// <param name="rt"></param>
        /// <param name="lb"></param>
        /// <param name="rb"></param>
        /// <returns></returns>
        public static AttributeBase Create( float[] lt, float[] rt, float[] lb, float[] rb )
        {
            System.Collections.Generic.List<float> values = new System.Collections.Generic.List<float>( 2 * 4 );
            values.AddRange( lt );
            values.AddRange( rt );
            values.AddRange( lb );
            values.AddRange( rb );
            var self = new AttributeBase( AttributeBase.Target.kVertex, null, 
                values.ToArray(), null );
            return self;
        }

        /// <summary>
        /// 処理
        /// </summary>
        /// <param name="part"></param>
        public static void OnUpdate( SpritePart part, AttributeBase attribute )
        {
            Vector2 lt = new Vector2( attribute.@float( 0 ), attribute.@float( 1 ) );
            Vector2 rt = new Vector2( attribute.@float( 2 ), attribute.@float( 3 ) );
            Vector2 lb = new Vector2( attribute.@float( 4 ), attribute.@float( 5 ) );
            Vector2 rb = new Vector2( attribute.@float( 6 ), attribute.@float( 7 ) );

            part.TransformVertices( lt, rt, lb, rb );
        }
    }
}
