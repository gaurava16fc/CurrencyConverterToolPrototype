using System;
using CurrencyConverterTool.Data;
using System.Linq;
using CurrencyConverterTool.ExceptionLogger;

namespace CurrencyConverterTool.Core
{
    public sealed class AuthenticationManager
    {
        private static readonly object _objLock = new object();
        private static AuthenticationManager _authenticationManagerInstance;
        private CurrencyConverterEntities entities = new CurrencyConverterEntities();

        //private constructor
        private AuthenticationManager()
        {
            // do nothing...
        }

        /// <summary>
        /// static Property to get instance of this Singleton class
        /// </summary>
        public static AuthenticationManager GetAuthenticationManagerInstance()
        {
            try
            {
                if (_authenticationManagerInstance == null)
                {
                    // Thread-Safe block
                    lock (_objLock)
                    {
                        if (_authenticationManagerInstance == null)
                        {
                            _authenticationManagerInstance = new AuthenticationManager();
                        }
                    }
                }
                return _authenticationManagerInstance;
            }
            catch (Exception)
            {
                throw;
            }
        }


        /// <summary>
        /// Check if the given sourceAPI Name is valild and exists into the DB table
        /// </summary>
        /// <param name="sourceAPIName"></param>
        /// <returns>Returns True if the given sourceAPI Name is valild and exists into the DB table, else false</returns>
        public bool IsValidSource(string sourceAPIName)
        {
            try
            {
                if (string.IsNullOrEmpty(sourceAPIName))
                    return false;

                var _source = GetSourceAPIMaster(sourceAPIName);
                if (_source != null)
                {
                    return true;
                }
                return false;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Check if the given sourceAPI Name is active in the DB table
        /// </summary>
        /// <param name="sourceAPIName"></param>
        /// <returns>Returns True if the given sourceAPI Name is active in the DB table, else false</returns>
        public bool IsActiveSource(string sourceAPIName)
        {
            try
            {
                if (string.IsNullOrEmpty(sourceAPIName))
                    return false;

                var _source = GetSourceAPIMaster(sourceAPIName);
                if (_source != null)
                {
                    return _source.IsActiveAPI;
                }
                return false;
            }
            catch (Exception)
            {
                throw;
            }
        }


        /// <summary>
        /// Extract the SourceAPIMaster entity against the given sourceAPI Name from the DB table
        /// </summary>
        /// <param name="sourceAPIName"></param>
        /// <returns>Returns SourceAPIMaster entity against the given sourceAPI Name populated from the DB table</returns>
        public SourceAPIMaster GetSourceAPIMaster(string sourceAPIName)
        {
            try
            {
                if (string.IsNullOrEmpty(sourceAPIName))
                    return null;

                return entities.SourceAPIMasters.FirstOrDefault(e => e.SourceAPIName.Trim().ToLower() == sourceAPIName.Trim().ToLower());
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
