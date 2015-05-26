using a.spritestudio.attribute;

namespace a.spritestudio.editor.attribute
{
    public class PRIO
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
            return new PriorityUpdater( (int) v.value );
        }
    }
}
