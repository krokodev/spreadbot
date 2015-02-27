using System.Diagnostics;

namespace Crocodev.Common.Identifier
{
   public class GenericIdentifier<TC, TV>
    {
        private readonly TV _value;

        public TV Value
        {
            get { return _value; }
        }

        protected GenericIdentifier(TV value)
        {
            _value = value;
        }

        protected GenericIdentifier()
        {
            _value = default(TV);
        }

        public static implicit operator TV(GenericIdentifier<TC, TV> value)
        {
            return value._value;
        }

        public override string ToString()
        {
            Trace.Assert(_value != null);
            return _value.ToString();
        }

        public static bool operator ==(GenericIdentifier<TC, TV> a, GenericIdentifier<TC, TV> b)
        {
            Trace.Assert(a != null);
            return a.Equals(b);
        }

        public static bool operator !=(GenericIdentifier<TC, TV> a, GenericIdentifier<TC, TV> b)
        {
            Trace.Assert(a!=null);
            return !a.Equals(b);
        }

        private bool Equals(GenericIdentifier<TC, TV> other)
        {
            if (ReferenceEquals(null, _value))
                return ReferenceEquals(null, other._value);
            return _value.Equals(other._value);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
                return false;
            return obj is GenericIdentifier<TC, TV> && Equals((GenericIdentifier<TC, TV>)obj);
        }

        public override int GetHashCode()
        {
            return ReferenceEquals(null, _value) ? 0 : _value.GetHashCode();
        }
    }
}