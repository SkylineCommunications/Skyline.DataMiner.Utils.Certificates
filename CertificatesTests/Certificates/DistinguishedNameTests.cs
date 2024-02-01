namespace Skyline.DataMiner.Utils.Certificates.Tests
{
	using Microsoft.VisualStudio.TestTools.UnitTesting;

	using Skyline.DataMiner.Utils.Certificates;

	[TestClass()]
	public class DistinguishedNameTests
	{
		[TestMethod()]
		public void DistinguishedNameTest()
		{
			var subject = DistinguishedName.GetDistinguishedName("My Root CA", "Skyline Communications", "Phoenix", "BE");
			Assert.AreEqual("My Root CA", subject.CommonName, "CN");
			Assert.AreEqual("Skyline Communications", subject.OrganizationName, "O");
			Assert.AreEqual("Phoenix", subject.OrganizationalUnitName, "OU");
			Assert.AreEqual("BE", subject.CountryName, "C");
			var otherSubject = new DistinguishedName(subject.Value);
			Assert.AreEqual("My Root CA", otherSubject.CommonName, "other CN");
			Assert.AreEqual("Skyline Communications", otherSubject.OrganizationName, "other O");
			Assert.AreEqual("Phoenix", otherSubject.OrganizationalUnitName, "other OU");
			Assert.AreEqual("BE", otherSubject.CountryName, "other C");
		}
	}
}