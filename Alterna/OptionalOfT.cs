using System;
using System.Diagnostics;

namespace Alterna
{
    /// <summary>
    ///     Represents a value that may or may not be present.
    /// </summary>
    /// <typeparam name="T">
    ///     The underlying type of the <c>Optional</c>.
    /// </typeparam>
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public struct Optional<T> : IOptional<T>, IEquatable<Optional<T>>
    {
        /// <summary>
        ///     Represents the absence of value.
        /// </summary>
        public static Optional<T> None => default(Optional<T>);

        /// <summary>
        ///     Returns an <c>Optional</c> with the specified non-null value.
        /// </summary>
        /// <param name="value">
        ///     The non-null value to wrap in an <c>Optional</c>.
        /// </param>
        /// <returns>
        ///     An <c>Optional</c> with the value specified.
        /// </returns>
        /// <exception cref="InvalidOperationException">
        ///     If <paramref name="value"/> is <c>null</c>.
        /// </exception>
        public static Optional<T> Some(T value) => new Optional<T>(value);

        /// <summary>
        ///     Returns an <c>Optional</c> describing the specified value, if 
        ///     non-null, otherwise returns an empty <c>Optional</c>.
        /// </summary>
        /// <param name="value">
        ///     The possibly-null value to describe.
        /// </param>
        /// <returns>
        ///     An <c>Optional</c> with the value specified if it is non-null,
        ///     otherwise an empty <c>Optional</c>.
        /// </returns>
        public static Optional<T> From(T value) =>
            value == null ? None : Some(value);

        /// <summary>
        ///     Gets a <c>bool</c> value indicating whether the <c>Optional</c>
        ///     has a value.
        /// </summary>
        public bool HasValue { get; }

        private readonly T value;
        /// <summary>
        ///     Gets the value of the <c>Optional</c> if it has one, otherwise
        ///     throws an <see cref="InvalidOperationException"/>.
        /// </summary>
        /// <exception cref="InvalidOperationException">
        ///     If the <c>Optional</c> has no value.
        /// </exception>
        public T Value
        {
            get
            {
                if (!HasValue)
                    throw new InvalidOperationException();

                return value;
            }
        }

        /// <summary>
        ///     If the <c>Optional</c> has a value, invokes the 
        ///     <paramref name="ifSome"/> action if it is specified, passing it
        ///     the value as a parameter.
        ///     If the <c>Optional</c> has no value, invokes the 
        ///     <paramref name="ifNone"/> action if it is specified.
        ///     At least one action must be specified.
        /// </summary>
        /// <param name="ifSome">
        ///     The action to execute if the <c>Optional</c> has a value.
        /// </param>
        /// <param name="ifNone">
        ///     The action to execute if the <c>Optional</c> has no value.
        /// </param>
        /// <exception cref="ArgumentNullException">
        ///     If both <paramref name="ifSome"/> and <paramref name="ifNone"/>
        ///     are null.
        /// </exception>
        public void Match(Action<T> ifSome = null, Action ifNone = null)
        {
            if (ifSome == null && ifNone == null)
                throw new ArgumentNullException("At least one action must not be null");
            if (ifSome != null && HasValue)
                ifSome(Value);
            if (ifNone != null && !HasValue)
                ifNone();
        }

        /// <summary>
        ///     Converts an <c>Optional</c> of type <c>T</c> to a value of type 
        ///     <c>U</c>.
        /// </summary>
        /// <typeparam name="U">
        ///     The destination type of the conversion.
        /// </typeparam>
        /// <param name="ifSome">
        ///     A delegate that converts a value of the underlying type of the 
        ///     <c>Optional</c> to the destination type. Called if the 
        ///     <c>Optional</c> has a value.
        /// </param>
        /// <param name="ifNone">
        ///     A value of the destination type. It is returned if the
        ///     <c>Optional</c> has no value.
        /// </param>
        /// <returns>
        ///     The result of the conversion.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        ///     If <paramref name="ifSome"/> is <c>null</c>.
        /// </exception>
        public U Convert<U>(Func<T, U> ifSome, U ifNone)
        {
            if (ifSome == null)
                throw new ArgumentNullException(nameof(ifSome));

            return HasValue ? ifSome(Value) : ifNone;
        }

        /// <summary>
        ///     If the <c>Optional</c> has a value, apply the provided mapping 
        ///     delegate to it, and if the result is non-null, return an 
        ///     <c>Optional</c> describing the result. Otherwise return an empty
        ///     <c>Optional</c>.
        /// </summary>
        /// <typeparam name="U">
        ///     The destination type of the mapping.
        /// </typeparam>
        /// <param name="mapper">
        ///     A mapping delegate to apply to the value, if present.
        /// </param>
        /// <returns>
        ///     An <c>Optional</c> describing the result of the mapping delegate
        ///     applied to the value of this <c>Optional</c> if it has one.
        ///     Otherwise an empty <c>Optional</c>.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        ///     If <paramref name="mapper"/> is <c>null</c>.
        /// </exception>
        public Optional<U> Map<U>(Func<T, U> mapper)
        {
            if (mapper == null)
                throw new ArgumentNullException(nameof(mapper));

            return HasValue
                ? Optional<U>.Some(mapper(Value))
                : Optional<U>.None;
        }

        /// <summary>
        ///     If the <c>Optional</c> has a value, apply the provided mapping
        ///     delegate to it, and if the result is not <c>None</c>, return an
        ///     empty <c>Optional</c> describing the value of the result.
        ///     Otherwise return an empty <c>Optional</c>.
        /// </summary>
        /// <typeparam name="U">
        ///     The destination type of the mapping.
        /// </typeparam>
        /// <param name="mapper">
        ///     A mapping delegate to apply to the value, if present.
        /// </param>
        /// <returns>
        ///     An empty <c>Optional</c> describing the value of the result if
        ///     it has one. Otherwise an empty <c>Optional</c>.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        ///     If <paramref name="mapper"/> is <c>null</c>.
        /// </exception>
        public Optional<U> FlatMap<U>(Func<T, Optional<U>> mapper)
        {
            if (mapper == null)
                throw new ArgumentNullException(nameof(mapper));

            return HasValue ? mapper(Value) : Optional<U>.None;
        }

        /// <summary>
        ///     If the <c>Optional</c> has a value, and it satisfies the 
        ///     condition specified, returns an <c>Optional</c> describing the 
        ///     value. Otherwise return an empty <c>Optional</c>.
        /// </summary>
        /// <param name="condition">
        ///     A condition to test the value against.
        /// </param>
        /// <returns>
        ///     An <c>Optional</c> describing the value if it is present and 
        ///     satisfies the condition specified. Otherwise return an empty 
        ///     <c>Optional</c>.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        ///     If <paramref name="condition"/> is <c>null</c>.
        /// </exception>
        public Optional<T> When(Predicate<T> condition)
        {
            if (condition == null)
                throw new ArgumentNullException(nameof(condition));

            if (!HasValue)
                return None;

            return condition(Value) ? this : None;
        }

        /// <summary>
        ///     Returns the value of the <c>Optional</c> if it has one.
        ///     Otherwise returns the explicit default value if it is
        ///     specified, or the default value of the underlying type of
        ///     the <c>Optional</c>.
        /// </summary>
        /// <param name="defaultValue">
        ///     The value to return if the <c>Optional</c> has no value.
        /// </param>
        /// <returns>
        ///     The value of the <c>Optional</c> if it has one.
        ///     Otherwise, the explicit default value if it is
        ///     specified, or the default value of the underlying type of
        ///     the <c>Optional</c>.
        /// </returns>
        public T ValueOrDefault(T defaultValue = default(T)) =>
            HasValue ? Value : defaultValue;

        /// <summary>
        ///     Gets the value of the <c>Optional</c> if it has one.
        /// </summary>
        /// <param name="value">
        ///     When this method returns, contains the value of the 
        ///     <c>Optional</c>, if it has one; otherwise, the default value for
        ///     the type of the value parameter. This parameter is passed 
        ///     uninitialized.
        /// </param>
        /// <returns>
        ///     <c>true</c> if the <c>Optional</c> has a value; otherwise,
        ///     <c>false</c>.
        /// </returns>
        public bool TryGetValue(out T value)
        {
            value = HasValue ? Value : default(T);

            return HasValue;
        }

        /// <summary>
        ///     Determines whether the specified object is equal to this object.
        /// </summary>
        /// <param name="other">
        ///     The object to compare with the current object.
        /// </param>
        /// <returns>
        ///     A bool value indicating whether the specified object is equal to
        ///     this object.
        /// </returns>
        public bool Equals(Optional<T> other) =>
            (!HasValue && !other.HasValue)
            || (HasValue && other.HasValue && Value.Equals(other.Value));

        /// <summary>
        ///     Determines whether the specified object is equal to this object.
        /// </summary>
        /// <param name="obj">
        ///     The object to compare with the current object.
        /// </param>
        /// <returns>
        ///     A bool value indicating whether the specified object is equal to
        ///     this object.
        /// </returns>
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

        /// <summary>
        ///     Explicit conversion operator. Returns the value if present,
        ///     otherwise throws an <see cref="InvalidOperationException"/>.
        /// </summary>
        /// <param name="v">
        ///     The <c>Optional</c> to unwrap.
        /// </param>
        /// <returns>
        ///     The value if present.
        /// </returns>
        /// <exception cref="InvalidOperationException">
        ///     If the <c>Optional</c> has no value.
        /// </exception>
        public static explicit operator T(Optional<T> v) => v.Value;

        /// <summary>
        ///     Implicit conversion operator. Returns an <c>Optional</c>
        ///     describing the left hand side.
        /// </summary>
        /// <param name="v">
        ///     The value to wrap in an <c>Optional</c>.
        /// </param>
        /// <returns>
        ///     An <c>Optional</c> describing the left hand side.
        /// </returns>
        public static implicit operator Optional<T>(T v) =>
            v == null ? None : Some(v);

        /// <summary>
        ///     Returns the hash code of this <c>Optional</c>.
        /// </summary>
        /// <returns>
        ///     The hash code of this <c>Optional</c>.
        /// </returns>
        public override int GetHashCode() =>
            HasValue ? Value.GetHashCode() : 0;

        /// <summary>
        ///     Returns the string representation of this <c>Optional</c>.
        /// </summary>
        /// <returns>
        ///     The string representation of this <c>Optional</c>.
        /// </returns>
        public override string ToString() =>
            HasValue ? "Some " + Value : "None";

        /// <summary>
        ///     Returns the string representation of this <c>Optional</c> to be
        ///     used in the debugger.
        /// </summary>
        public string DebuggerDisplay => ToString();

        private Optional(T value)
        {
            if (value == null)
                throw new ArgumentNullException(nameof(value));

            this.value = value;
            HasValue = true;
        }
    }
}
