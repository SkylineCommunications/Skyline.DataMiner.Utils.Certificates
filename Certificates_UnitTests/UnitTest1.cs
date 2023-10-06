namespace Certificates_UnitTests
{
	using System;
	using System.Collections.Generic;
	using System.IO;
	using System.Linq;

	using Microsoft.VisualStudio.TestTools.UnitTesting;

	using Skyline.DataMiner.Utils.Certificates;

	[TestClass]
	public class UnitTest1
	{
		[TestMethod]
		public void GetCertInfo()
		{
			string folderPath = "C:\\Skyline DataMiner\\Documents\\DMA_COMMON_DOCUMENTS\\Certificates\\SignedCertificates";
			var certificates = new Dictionary<string, Skyline.DataMiner.Utils.Certificates.ICertificate>();
			foreach (string folder in Directory.GetDirectories(folderPath))
			{
				foreach (string inner_folder in Directory.GetDirectories(folder))
				{
					var p12 = Directory.GetFiles(inner_folder).First(x => x.EndsWith(".p12"));
					var crt = Directory.GetFiles(inner_folder).First(x => x.EndsWith(".crt"));
					ICertificate cert = CertificatesFactory.GetCertificate(crt, p12);

					certificates[inner_folder] = cert;
				}
			}

			foreach (var cert in certificates)
			{
				var dn = cert.Value.CertificateInfo.DistinguishedName;
				var expiry = cert.Value.CertificateFile.NotAfter;
				var issuer = cert.Value.Issuer;
			}
		}

		[TestMethod]
		public void TransferCerts()
		{
			var rootCAsPath = "C:\\Skyline DataMiner\\Documents\\DMA_COMMON_DOCUMENTS\\Certificates\\CertificateAuthorities";

			var p12Path = "C:\\Skyline DataMiner\\Documents\\DMA_COMMON_DOCUMENTS\\Certificates\\SignedCertificates\\opensearch_cert\\opensearch_cert.p12";
			var crtPath = "C:\\Skyline DataMiner\\Documents\\DMA_COMMON_DOCUMENTS\\Certificates\\SignedCertificates\\opensearch_cert\\opensearch_cert.crt";
			var certificate = CertificatesFactory.GetCertificate(crtPath, p12Path);

			var res = certificate.GetIssuerFilePath(rootCAsPath);
		}
	}
}
