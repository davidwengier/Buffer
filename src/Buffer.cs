using System;
using System.Collections.Generic;
using System.Linq;

namespace Buffer
{
	public class Buffer<T> : IEnumerable<T>
	{
		private T[] m_values;
		private int m_start;
		private int m_end;

		public Buffer(int maxElements)
		{
			m_values = new T[maxElements];
			m_end = 0;
		}

		public void Push(T value)
		{
			m_values[m_end] = value;
			m_end++;
		}

		public T Pop()
		{
			m_start++;
			return m_values[m_start - 1];
		}
	}
}