using a.spritestudio.attribute;

namespace a.spritestudio.editor.attribute
{
    public class ROTX
        : BasicSingleFloatAttribute
    {
        public override AttributeBase CreateKeyFrame( SpritePart part, ValueBase value )
        {
            Value v = (Value) value;
            return RotationUpdater.Create( RotationUpdater.kTargetX, v.value );
        }
    }
}
