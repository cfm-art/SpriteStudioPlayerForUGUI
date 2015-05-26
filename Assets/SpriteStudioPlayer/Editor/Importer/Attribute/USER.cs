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
            public string value;
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
                value = node.AtText(),
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
