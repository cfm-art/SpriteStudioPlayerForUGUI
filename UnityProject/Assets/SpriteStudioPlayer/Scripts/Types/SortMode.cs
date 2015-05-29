namespace a.spritestudio.types
{
    public enum SortMode
    {
        kPriority
    }

    public static class SortModeOpeartor
    {
        public static SortMode FromString( string v )
        {
            switch ( v ) {
                case "prio":
                    return SortMode.kPriority;

                default:
                    return SortMode.kPriority;
            }
        }
    }
}
