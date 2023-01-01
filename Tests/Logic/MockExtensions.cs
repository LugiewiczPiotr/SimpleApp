using System.Threading;
using FluentValidation;
using FluentValidation.Results;

namespace Moq
{
    public static class MockExtensions
    {
        public static void SetValidationSuccess<T>(this Mock<IValidator<T>> validator)
        {
            validator.Setup(r => r.ValidateAsync(It.IsAny<T>(), CancellationToken.None)).ReturnsAsync(new ValidationResult());
        }

        public static void SetValidationFailure<T>(this Mock<IValidator<T>> validator, string validatedProperty, string errorMessage)
        {
            validator.Setup(r => r.ValidateAsync(It.IsAny<T>(), CancellationToken.None)).ReturnsAsync(new ValidationResult(new[]
            {
                new ValidationFailure(validatedProperty, errorMessage),
            }));
        }
    }
}
