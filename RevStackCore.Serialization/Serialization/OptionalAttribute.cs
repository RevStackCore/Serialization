using System;
namespace RevStackCore.Serialization
{
	/// <summary>
	/// Marks a property as optional when being serialized. 
	/// </summary>
	[AttributeUsage(AttributeTargets.Property)]
	public class OptionalAttribute : Attribute
	{
	}
}
