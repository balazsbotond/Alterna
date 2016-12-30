using System;

namespace Alterna
{
    public struct Optional<T> : IEquatable<Optional<T>>
    {
        public static Optional<T> None => default(Optional<T>);
        public static Optional<T> Some(T value) => new Optional<T>(value);
        public static Optional<T> From(T value) =>
            value == null ? None : Some(value);

        public bool HasValue { get; }

        private readonly T value;
        public T Value
        {
            get
            {
                if (!HasValue)
                    throw new InvalidOperationException();

                return value;
            }
        }

        private Optional(T value)
        {
            if (value == null)
                throw new ArgumentNullException(nameof(value));

            this.value = value;
            HasValue = true;
        }

        public void Match(Action<T> ifSome = null, Action ifNone = null)
        {
            if (ifSome != null && HasValue)
                ifSome(Value);
            if (ifNone != null && !HasValue)
                ifNone();
        }

        public U Convert<U>(Func<T, U> ifSome, Func<U> ifNone) { throw new NotImplementedException(); }

        public void Map<U>(Func<T, Optional<U>> mapper) { }

        public Optional<U> FlatMap<U>(Func<T, Optional<U>> mapper) { throw new NotImplementedException(); }

        public Optional<T> When(Predicate<T> condition) { throw new NotImplementedException(); }

        public T ValueOrDefault(T defaultValue = default(T)) =>
            HasValue ? Value : defaultValue;

        public bool Equals(Optional<T> other) =>
            Value.Equals(other.Value) && HasValue == other.HasValue;

        public override bool Equals(object obj)
        {
            if (obj is Optional<T>)
                return Equals((Optional<T>)obj);

            return false;
        }

        public override int GetHashCode() =>
            Value.GetHashCode() ^ HasValue.GetHashCode();
    }
}
