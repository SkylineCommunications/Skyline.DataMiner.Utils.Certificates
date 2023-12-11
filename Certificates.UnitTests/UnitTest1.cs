using System;
using System.Net;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using Skyline.DataMiner.Utils.Certificates;

namespace Certificates.UnitTests
{
	[TestClass]
	public class UnitTest1
	{
		string publicKeyPath = "C:\\Skyline DataMiner\\Documents\\DMA_COMMON_DOCUMENTS\\Certificates\\SignedCertificates\\opensearch162\\opensearch162.crt";
		string privateKeyPath = "C:\\Skyline DataMiner\\Documents\\DMA_COMMON_DOCUMENTS\\Certificates\\SignedCertificates\\opensearch162\\opensearch162.p12";
		string caFolder = @"C:\Skyline DataMiner\Documents\DMA_COMMON_DOCUMENTS\Certificates\CertificateAuthorities";

		[TestMethod]
		public void GetCertificateTest()
		{
			ICertificate certificate = CertificatesFactory.GetCertificate(publicKeyPath, privateKeyPath);
			var result = certificate.GetIssuerCrtFilePath(caFolder);
		}
	}
}
