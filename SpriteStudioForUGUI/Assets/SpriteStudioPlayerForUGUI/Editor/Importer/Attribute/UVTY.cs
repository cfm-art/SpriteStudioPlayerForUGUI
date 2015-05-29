namespace a.spritestudio.editor.attribute
{
    /// <summary>
    ///  S  T  U  V
    /// SX SY TX TY
    /// </summary>
    public class UVTY
        : BasicSingleFloatAttribute
    {
        public override spritestudio.attribute.AttributeBase CreateKeyFrame( SpritePart part, ValueBase value )
        {
            Value v = (Value) value;
            return spritestudio.attribute.TextureCoordUpdater.Create(
                    spritestudio.attribute.TextureCoordUpdater.kTargetV, v.value );
        }
    }
}
