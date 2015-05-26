namespace a.spritestudio.editor.attribute
{
    public class BasicBooleanAttribute
        : SpriteAttribute
    {
        /// <summary>
        /// キーフレームの中身
        /// </summary>
        protected class Value
            : SpriteAttribute.ValueBase
        {
            public bool on;

            public override string ToString()
            {
                return on.ToString();
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
            return new Value() {
                on = node.AtBoolean(),
            };
        }

        /// <summary>
        /// 補間なし
        /// </summary>
        protected override bool IsInterpolation
        {
            get
            {
                return false;
            }
        }
    }
}
