using a.spritestudio.attribute;

namespace a.spritestudio.editor.attribute
{
    public class ROTY
        : BasicSingleFloatAttribute
    {
        public override AttributeBase CreateKeyFrame( SpritePart part, ValueBase value )
        {
            Value v = (Value) value;
            return RotationUpdater.Create( RotationUpdater.kTargetY, v.value );
        }
    }
}
