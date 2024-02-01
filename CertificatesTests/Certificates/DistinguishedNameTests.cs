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
			string expectedValue = "CN=My Root CA, O=Skyline Communications, OU=Phoenix, C=BE";
			Assert.AreEqual(expectedValue, subject.Value, "value");
			var otherSubject = new DistinguishedName(subject.Value);
			Assert.AreEqual("My Root CA", otherSubject.CommonName, "other CN");
			Assert.AreEqual("Skyline Communications", otherSubject.OrganizationName, "other O");
			Assert.AreEqual("Phoenix", otherSubject.OrganizationalUnitName, "other OU");
			Assert.AreEqual("BE", otherSubject.CountryName, "other C");
		}

		[TestMethod()]
		public void DistinguishedNameTest2()
		{
			string dn = "C=BE, O=Skyline Communications, OU=Phoenix, CN=MichielsRoot";
			var otherSubject = new DistinguishedName(dn);
			Assert.AreEqual("MichielsRoot", otherSubject.CommonName, "other CN");
			Assert.AreEqual("Skyline Communications", otherSubject.OrganizationName, "other O");
			Assert.AreEqual("Phoenix", otherSubject.OrganizationalUnitName, "other OU");
			Assert.AreEqual("BE", otherSubject.CountryName, "other C");
		}
	}
}