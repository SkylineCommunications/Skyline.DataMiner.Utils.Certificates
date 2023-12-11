namespace Skyline.DataMiner.Utils.Certificates
{
	using System.Collections.Generic;
	using System.IO;
	using System.Linq;
	using System.Security.Cryptography.X509Certificates;

	internal class Certificate : ICertificate 
	{
		private readonly string _crtPath;
		private readonly string _p12Path;
		private string _issuerCrtPath;
		private string _issuerP12Path;
		private CertificateInfo certificateInfo;
		private X509Certificate2 certificate;

		public Certificate(string crtPath, string p12Path)
		{
			_crtPath = crtPath;
			_p12Path = p12Path;
		}

		public string CrtPath
		{
			get
			{
				return _crtPath;
			}
		}

		public string P12Path
		{
			get
			{
				return _p12Path;
			}
		}

		public string Issuer
		{
			get
			{
				return CertificateFile.Issuer;
			}
		}

		public CertificateInfo CertificateInfo
		{
			get
			{
				if (certificateInfo == null)
				{
					GetCertificateInfo();
				}
				return certificateInfo;
			}
		}

		public X509Certificate2 CertificateFile
		{
			get
			{
				if (certificate == null)
				{
					GetCertificateFile();
				}
				return certificate;
			}
		}

		public void Dispose()
		{
			certificate.Dispose();
		}

		public string GetIssuerCrtFilePath(string folderPath)
		{
			if (!string.IsNullOrEmpty(_issuerCrtPath))
			{
				return _issuerCrtPath;
			}

			var issuer = Issuer;

			foreach (var folder in Directory.GetDirectories(folderPath))
			{
				var folderName = folder.Substring(folder.LastIndexOf("\\") + 1);
				_issuerCrtPath = Directory.GetFiles(folder).First(x => x.EndsWith(".crt"));
				_issuerP12Path = Directory.GetFiles(folder).First(x => x.EndsWith(".p12"));

				var ca = CertificatesFactory.GetCertificate(_issuerCrtPath, _issuerP12Path);
				if (UtilityFunctions.CompareDistinguishedNames(issuer, ca.CertificateInfo.DistinguishedName))
				{
					return _issuerCrtPath;
				}				
			}

			return string.Empty;
		}

		public string GetIssuerP12FilePath(string folderPath)
		{
			if (!string.IsNullOrEmpty(_issuerP12Path))
			{
				return _issuerP12Path;
			}

			var issuer = Issuer;

			foreach (var folder in Directory.GetDirectories(folderPath))
			{
				var folderName = folder.Substring(folder.LastIndexOf("\\") + 1);
				_issuerCrtPath = Directory.GetFiles(folder).First(x => x.EndsWith(".crt"));
				_issuerP12Path = Directory.GetFiles(folder).First(x => x.EndsWith(".p12"));

				return _issuerP12Path;
			}

			return string.Empty;
		}

		private void GetCertificateFile()
		{
			certificate = new X509Certificate2(_crtPath);
		}

		private void GetCertificateInfo()
		{
			var subject = CertificateFile.SubjectName.Name;
			Dictionary<string, string> parsedSubjectName = new Dictionary<string, string>();
			foreach (var name in subject.Split(','))
			{
				var str = name.Trim();
				var split = str.Split('=');
				parsedSubjectName[split[0]] = split[1];
			}

			certificateInfo = new CertificateInfo(parsedSubjectName["CN"], parsedSubjectName["O"], parsedSubjectName["OU"], parsedSubjectName["C"]);
		}		
	}
}