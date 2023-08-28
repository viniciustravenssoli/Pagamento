using System.Collections.Generic;
using System.Security;
using Manager.API.ViewModels;

namespace Manager.API.Utilities{
    public static class Responses{
        public static ResultViewModel ApplicationErrorMessage()
        {
            return new ResultViewModel
            {
                Message = "Internal aplication error, please try again",
                Success = false,
                Data = null
            };
        }

        public static ResultViewModel DomainErrorMessage(string message)
        {
            return new ResultViewModel
            {
                Message = message,
                Success = false,
                Data = null
            };
        }

        public static ResultViewModel DomainErrorMessage(string message, IReadOnlyCollection<string> errors)
        {
            return new ResultViewModel
            {
                Message = message,
                Success = false,
                Data = errors
            };
        }

        public static ResultViewModel UserDoNotExists()
        {
            return new ResultViewModel
            {
                Message = "User does not exists, please check the email",
                Success = false,
                Data = null
            };
        }

        public static ResultViewModel UnauthorizedErrorMessage()
        {
            return new ResultViewModel
            {
                Message = "Login or Password do not match",
                Success = false,
                Data = null
            };
        }

        public static ResultViewModel InvalidPayloud()
        {
            return new ResultViewModel
            {
                Message = "Invalid Payloud",
                Success = false,
                Data = null
            };
        }

    }
}