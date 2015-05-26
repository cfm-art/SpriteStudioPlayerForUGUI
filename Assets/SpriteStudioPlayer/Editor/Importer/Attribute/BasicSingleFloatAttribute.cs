namespace a.spritestudio.editor.attribute
{
    public class BasicSingleFloatAttribute
        : SpriteAttribute
    {
        /// <summary>
        /// キーフレームの中身
        /// </summary>
        protected class Value
            : SpriteAttribute.ValueBase
        {
            public string ipType;
            public float value;

            public override string ToString()
            {
                return string.Format( "ipType={0}, value={1}", ipType, value );
            }
        }

        /// <summary>
        /// キーフレーム生成
        /// </summary>
        /// <param name="key"></param>
        /// <param name="node"></param>
        /// <returns></returns>
        protected override SpriteAttribute.ValueBase CraeteValue( xml.NodeReader key, xml.NodeReader node )
        {
            var ipType = key.Attribute( "ipType" );
            return new Value() {
                ipType = ipType != null ? ipType.AtText() : "linear",
                value = node.AtFloat()
            };
        }
    }
}
