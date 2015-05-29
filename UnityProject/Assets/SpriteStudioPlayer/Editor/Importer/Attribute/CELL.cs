using a.spritestudio.attribute;

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
        public override bool IsInterpolation
        {
            get
            {
                return false;
            }
        }

        /// <summary>
        /// 生成
        /// </summary>
        /// <param name="part"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public override AttributeBase CreateKeyFrame( SpritePart part, ValueBase value )
        {
            Value v = (Value) value;
            var cell = part.Root.CellMap( v.mapId );
            return CellUpdater.Create( v.mapId, cell.FindCell( v.name ) );
        }
    }
}
