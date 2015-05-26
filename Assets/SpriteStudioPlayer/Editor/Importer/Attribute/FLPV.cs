namespace a.spritestudio.editor.attribute
{
    public class FLPV
        : BasicBooleanAttribute
    {
        public override spritestudio.attribute.AttributeBase CreateKeyFrame( SpritePart part, ValueBase value )
        {
            Value v = (Value) value;
            return new spritestudio.attribute.Flipper( v.on, false );
        }
    }
}
