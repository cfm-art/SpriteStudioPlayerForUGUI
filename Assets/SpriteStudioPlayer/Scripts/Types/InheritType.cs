namespace a.spritestudio.types
{
    public enum InheritType
    {
        kParent,
    }

    public static class InheritTypeOperator
    {
        public static InheritType FromString( string v )
        {
            switch ( v ) {
                case "parent":
                    return InheritType.kParent;

                default:
                    return InheritType.kParent;
            }
        }
    }
}
