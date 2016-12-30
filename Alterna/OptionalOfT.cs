using System;
using System.Diagnostics;

namespace Alterna
{
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
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
            if (ifSome == null && ifNone == null)
                throw new ArgumentNullException("At least one action must not be null");
            if (ifSome != null && HasValue)
                ifSome(Value);
            if (ifNone != null && !HasValue)
                ifNone();
        }

        public U Convert<U>(Func<T, U> ifSome, U ifNone)
        {
            if (ifSome == null)
                throw new ArgumentNullException(nameof(ifSome));

            return HasValue ? ifSome(Value) : ifNone;
        }

        public Optional<U> Map<U>(Func<T, U> mapper)
        {
            if (mapper == null)
                throw new ArgumentNullException(nameof(mapper));

            return HasValue
                ? Optional<U>.Some(mapper(Value))
                : Optional<U>.None;
        }

        public Optional<U> FlatMap<U>(Func<T, Optional<U>> mapper)
        {
            if (mapper == null)
                throw new ArgumentNullException(nameof(mapper));

            return HasValue ? mapper(Value) : Optional<U>.None;
        }

        public Optional<T> When(Predicate<T> condition)
        {
            if (!HasValue)
                return None;

            return condition(Value) ? this : None;
        }

        public T ValueOrDefault(T defaultValue = default(T)) =>
            HasValue ? Value : defaultValue;

        public bool Equals(Optional<T> other) =>
            (!HasValue && !other.HasValue)
            || (HasValue && other.HasValue && Value.Equals(other.Value));

        public override bool Equals(object obj)
        {
            if (obj == null && !HasValue)
                return true;

            if (obj is Optional<T>)
                return Equals((Optional<T>)obj);

            if (obj is T && HasValue)
                return Value.Equals(obj);

            return false;
        }

        public static explicit operator T(Optional<T> v) => v.Value;

        public static implicit operator Optional<T>(T v) =>
            v == null ? None : Some(v);

        public override int GetHashCode() =>
            HasValue ? Value.GetHashCode() : 0;

        public string DebuggerDisplay =>
            HasValue ? "Some " + Value : "None";
    }
}
