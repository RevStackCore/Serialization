using System;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;

namespace RevStackCore.Serialization
{
	public static class Json
	{
		/// <summary>
        /// Serialize the specified value.
        /// </summary>
        /// <returns>The serialize.</returns>
        /// <param name="value">Value.</param>
        public static string Serialize(object value)
        {
            return JsonConvert.SerializeObject(value);
        }

        /// <summary>
		///     Json serialize a type to a string
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="value"></param>
		/// <returns>string</returns>
		public static string SerializeObject<T>(T value)
		{
			return serializeObject(value, false, true);
		}

		/// <summary>
		///  Json serialize a type to a string overload
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="value"></param>
		/// <param name="quoteName"></param>
		/// <returns>string</returns>
		public static string SerializeObject<T>(T value, bool quoteName)
		{
			return serializeObject(value, quoteName, true);
		}

		/// <summary>
		///   Json serialize a type to a string overload
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="value"></param>
		/// <param name="quoteName"></param>
		/// <param name="camelCase"></param>
		/// <returns>string</returns>
		public static string SerializeObject<T>(T value, bool quoteName, bool camelCase)
		{
			return serializeObject(value, quoteName, camelCase);
		}


		/// <summary>
		/// Casts an object to a generic type 
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="value"></param>
		/// <returns></returns>
		public static T Cast<T>(object value)
		{
			var json = serializeObject(value, false, false);
			var obj = DeserializeObject<T>(json);
			return obj;
		}

		/// <summary>
		/// an object to a generic type 
		/// </summary>
		/// <returns>The cast.</returns>
		/// <param name="value">Value.</param>
		/// <param name="camelCase">If set to <c>true</c> camel case.</param>
		/// <typeparam name="T">The 1st type parameter.</typeparam>
		public static T Cast<T>(object value, bool camelCase)
		{
			var json = serializeObject(value, false, camelCase);
			var obj = DeserializeObject<T>(json);
			return obj;
		}

		/// <summary>
		///     Deserialize Json string to object method overload
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="json"></param>
		/// <param name="properCase"></param>
		/// <returns></returns>
		public static T DeserializeObject<T>(string json, bool properCase)
		{
			return deserializeObject<T>(json, properCase);
		}


		/// <summary>
		///     Deserialize Json synchronously using JsonConvert
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="json"></param>
		/// <returns>T</returns>
		public static T DeserializeObject<T>(string json)
		{
			return JsonConvert.DeserializeObject<T>(json);
		}


		/// <summary>
		/// 
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="value"></param>
		/// <param name="quoteName"></param>
		/// <param name="camelCase"></param>
		/// <returns>string</returns>
		private static string serializeObject<T>(T value, bool quoteName, bool camelCase)
		{
			using (var stringWriter = new StringWriter())
			using (var jsonWriter = new JsonTextWriter(stringWriter))
			{
				JsonSerializer serializer;
				if (camelCase)
				{
					serializer = new JsonSerializer
					{
						ContractResolver = new CamelCasePropertyNamesContractResolver(),
						StringEscapeHandling = StringEscapeHandling.EscapeHtml
					};
				}
				else
				{
					serializer = new JsonSerializer
					{
						StringEscapeHandling = StringEscapeHandling.EscapeHtml
					};
				}

				serializer.Converters.Add(new StringEnumConverter());
				jsonWriter.QuoteName = quoteName;
				serializer.Serialize(jsonWriter, value);

				return stringWriter.ToString();
			}
		}

		/// <summary>
		/// 
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="json"></param>
		/// <param name="properCase"></param>
		/// <returns>T</returns>
		private static T deserializeObject<T>(string json, bool properCase)
		{
			using (var stringReader = new StringReader(json))
			using (var jsonReader = new JsonTextReader(stringReader))
			{
				JsonSerializer serializer;
				if (properCase)
				{
					serializer = new JsonSerializer
					{
						ContractResolver = new CamelCasePropertyNamesContractResolver()
					};
				}
				else
				{
					serializer = new JsonSerializer();
				}

				return serializer.Deserialize<T>(jsonReader);
			}
		}
	}
}
