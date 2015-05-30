namespace a.spritestudio.editor.attribute
{
    public class VERT
        : SpriteAttribute
    {
        /// <summary>
        /// キーフレームの中身
        /// </summary>
        private class Value
            : SpriteAttribute.ValueBase
        {
            public float[] lt;
            public float[] rt;
            public float[] lb;
            public float[] rb;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="part"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public override spritestudio.attribute.AttributeBase CreateKeyFrame( SpritePart part, SpriteAttribute.ValueBase value )
        {
            Value v = (Value) value;
            return spritestudio.attribute.VertexUpdater.Create( v.lt, v.rt, v.lb, v.rb );
        }

        /// <summary>
        /// キーフレーム生成
        /// </summary>
        /// <param name="key"></param>
        /// <param name="node"></param>
        /// <returns></returns>
        protected override SpriteAttribute.ValueBase CraeteValue( xml.NodeReader key, xml.NodeReader node )
        {
            return new Value() {
                lt = node.AtFloats( "LT", ' ' ),
                rt = node.AtFloats( "RT", ' ' ),
                lb = node.AtFloats( "LB", ' ' ),
                rb = node.AtFloats( "RB", ' ' ),
            };
        }
    }
}
