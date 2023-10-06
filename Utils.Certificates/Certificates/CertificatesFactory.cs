namespace Skyline.DataMiner.Utils.Certificates
{
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
	}
}