namespace a.spritestudio.editor.attribute
{
    public class SIZX
        : BasicSingleFloatAttribute
    {
        public override spritestudio.attribute.AttributeBase CreateKeyFrame( SpritePart part, ValueBase value )
        {
            Value v = (Value) value;
            return spritestudio.attribute.SizeUpdater.Create(
                    spritestudio.attribute.SizeUpdater.kTargetX, v.value );
        }
    }
}
