using System;
using CurrencyConverterTool.Data;
using CurrencyConverterTool.Core;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CurrencyConverterTool.UnitTest
{
    [TestClass]
    public class AuthenticationManagerTests
    {
        #region IsValidSource() Tests
        [TestMethod]
        public void IsValidSource_WhenInputIsEmpty_ReturnFalse()
        {
            //Arrange
            var sourceAPIName = string.Empty;
            //Act
            var result = AuthenticationManager.GetAuthenticationManagerInstance().IsValidSource(sourceAPIName);
            //Assert
            Assert.AreEqual(false, result);
        }

        [TestMethod]
        public void IsValidSource_WhenInput_Is_NotFound_In_Database_ReturnFalse()
        {
            var sourceAPIName = "Test_API_Name_Which_Is_Not_In_Database";
            var result = AuthenticationManager.GetAuthenticationManagerInstance().IsValidSource(sourceAPIName);
            Assert.AreEqual(false, result);
        }

        [TestMethod]
        public void IsValidSource_WhenInput_Is_Found_In_Database_ReturnTrue()
        {
            var sourceAPIName = "OandaAPI";
            var result = AuthenticationManager.GetAuthenticationManagerInstance().IsValidSource(sourceAPIName);
            Assert.AreEqual(true, result);
        }
        #endregion

        #region IsActiveSource() Tests
        [TestMethod]
        public void IsActiveSource_WhenInputIsEmpty_ReturnFalse()
        {
            //Arrange
            var sourceAPIName = string.Empty;
            //Act
            var result = AuthenticationManager.GetAuthenticationManagerInstance().IsActiveSource(sourceAPIName);
            //Assert
            Assert.AreEqual(false, result);
        }

        [TestMethod]
        public void IsActiveSource_WhenInput_Is_NotFound_In_Database_ReturnFalse()
        {
            var sourceAPIName = "Test_API_Name_Which_Is_Not_In_Database";
            var result = AuthenticationManager.GetAuthenticationManagerInstance().IsActiveSource(sourceAPIName);
            Assert.AreEqual(false, result);
        }

        [TestMethod]
        public void IsActiveSource_WhenInput_Is_Found_In_Database_ReturnTrue()
        {
            var sourceAPIName = "OandaAPI";
            var result = AuthenticationManager.GetAuthenticationManagerInstance().IsActiveSource(sourceAPIName);
            Assert.AreEqual(true, result);
        }

        #endregion


        #region GetSourceAPIMaster() Test
        [TestMethod]
        public void GetSourceAPIMaster_WhenInputIsEmpty_ReturnNull()
        {
            //Arrange
            var sourceAPIName = string.Empty;
            //Act
            var result = AuthenticationManager.GetAuthenticationManagerInstance().GetSourceAPIMaster(sourceAPIName);
            //Assert
            Assert.AreEqual(null, result);
        }

        [TestMethod]
        public void GetSourceAPIMaster_WhenInput_Is_NotFound_In_Database_ReturnFalse()
        {
            var sourceAPIName = "Test_API_Name_Which_Is_Not_In_Database";
            SourceAPIMaster sourceAPIMaster = null;

            sourceAPIMaster = AuthenticationManager.GetAuthenticationManagerInstance().GetSourceAPIMaster(sourceAPIName);
            sourceAPIMaster = sourceAPIMaster == null ? null : sourceAPIMaster;

            Assert.AreEqual(null, sourceAPIMaster);
        }

        [TestMethod]
        public void GetSourceAPIMaster_WhenInput_OandaAPI_Is_Found_In_Database_ReturnTrue()
        {
            var sourceAPIName = "OandaAPI";
            SourceAPIMaster sourceAPIMaster = null;
            string output = string.Empty;

            sourceAPIMaster = AuthenticationManager.GetAuthenticationManagerInstance().GetSourceAPIMaster(sourceAPIName);
            output = sourceAPIMaster==null?string.Empty:sourceAPIMaster.SourceAPIName;

            Assert.AreEqual(sourceAPIName, output);
        }
        #endregion
    }
}
