namespace a.spritestudio.editor.attribute
{
    public class USER
        : SpriteAttribute
    {
        /// <summary>
        /// キーフレームの中身
        /// </summary>
        private class Value
            : SpriteAttribute.ValueBase
        {
            public float[] point;
            public float[] rect;
            public int? integer;
            public string text;
        }

        /// <summary>
        /// キーフレーム生成
        /// </summary>
        /// <param name="key"></param>
        /// <param name="node"></param>
        /// <returns></returns>
        protected override SpriteAttribute.ValueBase CraeteValue( xml.NodeReader key, xml.NodeReader node )
        {
            var integer = node.ChildOrNull( "integer" );
            var @string = node.ChildOrNull( "string" );
            var rect = node.ChildOrNull( "rect" );
            var point = node.ChildOrNull( "point" );

            return new Value() {
                point = point != null ? point.AtFloats( ' ' ) : null,
                rect = rect != null ? rect.AtFloats( ' ' ) : null,
                integer = integer != null ? (int?) integer.AtInteger() : null,
                text = @string != null ? @string.AtText() : null,
            };
        }

        /// <summary>
        /// 補間なし
        /// </summary>
        public override bool IsInterpolation
        {
            get
            {
                return false;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="part"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public override spritestudio.attribute.AttributeBase CreateKeyFrame( SpritePart part, ValueBase value )
        {
            Value v = (Value) value;
            return spritestudio.attribute.UserDataNotifier.Create( v.integer, v.point, v.rect, v.text );
        }
    }
}
