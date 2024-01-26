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
		/// Defines the certificate object of the certificate.
		/// </summary>
		X509Certificate2 CertificateFile { get; }

		/// <summary>
		/// Defines the full path of the .crt file of the certificate.
		/// </summary>
		string CrtPath { get; }

		/// <summary>
		/// Gets whether or not the certificate is self-signed.
		/// </summary>
		bool IsSelfSigned { get; }

		/// <summary>
		/// Defines the distinguished name of the issuer of the certificate.
		/// </summary>
		DistinguishedName Issuer { get; }

		/// <summary>
		/// Defines the full path of the .p12 file of the certificate.
		/// </summary>
		string P12Path { get; }

		/// <summary>
		/// Defines the distinguished name of the certificate.
		/// </summary>
		DistinguishedName Subject { get; }

		/// <summary>
		/// Get the issuer from a set of given folders. This method should only be used if the certificate is not <see cref="IsSelfSigned"/>.
		/// For every folder provided the folder should contain for every certificate a folder that contains .p12 and .crt files.
		/// </summary>
		/// <param name="folderPaths">The folder paths to search the issuer.</param>
		/// <returns>The certificate of the issuer</returns>
		ICertificate GetIssuer(params string[] folderPaths);
	}
}