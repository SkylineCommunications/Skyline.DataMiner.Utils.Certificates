namespace Skyline.DataMiner.Utils.Certificates
{
	using System;
	using System.IO;

	/// <summary>
	/// Defines the methods available for CertificatesFactory.
	/// </summary>
	public static class CertificatesFactory
	{
		/// <summary>
		/// Get the certificate object from the .crt file path and p12 file path.
		/// </summary>
		/// <param name="crtPath">The full path of the .crt file (e.g. C:\mycertificates\mycert.crt).</param>
		/// <param name="p12Path">The full path of the .p12 file (e.g. C:\mycertificates\mycert.p12).</param>
		/// <returns>The certificate.</returns>
		public static ICertificate GetCertificate(string crtPath, string p12Path)
		{
			return new Certificate(crtPath, p12Path);
		}

		/// <summary>
		/// Get the certificate object from the folder containing the .crt and p12 file.
		/// </summary>
		/// <param name="folderPath">The full path of the folder.</param>
		/// <returns>The certificate.</returns>
		public static ICertificate GetCertificate(string folderPath)
		{
			var crtPath = Array.Find(Directory.GetFiles(folderPath), x => x.EndsWith(".crt"));
			var p12Path = Array.Find(Directory.GetFiles(folderPath), x => x.EndsWith(".p12"));
			if (string.IsNullOrWhiteSpace(crtPath) || string.IsNullOrWhiteSpace(p12Path))
			{
				throw new ArgumentException($"Unable to find the .crt or .p12 file at '{folderPath}'");
			}

			return new Certificate(crtPath, p12Path);
		}
	}
}