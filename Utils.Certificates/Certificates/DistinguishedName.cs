namespace Skyline.DataMiner.Utils.Certificates
{
	using System;
	using System.Collections.Generic;

	/// <summary>
	/// The distinguished name.
	/// </summary>
	public sealed class DistinguishedName : IEquatable<DistinguishedName>
	{
		private Dictionary<string, string> _lookup;

		/// <summary>
		/// Initialize a new instance of <see cref="DistinguishedName"/>.
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
				if (Lookup.ContainsKey("CN"))
				{
					return Lookup["CN"];
				}
				else
				{
					return string.Empty;
				}
			}
		}

		/// <summary>
		/// Gets the CountryName (C) of the distinguished name.
		/// </summary>
		public string CountryName
		{
			get
			{
				if (Lookup.ContainsKey("C"))
				{
					return Lookup["C"];
				}
				else
				{
					return string.Empty;
				}
			}
		}

		/// <summary>
		/// Gets the LocalityName (L) of the distinguished name.
		/// </summary>
		public string LocalityName
		{
			get
			{
				if (Lookup.ContainsKey("L"))
				{
					return Lookup["L"];
				}
				else
				{
					return string.Empty;
				}
			}
		}

		/// <summary>
		/// The lookup dictionary for a DN with ',' or ';' as separator for the different entries and '=' as separator between the attribute name and value.
		/// </summary>
		public Dictionary<string, string> Lookup
		{
			get
			{
				if (_lookup == null)
				{
					_lookup = new Dictionary<string, string>();
					var parts = Value.Split(new[] { ',', ';' });
					foreach (var part in parts)
					{
						if (part.Contains("="))
						{
							var valuepair = part.Split(new[] { '=' }, 2);
							_lookup[valuepair[0]] = valuepair[1];
						}
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
				if (Lookup.ContainsKey("OU"))
				{
					return Lookup["OU"];
				}
				else
				{
					return string.Empty;
				}
			}
		}

		/// <summary>
		/// Gets the OrganizationName (O) of the distinguished name.
		/// </summary>
		public string OrganizationName
		{
			get
			{
				if (Lookup.ContainsKey("O"))
				{
					return Lookup["O"];
				}
				else
				{
					return string.Empty;
				}
			}
		}

		/// <summary>
		/// Gets the StateOrProvinceName (ST) of the distinguished name.
		/// </summary>
		public string StateOrProvinceName
		{
			get
			{
				if (Lookup.ContainsKey("ST"))
				{
					return Lookup["ST"];
				}
				else
				{
					return string.Empty;
				}
			}
		}

		/// <summary>
		/// Gets the StreetAddress (STREET) of the distinguished name.
		/// </summary>
		public string StreetAddress
		{
			get
			{
				if (Lookup.ContainsKey("STREET"))
				{
					return Lookup["STREET"];
				}
				else
				{
					return string.Empty;
				}
			}
		}

		/// <summary>
		/// The value of the distinguished name.
		/// </summary>
		public string Value { get; private set; }

		/// <summary>
		/// Initialize a new instance of <see cref="DistinguishedName"/>.
		/// </summary>
		/// <param name="commonName">The CommonName.</param>
		/// <param name="localityName">The LocalityName.</param>
		/// <param name="stateOrProvinceName">The StateOrProvinceName.</param>
		/// <param name="organizationName">The OrganizationName.</param>
		/// <param name="organizationalUnitName">The OrganizationalUnitName.</param>
		/// <param name="countryName">The CountryName.</param>
		/// <param name="streetAddress">The StreetAddress.</param>
		public static DistinguishedName GetDistinguishedName(string commonName, string organizationName = null, string organizationalUnitName = null, string countryName = null, string localityName = null, string stateOrProvinceName = null, string streetAddress = null)
		{
			List<string> parts = new List<string>();
			if (!string.IsNullOrWhiteSpace(commonName)) { parts.Add(commonName); }
			if (!string.IsNullOrWhiteSpace(localityName)) { parts.Add(localityName); }
			if (!string.IsNullOrWhiteSpace(stateOrProvinceName)) { parts.Add(stateOrProvinceName); }
			if (!string.IsNullOrWhiteSpace(organizationName)) { parts.Add(organizationName); }
			if (!string.IsNullOrWhiteSpace(organizationalUnitName)) { parts.Add(organizationalUnitName); }
			if (!string.IsNullOrWhiteSpace(countryName)) { parts.Add(countryName); }
			if (!string.IsNullOrWhiteSpace(streetAddress)) { parts.Add(streetAddress); }
			string dn = string.Join(",", parts.ToArray());
			if (string.IsNullOrWhiteSpace(dn))
			{
				throw new ArgumentException("At least one field has to be provided");
			}

			return new DistinguishedName(dn);
		}

		/// <inheritdoc/>
		public static bool operator !=(DistinguishedName obj1, DistinguishedName obj2) => !(obj1 == obj2);

		/// <inheritdoc/>
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

			DistinguishedName tObj = obj as DistinguishedName;
			if (tObj == null)
			{
				return false;
			}
			else
			{
				return Equals(tObj);
			}
		}

		/// <inheritdoc/>
		public override int GetHashCode()
		{
			return Value.GetHashCode();
		}
	}
}