namespace Skyline.DataMiner.Utils.Certificates
{
	using System;
	using System.IO;
	using System.Security.Cryptography.X509Certificates;

	internal sealed class Certificate : ICertificate
	{
		private readonly string _crtPath;
		private readonly string _p12Path;
		private X509Certificate2 _certificate;
		private DistinguishedName _issuer;
		private DistinguishedName _subject;

		public Certificate(string crtPath, string p12Path)
		{
			_crtPath = crtPath;
			_p12Path = p12Path;
		}

		public X509Certificate2 CertificateFile
		{
			get
			{
				_certificate = _certificate ?? new X509Certificate2(_crtPath);
				return _certificate;
			}
		}

		public string CrtPath
		{
			get
			{
				return _crtPath;
			}
		}

		public bool IsSelfSigned
		{
			get
			{
				return Issuer == Subject;
			}
		}

		public DistinguishedName Issuer
		{
			get
			{
				if (_issuer == null)
				{
					var issuer = CertificateFile.Issuer;
					_issuer = new DistinguishedName(issuer);
				}

				return _issuer;
			}
		}

		public string P12Path
		{
			get
			{
				return _p12Path;
			}
		}

		public DistinguishedName Subject
		{
			get
			{
				if (_subject == null)
				{
					var subject = CertificateFile.SubjectName.Name;
					_subject = new DistinguishedName(subject);
				}

				return _subject;
			}
		}

		public void Dispose()
		{
			_certificate?.Dispose();
		}

		public ICertificate GetIssuersCert(params string[] folderPaths)
		{
			foreach (var folder in folderPaths)
			{
				foreach (var subfolder in Directory.GetDirectories(folder))
				{
					var issuerCrtPath = Array.Find(Directory.GetFiles(subfolder), x => x.EndsWith(".crt"));
					var issuerP12Path = Array.Find(Directory.GetFiles(subfolder), x => x.EndsWith(".p12"));
					if (string.IsNullOrWhiteSpace(issuerCrtPath) || string.IsNullOrWhiteSpace(issuerP12Path))
					{
						continue;
					}

					var issuerCrt = CertificatesFactory.GetCertificate(issuerCrtPath, issuerP12Path);
					if (issuerCrt.Subject == Issuer)
					{
						return issuerCrt;
					}
				}
			}

			throw new FileNotFoundException("Was unable to find the certificate files (.crt and .p12) that match the issuer");
		}
	}
}