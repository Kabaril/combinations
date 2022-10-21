using Terraria.DataStructures;

namespace Combinations.Items.MasterThrowingCharm
{
	public sealed class EntitySource_MasterThrowingCharm : IEntitySource
	{
		public string Context { get; }

		public EntitySource_MasterThrowingCharm(string context = null)
		{
			Context = context;
		}
	}
}
