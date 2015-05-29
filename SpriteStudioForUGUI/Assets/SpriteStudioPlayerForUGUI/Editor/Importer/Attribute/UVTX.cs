namespace a.spritestudio.editor.attribute
{
    /// <summary>
    ///  S  T  U  V
    /// SX SY TX TY
    /// </summary>
    public class UVTX
        : BasicSingleFloatAttribute
    {
        public override spritestudio.attribute.AttributeBase CreateKeyFrame( SpritePart part, ValueBase value )
        {
            Value v = (Value) value;
            return spritestudio.attribute.TextureCoordUpdater.Create(
                    spritestudio.attribute.TextureCoordUpdater.kTargetU, v.value );
        }
    }
}
