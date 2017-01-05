using System;

namespace Alterna
{
    /// <summary>
    ///     Represents a value that may or may not be present.
    /// </summary>
    /// <typeparam name="T">
    ///     The underlying type of the <c>Optional</c>.
    /// </typeparam>
    public interface IOptional<out T>
    {
        /// <summary>
        ///     Gets a <c>bool</c> value indicating whether the <c>Optional</c>
        ///     has a value.
        /// </summary>
        bool HasValue { get; }

        /// <summary>
        ///     Gets the value of the <c>Optional</c> if it has one, otherwise
        ///     throws an <see cref="InvalidOperationException"/>.
        /// </summary>
        /// <exception cref="InvalidOperationException">
        ///     If the <c>Optional</c> has no value.
        /// </exception>
        T Value { get; }
    }
}
