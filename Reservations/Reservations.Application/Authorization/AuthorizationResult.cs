using System.Collections.Generic;

namespace Reservations.Application.Authorization
{
    public  class AuthorizationResult
    {
        public AuthorizationResult(bool success, IEnumerable<string> errors = null)
        {
            Success = success;
            Errors = errors ?? new List<string>();
        }

        /// <summary>
        /// Indicates status of the authorization, True in case of success, False in case of failure
        /// </summary>
        public bool Success { get; }

        /// <summary>
        /// Optional list of errors
        /// </summary>
        public IEnumerable<string> Errors { get; }
    }
}
