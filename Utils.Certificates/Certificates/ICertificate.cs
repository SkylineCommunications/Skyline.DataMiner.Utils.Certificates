namespace Skyline.DataMiner.Utils.Certificates
{
	using System;
	using System.Security.Cryptography.X509Certificates;

	/// <summary>
	/// Defines a certificate detailing the associated file paths and certificate details.
	/// </summary>
	public interface ICertificate : IDisposable
	{
		/// <summary>
		/// Defines the full path of the .p12 file of the certificate.
		/// </summary>
		string P12Path { get; }

		/// <summary>
		/// Defines the full path of the .crt file of the certificate.
		/// </summary>
		string CrtPath { get; }

		/// <summary>
		/// Defines the distinguished name of the issuer of the certificate.
		/// </summary>
		string Issuer { get; }

		/// <summary>
		/// Defines the certificate object of the certificate.
		/// </summary>
		X509Certificate2 CertificateFile { get; }

		/// <summary>
		/// Defines the certificate info of the certificate.
		/// </summary>
		CertificateInfo CertificateInfo { get; }

		/// <summary>
		/// Gets the issuer .crt file path from a given folder.
		/// </summary>
		/// <returns></returns>
		string GetIssuerCrtFilePath(string folderPath);

		/// <summary>
		/// Gets the issuer .p12 file path from a given folder.
		/// </summary>
		/// <returns></returns>
		string GetIssuerP12FilePath(string folderPath);
	}
}