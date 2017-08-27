using System.Linq;
using Xunit;

namespace Buffer.Tests
{
	public class BufferTests
	{
		[Fact]
		public void Buffer_With_Empty_Space()
		{
			Buffer<int> b = new Buffer<int>(4);
			b.Enqueue(1);
			Assert.Equal(1, b.Count);
			b.Enqueue(2);
			Assert.Equal(2, b.Count);
			b.Enqueue(3);
			Assert.Equal(3, b.Count);

			Assert.Equal(1, b.Dequeue());
			Assert.Equal(2, b.Dequeue());
			Assert.Equal(3, b.Dequeue());
			Assert.Equal(0, b.Count);
		}

		[Fact]
		public void Buffer_With_No_Space()
		{
			Buffer<int> b = new Buffer<int>(4);
			b.Enqueue(1);
			Assert.Equal(1, b.Count);
			b.Enqueue(2);
			Assert.Equal(2, b.Count);
			b.Enqueue(3);
			Assert.Equal(3, b.Count);
			b.Enqueue(4);
			Assert.Equal(4, b.Count);

			Assert.Equal(1, b.Dequeue());
			Assert.Equal(2, b.Dequeue());
			Assert.Equal(3, b.Dequeue());
			Assert.Equal(4, b.Dequeue());
			Assert.Equal(0, b.Count);
		}

		[Fact]
		public void Buffer_With_Overflow()
		{
			Buffer<int> b = new Buffer<int>(2);
			b.Enqueue(1);
			Assert.Equal(1, b.Count);
			b.Enqueue(2);
			Assert.Equal(2, b.Count);
			b.Enqueue(3);
			Assert.Equal(2, b.Count);

			Assert.Equal(2, b.Dequeue());
			Assert.Equal(3, b.Dequeue());
			Assert.Equal(0, b.Count);
		}

		[Fact]
		public void Buffer_Clear_Halfway()
		{
			Buffer<int> b = new Buffer<int>(2);
			b.Enqueue(1);
			Assert.Equal(1, b.Count);
			b.Enqueue(2);
			b.Enqueue(3);

			b.Clear();

			b.Enqueue(1);
			Assert.Equal(1, b.Count);
			b.Enqueue(2);
			Assert.Equal(2, b.Count);
			b.Enqueue(3);
			Assert.Equal(2, b.Count);

			Assert.Equal(2, b.Dequeue());
			Assert.Equal(3, b.Dequeue());
			Assert.Equal(0, b.Count);
		}
	}
}