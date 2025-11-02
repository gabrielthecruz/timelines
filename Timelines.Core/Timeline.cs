using System.Runtime.CompilerServices;

namespace Timelines.Core
{
	public class Timeline
	{
		public Node Root { get; set; }

		public Timeline(Node root) => Root = root;
		public Timeline(string rootTitle) => Root = new Node(rootTitle);
	}
}
