namespace Skyline.DataMiner.Utils.Certificates
{
	using System.Collections.Generic;
	using System.Linq;

	internal static class UtilityFunctions
	{
		internal static bool CompareDistinguishedNames(string cert1Name, string cert2Name)
		{
			var cert1Names = ParseDistinguishedName(cert1Name);
			var cert2Names = ParseDistinguishedName(cert2Name);

			var dictionaryComparer = new DictionaryComparer<string, string>();

			return dictionaryComparer.Equals(cert1Names, cert2Names);
		}

		internal static Dictionary<string, string> ParseDistinguishedName(string distinguishedName)
		{
			var dict = new Dictionary<string, string>();
			var nameArray = distinguishedName.Split(',');

			foreach (var name in nameArray)
			{
				var nameSplit = name.Split('=');
				var key = nameSplit[0].Trim();
				var value = nameSplit[1].Trim();

				dict.Add(key, value);
			}

			return dict;
		}

		internal class DictionaryComparer<TKey, TValue> : IEqualityComparer<Dictionary<TKey, TValue>>
		{
			public bool Equals(Dictionary<TKey, TValue> x, Dictionary<TKey, TValue> y)
			{
				// Check whether the dictionaries are equal
				return x.Count == y.Count && !x.Except(y).Any();
			}

			public int GetHashCode(Dictionary<TKey, TValue> obj)
			{
				int hash = 0;
				foreach (var pair in obj)
				{
					hash ^= pair.GetHashCode();
				}
				return hash;
			}
		}
	}
}
