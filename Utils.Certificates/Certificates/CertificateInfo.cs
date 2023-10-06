namespace Skyline.DataMiner.Utils.Certificates
{
	using System.Collections.Generic;

	public class CertificateInfo
	{
		/// <summary>
		/// Initialize a new instance of <see cref="CertificateInfo"/>.
		/// </summary>
		/// <param name="commonName">The common name of the certificate.</param>
		/// <param name="organization">The organization field of the certificate.</param>
		/// <param name="organizationalUnit">The organizational unit field of the certificate.</param>
		/// <param name="country">The country field of the certificate.</param>
		public CertificateInfo(string commonName, string organization, string organizationalUnit, string country)
		{
			CommonName = commonName;
			Organization = organization;
			OrganizationalUnit = organizationalUnit;
			Country = country;
		}

		/// <summary>
		/// Gets or sets the common name of the certificate.
		/// </summary>
		public string CommonName { get; set; }

		/// <summary>
		/// Gets or sets the organization of the certificate.
		/// </summary>
		public string Organization { get; set; }

		/// <summary>
		/// Gets or sets the organizational unit of the certificate.
		/// </summary>
		public string OrganizationalUnit { get; set; }

		/// <summary>
		/// Gets or sets the country of the certificate.
		/// </summary>
		public string Country { get; set; }

		/// <summary>
		/// Gets the distinguished name of the certificate
		/// </summary>
		public string DistinguishedName
		{
			get
			{
				var builder = new List<string>();

				builder.Add($"CN={CommonName}");

				if (!string.IsNullOrEmpty(OrganizationalUnit))
				{
					builder.Add($"OU={OrganizationalUnit}");
				}

				if (!string.IsNullOrEmpty(Organization))
				{
					builder.Add($"O={Organization}");
				}

				if (!string.IsNullOrEmpty(Country))
				{
					builder.Add($"C={Country}");
				}

				return string.Join(",", builder);
			}
		}
	}
}