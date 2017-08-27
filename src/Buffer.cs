using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Buffer
{
	/// <summary>
	/// A rolling buffer
	/// </summary>
	/// <typeparam name="T"></typeparam>
	/// <seealso cref="System.Collections.Generic.IEnumerable{T}" />
	public class Buffer<T> : IEnumerable<T>
	{
		private int _maxForVariable;
		private T[] _values;
		private int _start;
		private int _end;
		private bool _hasWritten;
		private int _count;

		/// <summary>
		/// Gets the count.
		/// </summary>
		public int Count
		{
			get { return _count; }
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="Buffer{T}"/> class.
		/// </summary>
		/// <param name="maxElements">The maximum elements.</param>
		public Buffer(int maxElements)
		{
			_maxForVariable = maxElements - 1;
			_values = new T[maxElements];
		}

		/// <summary>
		/// Pushes the specified value.
		/// </summary>
		/// <param name="value">The value.</param>
		public void Enqueue(T value)
		{
			// if we've caught up to start again, it needs to run away from us
			if (_end == _start && _hasWritten)
			{
				IncrementOrRollover(ref _start);
			}
			_values[_end] = value;
			if (_count != _values.Length) _count++;
			IncrementOrRollover(ref _end);
			_hasWritten = true;
		}

		private void IncrementOrRollover(ref int variable)
		{
			if (variable == _maxForVariable)
			{
				variable = 0;
			}
			else
			{
				variable++;
			}
		}

		/// <summary>
		/// Pops this instance.
		/// </summary>
		/// <returns></returns>
		public T Dequeue()
		{
			T result = _values[_start];
			_count--;
			IncrementOrRollover(ref _start);
			return result;
		}

		/// <summary>
		/// Clears this instance.
		/// </summary>
		public void Clear()
		{
			_values = new T[_values.Length];
			_start = 0;
			_end = 0;
			_count = 0;
			_hasWritten = false;
		}

		/// <summary>
		/// Returns an enumerator that iterates through the collection.
		/// </summary>
		/// <returns>
		/// An enumerator that can be used to iterate through the collection.
		/// </returns>
		public IEnumerator<T> GetEnumerator()
		{
			return ((IEnumerable<T>)_values).GetEnumerator();
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return _values.GetEnumerator();
		}
	}
}