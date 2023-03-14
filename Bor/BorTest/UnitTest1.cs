using Bor;
using Microsoft.VisualBasic;
using System.Xml.Linq;

namespace BorTest
{
    public class Tests
    {
        [Test]
        public void TheAddedElementShouldbeFoundInBor()
        {
            var bor = new BorClass();
            bor.Add("end");
            Assert.AreEqual(true, bor.Contains("end"), "Problems with the addition test!\n");
        }

        [Test]
        public void ByAddingAndRemovingAnElementItShouldNotStayInBor()
        {
            var bor = new BorClass();
            bor.Add("end");
            bor.Remove("end");
            Assert.AreEqual(false, bor.Contains("end"), "Problems with the deletion test!\n");
        }

        [Test]
        public void AddTwoStringsWithTheSamePrefixBorTheNumberOfStringsWithThisPrefixIsTwo()
        {
            var bor = new BorClass();
            bor.Add("endProgram");
            bor.Add("endFunction");
            Assert.AreEqual(2, bor.HowManyStartsWithPrefix("end"), "Problems with the prefix test!");
        }
    }
}