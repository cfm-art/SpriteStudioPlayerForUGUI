namespace a.spritestudio.editor.attribute
{
    public class CELL
        : SpriteAttribute
    {
        /// <summary>
        /// キーフレームの中身
        /// </summary>
        private class Value
            : SpriteAttribute.ValueBase
        {
            public int mapId;
            public string name;

            public override string ToString()
            {
                return string.Format( "mapId={0}, name={1}", mapId, name );
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
                mapId = node.AtInteger( "mapId" ),
                name = node.AtText( "name" ),
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
