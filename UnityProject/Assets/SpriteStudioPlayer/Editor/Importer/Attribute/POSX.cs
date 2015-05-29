using a.spritestudio.attribute;

namespace a.spritestudio.editor.attribute
{
    public class POSX
        : BasicSingleFloatAttribute
    {
        /// <summary>
        /// 生成
        /// </summary>
        /// <param name="part"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public override AttributeBase CreateKeyFrame( SpritePart part, ValueBase value )
        {
            Value v = (Value) value;
            return PositionUpdater.Create( PositionUpdater.kTargetX, v.value );
        }
    }
}
