﻿using System.Diagnostics.CodeAnalysis;
using MediatR;
using MoneyFox.Application.Resources;
using MoneyFox.Presentation.ViewModels;
using Moq;
using Should;
using Xunit;

namespace MoneyFox.Presentation.Tests.ViewModels
{
    [ExcludeFromCodeCoverage]
    public class AddAccountViewModelTests
    {
        [Fact]
        public void Ctor_CategoryCreated()
        {
            // Arrange
            // Act
            var mediator = new Mock<IMediator>();

            var addAccountVm = new AddAccountViewModel(mediator.Object, null, null, null, null, null);


            // Assert
            addAccountVm.SelectedAccount.ShouldNotBeNull();
        }

        [Fact]
        public void Ctor_Title_Set()
        {
            // Arrange
            // Act
            var mediator = new Mock<IMediator>();

            var addAccountVm = new AddAccountViewModel(mediator.Object, null, null, null, null, null);

            // Assert
            addAccountVm.Title.ShouldEqual(Strings.AddAccountTitle);
        }
    }
}
