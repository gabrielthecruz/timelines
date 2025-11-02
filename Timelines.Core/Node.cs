using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Timelines.Core.Exceptions;

namespace Timelines.Core
{
	public class Node
	{
		public string Title { get; set; }
		public Dictionary<string, Node> Children { get; set; }

		public Node()
		{
			Children = [];
			Title = string.Empty;
		}

		public Node(string title)
		{
			Children = [];
			Title = title;
		}

		public void AddChild(Node child)
		{
			if (Children.ContainsKey(child.Title))
			{
				throw new ChildAlreadyExistsException("Cannot add existent child to node.");
			}

			Children.Add(child.Title, child);
		}

		public void AddChild(string nodeTitle)
		{
			if (Children.ContainsKey(nodeTitle))
			{
				throw new ChildAlreadyExistsException("Cannot add existent child to node.");
			}

			Children.Add(nodeTitle, new Node(nodeTitle));
		}

		public void RemoveChild(string nodeTitle)
		{
			if (!Children.ContainsKey(nodeTitle))
			{
				throw new MissingChildException($"Node '{nodeTitle}' is not a child of '{Title}'.");
			}

			Children.Remove(nodeTitle);
		}
	}
}
