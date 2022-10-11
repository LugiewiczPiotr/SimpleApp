using System.Linq;
using FluentAssertions;
using FluentAssertions.Execution;
using FluentAssertions.Primitives;
using SimpleApp.Core;

namespace Microsoft.AspNetCore.Mvc
{
    public static class ActionResultExtensions
    {
        public static ActionResultAssertions Should(this IActionResult actionResult)
        {
            return new ActionResultAssertions(actionResult);
        }

        public class ActionResultAssertions : ReferenceTypeAssertions<IActionResult, ActionResultAssertions>
        {
            public ActionResultAssertions(IActionResult actionResult)
            {
                Subject = actionResult;
            }

            protected override string Identifier => "actionResult";

            public ActionResultAssertions BeOk<T>(T value, string because = "", params object[] becauseArgs)
            {
                var subject = Subject.As<ObjectResult>();
                var subjectValue = subject.Value.As<Result<T>>();

                Execute.Assertion
                    .BecauseOf(because, becauseArgs)
                    .ForCondition(subject != null)
                    .FailWith("The ObjectResult cannot be null");

                Execute.Assertion
                    .BecauseOf(because, becauseArgs)
                    .ForCondition(subjectValue.Success)
                    .FailWith("The Success should be true");

                Execute.Assertion
                    .BecauseOf(because, becauseArgs)
                    .ForCondition(subject.Value != null)
                    .FailWith("Value cannot be null");

                Execute.Assertion
                   .BecauseOf(because, becauseArgs)
                   .ForCondition(subject.StatusCode == 200)
                   .FailWith("The StatusCode should be 200");

                value.Should().BeEquivalentTo(subjectValue.Value);

                Execute.Assertion
                    .BecauseOf(because, becauseArgs)
                    .ForCondition(!subjectValue.Errors.Any())
                    .FailWith("The Errors should be null");

                return this;
            }

            public ActionResultAssertions BeCreatedAtAction<T>(T value, string because = "", params object[] becauseArgs)
            {
                var subject = Subject.As<ObjectResult>();
                var subjectValue = subject.Value.As<Result<T>>();

                Execute.Assertion
                    .BecauseOf(because, becauseArgs)
                    .ForCondition(subject != null)
                    .FailWith("The ObjectResult cannot be null");

                Execute.Assertion
                    .BecauseOf(because, becauseArgs)
                    .ForCondition(subjectValue.Success)
                    .FailWith("The Success should be true");

                Execute.Assertion
                    .BecauseOf(because, becauseArgs)
                    .ForCondition(subject.Value != null)
                    .FailWith("Value cannot be null");

                Execute.Assertion
                   .BecauseOf(because, becauseArgs)
                   .ForCondition(subject.StatusCode == 201)
                   .FailWith("The StatusCode should be 201");

                value.Should().BeEquivalentTo(subjectValue.Value);

                Execute.Assertion
                    .BecauseOf(because, becauseArgs)
                    .ForCondition(!subjectValue.Errors.Any())
                    .FailWith("The Errors should be null");

                return this;
            }

            public ActionResultAssertions BeBadRequest<T>(
                string message,
                string because = "",
                params object[] becauseArgs)
            {
                var subject = Subject.As<ObjectResult>();
                var subjectValue = subject.Value.As<Result<T>>();

                Execute.Assertion
                    .BecauseOf(because, becauseArgs)
                    .ForCondition(Subject != null)
                    .FailWith("The ObjectResult cannot be null");

                Execute.Assertion
                    .BecauseOf(because, becauseArgs)
                    .ForCondition(subjectValue.Success == false)
                    .FailWith("The Success should be false");

                Execute.Assertion
                    .BecauseOf(because, becauseArgs)
                    .ForCondition(subjectValue.Success == false && (subjectValue.Value == null || subject.Value.Equals(default(T))))
                    .FailWith("The Value should be null");

                Execute.Assertion
                    .BecauseOf(because, becauseArgs)
                    .ForCondition(subjectValue.Errors != null)
                    .FailWith("The Errors should have errors");

                Execute.Assertion
                 .BecauseOf(because, becauseArgs)
                 .ForCondition(subject.StatusCode == 400)
                 .FailWith("The StatusCode should be 400");

                return this;
            }

            public ActionResultAssertions BeNotFound<T>(
               string message,
               string because = "",
               params object[] becauseArgs)
            {
                var subject = Subject.As<ObjectResult>();
                var subjectValue = subject.Value.As<Result<T>>();

                Execute.Assertion
                    .BecauseOf(because, becauseArgs)
                    .ForCondition(Subject != null)
                    .FailWith("The ObjectResult cannot be null");

                Execute.Assertion
                    .BecauseOf(because, becauseArgs)
                    .ForCondition(subjectValue.Success == false)
                    .FailWith("The Success should be false");

                Execute.Assertion
                    .BecauseOf(because, becauseArgs)
                    .ForCondition(subjectValue.Errors != null)
                    .FailWith("The Errors should have errors");

                Execute.Assertion
                 .BecauseOf(because, becauseArgs)
                 .ForCondition(subject.StatusCode == 404)
                 .FailWith("The StatusCode should be 404");

                return this;
            }
        }
    }
}
