using System;
using CurrencyConverterTool.Data;
using CurrencyConverterTool.Core;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CurrencyConverterTool.UnitTest
{
    [TestClass]
    public class ConverterTests
    {
        [TestMethod]
        public void Converter_BuildRequestDependancy_Unit_When_APIClassName_Is_Defined_EmptyOrNull_In_Database_FailThisTest()
        {
            //Arrange
            var sourceAPIName = "OandaAPI";
            SourceAPIMaster sourceAPIMaster = null;
            string output = null;
            string expectedResult = null;

            //Act
            sourceAPIMaster = AuthenticationManager.GetAuthenticationManagerInstance().GetSourceAPIMaster(sourceAPIName);
            if (sourceAPIMaster != null)
            {
                output = sourceAPIMaster.SourceAPIClassName;
                output = string.IsNullOrEmpty(output) ? null : output;
            }
            //Assert
            Assert.AreEqual(expectedResult, output);
        }

        [TestMethod]
        public void Converter_BuildRequestDependancy_Unit_When_SupportedClass_Is_Not_Defined_FailThisTest()
        {
            //Arrange
            var sourceAPIName = "CurrencyConverterAPI";
            SourceAPIMaster sourceAPIMaster = null;
            string output = null;
            Type sourceClassType = null;
            Type expectedResultType = (from assembly in AppDomain.CurrentDomain.GetAssemblies()
                                       from type in assembly.GetTypes()
                                       where type.Name == "CurrencyConverterAPI"
                                       select type).FirstOrDefault(); ;

            //Act
            sourceAPIMaster = AuthenticationManager.GetAuthenticationManagerInstance().GetSourceAPIMaster(sourceAPIName);
            if (sourceAPIMaster != null)
            {
                output = sourceAPIMaster.SourceAPIClassName;
                output = string.IsNullOrEmpty(output) ? null : output;
            }
            if (string.IsNullOrEmpty(output) == false)
            {
                sourceClassType = (from assembly in AppDomain.CurrentDomain.GetAssemblies()
                                   from type in assembly.GetTypes()
                                   where type.Name == output
                                   select type).FirstOrDefault();

                if (sourceClassType == null)
                {
                    Console.WriteLine(String.Format("Source Name: '{0}': Define class '{1}'!", sourceAPIName, output));
                }
            }
            //Assert
            Assert.AreEqual(expectedResultType, sourceClassType);
        }
    }
}
