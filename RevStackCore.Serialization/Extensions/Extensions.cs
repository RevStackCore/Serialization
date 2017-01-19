using System;
namespace RevStackCore.Serialization
{
	public static class Extensions
	{
		public static string ToJsonString<T>(this T src)
        {
            if (src == null) return null;
            return Json.SerializeObject<T>(src, true, true);
        }

		public static string ToJsonString<T>(this T src, bool camelCase)
        {
            if (src == null) return null;
            return Json.SerializeObject<T>(src,true,camelCase);
        }
	}
}
