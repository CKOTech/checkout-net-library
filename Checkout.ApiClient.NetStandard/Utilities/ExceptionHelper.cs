using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Checkout.Utilities
{
    public class ExceptionHelper
    {
        public static string FlattenExceptionMessages(Exception exc)
        {
            Exception logException = exc;

            if (exc.InnerException != null)
            {
                logException = exc.InnerException;
            }
            return GetExceptionMessages(logException);
        }

        /// <summary>
        /// Gets all exception messages going through all inner excetions
        /// </summary>
        private static string GetExceptionMessages(Exception currentException)
        {
            StringBuilder errorMessage = new StringBuilder();

            while (currentException != null)
            {
                if (!string.IsNullOrEmpty(currentException.Message))
                {
                    errorMessage.AppendFormat("{0} {1}", currentException.Message, Environment.NewLine);
                }

                currentException = currentException.InnerException;
            }

            return errorMessage.ToString();
        }
    }
}