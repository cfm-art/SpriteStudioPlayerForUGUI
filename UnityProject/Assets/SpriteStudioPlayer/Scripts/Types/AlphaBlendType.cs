namespace a.spritestudio.types
{
    public enum AlphaBlendType
    {
        kMix,
        kAdd,
        kMul,
        kSub,
    }

    public static class AlphaBlendTypeOperator
    {
        public static AlphaBlendType FromString( string v )
        {
            switch ( v ) {
                case "mix":
                    return AlphaBlendType.kMix;
                case "mul":
                    return AlphaBlendType.kMul;
                case "sub":
                    return AlphaBlendType.kSub;
                case "add":
                    return AlphaBlendType.kAdd;

                default:
                    return AlphaBlendType.kMix;
            }
        }
    }
}
