using FluentValidation;
using FluentValidation.Results;

namespace Moq
{
    public static class MockExtensions
    {
        public static void SetValidationSuccess<T>(this Mock<IValidator<T>> validator)
        {
            validator.Setup(r => r.Validate(It.IsAny<T>())).Returns(new ValidationResult());
        }

        public static void SetValidationFailure<T>(this Mock<IValidator<T>> validator, string validatedProperty, string errorMessage)
        {
            validator.Setup(r => r.Validate(It.IsAny<T>())).Returns(new ValidationResult(new[]
            {
                new ValidationFailure(validatedProperty, errorMessage),
            }));
        }
    }
}
