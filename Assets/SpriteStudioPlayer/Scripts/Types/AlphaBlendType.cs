namespace a.spritestudio.types
{
    public enum AlphaBlendType
    {
        kMix,
        kAdd,
        kMultiply,
    }

    public static class AlphaBlendTypeOperator
    {
        public static AlphaBlendType FromString( string v )
        {
            switch ( v ) {
                case "mix":
                    return AlphaBlendType.kMix;

                default:
                    return AlphaBlendType.kMix;
            }
        }
    }
}
