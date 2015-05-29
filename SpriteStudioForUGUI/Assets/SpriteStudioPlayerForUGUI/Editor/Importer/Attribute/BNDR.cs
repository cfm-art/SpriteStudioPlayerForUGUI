namespace a.spritestudio.editor.attribute
{
    public class BNDR
        : SpriteAttribute
    {
        /// <summary>
        /// キーフレームの中身
        /// </summary>
        private class Value
            : SpriteAttribute.ValueBase
        {
        }

        /// <summary>
        /// キーフレーム生成
        /// </summary>
        /// <param name="key"></param>
        /// <param name="node"></param>
        /// <returns></returns>
        protected override SpriteAttribute.ValueBase CraeteValue( xml.NodeReader key, xml.NodeReader node )
        {
            throw new System.NotImplementedException();
        }
    }
}
