using a.spritestudio.attribute;

namespace a.spritestudio.editor.attribute
{
    public class ALPH
        : BasicSingleFloatAttribute
    {
        public override AttributeBase CreateKeyFrame( SpritePart part, ValueBase value )
        {
            Value v = (Value) value;
            return new TransparencyUpdater( v.value );
        }
    }
}
