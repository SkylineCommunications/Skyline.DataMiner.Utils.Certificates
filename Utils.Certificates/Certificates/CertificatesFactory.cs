namespace Skyline.DataMiner.Utils.Certificates
{
	using System;
	using System.IO;
	using System.Linq;

	/// <summary>
	/// Defines the methods available for CertificatesFactory.
	/// </summary>
	public static class CertificatesFactory
	{
		/// <summary>
		/// Get the certificate object from the .crt file path and p12 file path.
		/// </summary>
		/// <param name="crtPath">The full path of the .crt file (e.g. C:\mycertificates\mycert.crt)</param>
		/// <param name="p12Path">The full path of the .p12 file (e.g. C:\mycertificates\mycert.p12)</param>
		/// <returns></returns>
		public static ICertificate GetCertificate(string crtPath, string p12Path)
		{
			return new Certificate(crtPath, p12Path);
		}

		/// <summary>
		/// Get the certificate object from the folder containing the .crt and p12 file.
		/// </summary>
		/// <param name="folderPath">The full path of the folder.</param>
		/// <returns></returns>
		public static ICertificate GetCertificate(string folderPath)
		{
			var crtPath = Directory.GetFiles(folderPath).FirstOrDefault(x => x.EndsWith(".crt"));
			var p12Path = Directory.GetFiles(folderPath).FirstOrDefault(x => x.EndsWith(".p12"));
			if (string.IsNullOrWhiteSpace(crtPath) || string.IsNullOrWhiteSpace(p12Path))
			{
				throw new ArgumentException($"Unable to find the .crt or .p12 file at '{folderPath}'");
			}

			return new Certificate(crtPath, p12Path);
		}
	}
}