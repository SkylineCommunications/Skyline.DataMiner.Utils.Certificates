namespace Skyline.DataMiner.Utils.Certificates
{
	using System;
	using System.Collections.Generic;
	using System.Linq;

	/// <summary>
	/// The distinguished name.
	/// </summary>
	public sealed class DistinguishedName : IEquatable<DistinguishedName>
	{
		private Dictionary<string, string> _lookup;

		/// <summary>
		/// Initializes a new instance of the <see cref="DistinguishedName"/> class.
		/// </summary>
		/// <param name="distinguishedName">The distinguished name or certificate.</param>
		public DistinguishedName(string distinguishedName)
		{
			Value = distinguishedName;
		}

		/// <summary>
		/// Gets the CommonName (CN) of the distinguished name.
		/// </summary>
		public string CommonName
		{
			get
			{
				return Lookup.ContainsKey("CN") ? Lookup["CN"] : string.Empty;
			}
		}

		/// <summary>
		/// Gets the CountryName (C) of the distinguished name.
		/// </summary>
		public string CountryName
		{
			get
			{
				return Lookup.ContainsKey("C") ? Lookup["C"] : string.Empty;
			}
		}

		/// <summary>
		/// Gets the LocalityName (L) of the distinguished name.
		/// </summary>
		public string LocalityName
		{
			get
			{
				return Lookup.ContainsKey("L") ? Lookup["L"] : string.Empty;
			}
		}

		/// <summary>
		/// Gets the lookup dictionary for a DN with ',' or ';' as separator for the different entries and '=' as separator between the attribute name and value.
		/// </summary>
		public Dictionary<string, string> Lookup
		{
			get
			{
				if (_lookup == null)
				{
					_lookup = new Dictionary<string, string>();
					var parts = Value.Split(',', ';');
					foreach (var part in parts.Where(p => p.Contains("=")))
					{
						var valuepair = part.Split(new[] { '=' }, 2);
						_lookup[valuepair[0].Trim()] = valuepair[1].Trim();
					}
				}

				return _lookup;
			}
		}

		/// <summary>
		/// Gets the OrganizationalUnitName (OU) of the distinguished name.
		/// </summary>
		public string OrganizationalUnitName
		{
			get
			{
				return Lookup.ContainsKey("OU") ? Lookup["OU"] : string.Empty;
			}
		}

		/// <summary>
		/// Gets the OrganizationName (O) of the distinguished name.
		/// </summary>
		public string OrganizationName
		{
			get
			{
				return Lookup.ContainsKey("O") ? Lookup["O"] : string.Empty;
			}
		}

		/// <summary>
		/// Gets the StateOrProvinceName (ST) of the distinguished name.
		/// </summary>
		public string StateOrProvinceName
		{
			get
			{
				return Lookup.ContainsKey("ST") ? Lookup["ST"] : string.Empty;
			}
		}

		/// <summary>
		/// Gets the StreetAddress (STREET) of the distinguished name.
		/// </summary>
		public string StreetAddress
		{
			get
			{
				return Lookup.ContainsKey("STREET") ? Lookup["STREET"] : string.Empty;
			}
		}

		/// <summary>
		/// Gets the value of the distinguished name.
		/// </summary>
		public string Value { get; private set; }

		/// <summary>
		/// Initialize a new instance of <see cref="DistinguishedName"/>.
		/// </summary>
		/// <param name="commonName">The CommonName.</param>
		/// <param name="organizationName">The OrganizationName.</param>
		/// <param name="organizationalUnitName">The OrganizationalUnitName.</param>
		/// <param name="countryName">The CountryName.</param>
		/// <param name="localityName">The LocalityName.</param>
		/// <param name="stateOrProvinceName">The StateOrProvinceName.</param>
		/// <param name="streetAddress">The StreetAddress.</param>
		/// <returns>The distinguished name.</returns>
		public static DistinguishedName GetDistinguishedName(
			string commonName,
			string organizationName = null,
			string organizationalUnitName = null,
			string countryName = null,
			string localityName = null,
			string stateOrProvinceName = null,
			string streetAddress = null)
		{
			List<string> parts = new List<string>();
			if (!string.IsNullOrWhiteSpace(commonName))
			{
				parts.Add("CN=" + commonName);
			}

			if (!string.IsNullOrWhiteSpace(localityName))
			{
				parts.Add("L=" + localityName);
			}

			if (!string.IsNullOrWhiteSpace(stateOrProvinceName))
			{
				parts.Add("ST=" + stateOrProvinceName);
			}

			if (!string.IsNullOrWhiteSpace(organizationName))
			{
				parts.Add("O=" + organizationName);
			}

			if (!string.IsNullOrWhiteSpace(organizationalUnitName))
			{
				parts.Add("OU=" + organizationalUnitName);
			}

			if (!string.IsNullOrWhiteSpace(countryName))
			{
				parts.Add("C=" + countryName);
			}

			if (!string.IsNullOrWhiteSpace(streetAddress))
			{
				parts.Add("STREET=" + streetAddress);
			}

			string dn = string.Join(", ", parts.ToArray());
			if (string.IsNullOrWhiteSpace(dn))
			{
				throw new ArgumentException("At least one field has to be provided");
			}

			return new DistinguishedName(dn);
		}

		public static bool operator !=(DistinguishedName obj1, DistinguishedName obj2) => !(obj1 == obj2);

		public static bool operator ==(DistinguishedName obj1, DistinguishedName obj2)
		{
			if (ReferenceEquals(obj1, obj2))
				return true;
			if (ReferenceEquals(obj1, null))
				return false;
			if (ReferenceEquals(obj2, null))
				return false;
			return obj1.Equals(obj2);
		}

		/// <inheritdoc/>
		public bool Equals(DistinguishedName other)
		{
			if (other == null)
			{
				return false;
			}

			return this.Value == other.Value;
		}

		/// <inheritdoc/>
		public override bool Equals(object obj)
		{
			if (obj == null)
			{
				return false;
			}

			DistinguishedName transformedObj = obj as DistinguishedName;
			return Equals(transformedObj);
		}

		/// <inheritdoc/>
		public override int GetHashCode()
		{
			return Value.GetHashCode();
		}
	}
}