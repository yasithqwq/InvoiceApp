using FluentAssertions;
using FluentValidation.Results;
using Invoice.Domain.Exceptions;
using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace Invoice.Application.UnitTests.Exceptions
{
    public class EntityNotFoundExceptionTests
    {
        [Test]
        public void DefaultConstructorCreatesAnEmptyErrorDictionary()
        {
            var actual = new EntityNotFoundException("Invoice","Fail");

            actual.Message.Should().BeEquivalentTo("Entity \"Invoice\" (Fail) was not found.");
        }
    }
}
