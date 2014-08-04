using AutoMapper;
using NUnit.Framework;

namespace Paramount.Betterclassifieds.Tests.Utility
{
    [TestFixture]
    public class MappingBehaviourTests
    {
        [Test]
        public void Map_SubmitViewModel_FromViewModelToDto_ReturnsDto()
        {
            // Arrange
            var mockClass = new MockClassWithMappingBehavior();
            var viewModel = new ClassAsViewModel { Id = 1, Ref = "123" };

            // Act
            var result = mockClass.SubmitViewModel(viewModel);

            // Assert
            Assert.That(result.Id, Is.EqualTo(1));
            Assert.That(result.ReferenceNumber, Is.EqualTo("123"));
        }

        [Test]
        public void MapToInstance_SubmitViewModelToInstance_FromViewModelToDto_ReturnsDto()
        {
            // Arrange
            var mockClass = new MockClassWithMappingBehavior();
            var viewModel = new ClassAsViewModel { Id = 1, Ref = "123" };
            var dtoInstance = new ClassAsDto { Id = 2, ReferenceNumber = "something else that should change" };

            // Act
            ClassAsDto result = mockClass.SubmitViewModelToInstance(viewModel, dtoInstance);

            // Assert
            Assert.That(result.Id, Is.EqualTo(1));
            Assert.That(result.ReferenceNumber, Is.EqualTo("123"));
        }

        [Test]
        public void Map_GetFromService_FromDtoToView_ReturnsViewModel()
        {
            // Arrange
            var mockClass = new MockClassWithMappingBehavior();
            var dto = new ClassAsDto { Id = 1, ReferenceNumber = "123" };

            // Act
            var result = mockClass.GetFromService(dto);

            // Assert
            Assert.That(result.Id, Is.EqualTo(1));
            Assert.That(result.Ref, Is.EqualTo("123"));
        }

        [Test]
        public void MapToInstance_GetFromService_FromDtoToView_ReturnsViewModel()
        {
            // Arrange
            var mockClass = new MockClassWithMappingBehavior();
            var dto = new ClassAsDto { Id = 1, ReferenceNumber = "123" };
            var viewModelInstance = new ClassAsViewModel { Id = 2, Ref = "This value should change!" };

            // Act
            var result = mockClass.GetFromServiceToViewModelInstance(dto, viewModelInstance);

            // Assert
            Assert.That(result.Id, Is.EqualTo(1));
            Assert.That(result.Ref, Is.EqualTo("123"));
        }

        private class MockClassWithMappingBehavior : IMappingBehaviour
        {
            public ClassAsDto SubmitViewModel(ClassAsViewModel classAsViewModel)
            {
                var dto = this.Map<ClassAsViewModel, ClassAsDto>(classAsViewModel);

                return dto;
            }

            public ClassAsDto SubmitViewModelToInstance(ClassAsViewModel viewModel, ClassAsDto dtoInstance)
            {
                this.Map(viewModel, dtoInstance);
                return dtoInstance;
            }

            public ClassAsViewModel GetFromService(ClassAsDto dto)
            {
                var viewModel = this.Map<ClassAsDto, ClassAsViewModel>(dto);
                return viewModel;
            }

            public ClassAsViewModel GetFromServiceToViewModelInstance(ClassAsDto dtoInstance, ClassAsViewModel viewModelInstance)
            {
                this.Map(dtoInstance, viewModelInstance);
                return viewModelInstance;
            }

            // Mapping definitions
            public void OnRegisterMaps(IConfiguration configuration)
            {
                configuration.CreateProfile("TestProfile");

                // To DTO
                configuration.CreateMap<ClassAsViewModel, ClassAsDto>()
                    .ForMember(member => member.ReferenceNumber, options => options.MapFrom(source => source.Ref));

                // From DTO
                configuration.CreateMap<ClassAsDto, ClassAsViewModel>()
                    .ForMember(member => member.Ref, options => options.MapFrom(source => source.ReferenceNumber));
            }

        }

        private class ClassAsViewModel
        {
            public int Id { get; set; }
            public string Ref { get; set; }
        }

        private class ClassAsDto
        {
            public int Id { get; set; }
            public string ReferenceNumber { get; set; }
        }

    }
}
