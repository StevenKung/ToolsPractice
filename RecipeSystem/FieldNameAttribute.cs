using System;

namespace RecipeSystem
{
	/// <summary>
	/// Attach this attribute on enum field
	/// </summary>
	public class FieldNameAttribute : Attribute
	{
		public readonly string Name;
		public FieldNameAttribute(string name)
		{
			Name = name;
		}
	}
}
