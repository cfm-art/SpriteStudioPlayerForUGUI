using a.spritestudio.attribute;

namespace a.spritestudio.editor.attribute
{
    public class SCLY
        : BasicSingleFloatAttribute
    {
        public override AttributeBase CreateKeyFrame( SpritePart part, ValueBase value )
        {
            Value v = (Value) value;
            return ScalingUpdater.Create( ScalingUpdater.kTargetY, v.value );
        }
    }
}
