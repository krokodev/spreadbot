namespace Crocodev.Common.Identifier
{
    public class Identifiable<TC, TV>
    {
        public class Identifier : GenericIdentifier<TC, TV>
        {
            private Identifier(TV value)
                : base(value)
            {
            }

            public Identifier()
            {
            }

            public static explicit operator Identifier(TV value)
            {
                return new Identifier(value);
            }
        }
    }
}