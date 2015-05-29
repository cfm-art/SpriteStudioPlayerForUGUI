namespace a.spritestudio.types
{
    public enum BoundsType
    {
        kNone,
    }

    public static class BoundsTypeOperator
    {
        public static BoundsType FromString( string v )
        {
            switch ( v ) {
                case "none":
                    return BoundsType.kNone;

                default:
                    return BoundsType.kNone;
            }
        }
    }
}
