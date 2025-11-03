using Timelines.Core;
using Timelines.Core.Exceptions;

namespace Timelines.Test
{
	public class NodeTest
	{
		private readonly Node RootNode;

		public NodeTest() => RootNode = new Node { Title = "Root node" };

		[Fact]
		public void AddChild()
		{
			var node = new Node { Title = "Test Node" };
			RootNode.AddChild(node);

			Assert.Single(RootNode.Children);
		}

		[Fact]
		public void AddDuplicatedChild()
		{
			var node = new Node{ Title = "Test Node" };
			RootNode.AddChild(node);

			Assert.Throws<ChildAlreadyExistsException>(() => RootNode.AddChild(node));
		}

		[Fact]
		public void RemoveChild()
		{
			var node = new Node { Title = "Test Node" };

			RootNode.AddChild(node);
			RootNode.RemoveChild(node.Title);

			Assert.Empty(RootNode.Children);
		}

		[Fact]
		public void RemoveMissingChildByTitle()
		{
			Assert.Throws<MissingChildException>(() => RootNode.RemoveChild("Test Node"));
		}
	}
}