﻿using AutoMapper;
using MoneyFox.Domain.Entities;
using MoneyFox.Presentation.Tests.Collections;
using MoneyFox.Presentation.ViewModels;
using Should;
using Xunit;

namespace MoneyFox.Presentation.Tests.Mapper
{
    [Collection("AutoMapperCollection")]
    public class CategoryViewModelMapperTests
    {
        private readonly IMapper mapper;

        public CategoryViewModelMapperTests(MapperCollectionFixture fixture)
        {
            mapper = fixture.mapper;
        }

        [Fact]
        public void CategoryMappedToCorrectType()
        {
            // Arrange
            var category = new Category("test");

            // Act
            var result = mapper.Map<CategoryViewModel>(category);

            // Assert
            result.ShouldBeType<CategoryViewModel>();
        }

        [Fact]
        public void CategoryMappedToCorrectFields()
        {
            // Arrange
            var category = new Category("test", "testnote");

            // Act
            var result = mapper.Map<CategoryViewModel>(category);

            // Assert
            result.Name.ShouldEqual(category.Name);
            result.Note.ShouldEqual(category.Note);
        }
    }
}
