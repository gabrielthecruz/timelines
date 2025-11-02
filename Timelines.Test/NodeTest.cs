using NuGet.Frameworks;
using System.Xml.Linq;
using Timelines.Core;
using Timelines.Core.Exceptions;

namespace Timelines.Test
{
	public class NodeTest
	{
		private readonly Node RootNode;

		public NodeTest() => RootNode = new Node("Root node");

		[Fact]
		public void AddChildByNode()
		{
			var node = new Node("Test Node");
			RootNode.AddChild(node);

			Assert.Single(RootNode.Children);
		}

		[Fact]
		public void AddDuplicatedChildByNode()
		{
			var node = new Node("Test Node");
			RootNode.AddChild(node);

			Assert.Throws<ChildAlreadyExistsException>(() => RootNode.AddChild(node));
		}

		[Fact]
		public void AddChildByTitle()
		{
			RootNode.AddChild("Test Node");

			Assert.Single(RootNode.Children);
		}

		[Fact]
		public void AddDuplicatedChildByTitle()
		{
			var nodeTitle = "Test Node";
			RootNode.AddChild(nodeTitle);

			Assert.Throws<ChildAlreadyExistsException>(() => RootNode.AddChild(nodeTitle));
		}

		[Fact]
		public void RemoveChildByTitle()
		{
			var nodeTitle = "Test Node";

			RootNode.AddChild(nodeTitle);
			RootNode.RemoveChild(nodeTitle);

			Assert.Empty(RootNode.Children);
		}

		[Fact]
		public void RemoveMissingChildByTitle()
		{
			Assert.Throws<MissingChildException>(() => RootNode.RemoveChild("Test Node"));
		}
	}
}