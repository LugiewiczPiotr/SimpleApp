﻿using System;
using System.Collections.Generic;
using System.Text;

namespace SimpleApp.Core
{
    public class Result
    {
        public bool Success { get; set; }
        public IEnumerable<ErrorMessage> Errors { get; set; }

        public static Result Ok()
        {
            return new Result()
            {
                Success = true
            };
        }

        public static Result<T> Ok<T>(T value)
        {
            return new Result<T>()
            {
                Success = true,
                Value = value,
                Errors = new List<ErrorMessage>()
            };
        }

        public static Result<T> Failure<T>(string message)
        {
            return Failure<T>(string.Empty, message);
        }

        public static Result<T> Failure<T>(string propertyName, string message)
        {
            var result = new Result<T>
            {
                Success = false,
                Errors = new List<ErrorMessage>()
                {
                    new ErrorMessage()
                    {
                        PropertyName = propertyName,
                        Message = message
                    }
                }
            };

            return result;
        }

        public static Result<T> Failure<T>(IEnumerable<ErrorMessage> errorsList)
        {
            var result = new Result<T>
            {
                Success = false,
                Errors = errorsList
            };

            return result;
        }
    }

    public class Result<T> : Result
    {
        public T Value { get; set; }
    }

    public class ErrorMessage
    {
        public string PropertyName { get; set; }
        public string Message { get; set; }
    }
}
