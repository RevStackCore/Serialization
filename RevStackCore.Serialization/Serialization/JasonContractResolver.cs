using System;
using Newtonsoft.Json.Serialization;

namespace RevStackCore.Serialization
{
	public class LowerCaseContractResolver : DefaultContractResolver
	{
		protected override string ResolvePropertyName(string propertyName)
		{
			return propertyName.ToLower();
		}
	}
}
