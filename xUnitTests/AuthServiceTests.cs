using AuthMicroservice.DataAccess.Interfaces;
using AuthMicroservice.Model;
using AuthMicroservice.Services.Implentations;
using AuthMicroservice.Services.Utility;
using AutoMapper;

namespace xUnitTests;
using Moq;
using Xunit;

public class AuthServiceTests
{ 
    private readonly UserService _userService;
    private readonly Mock<IUserRepository> _userRepositoryMock = new Mock<IUserRepository>();
    private readonly Mock<HashingLogic> _hashingLogicMock = new Mock<HashingLogic>();
    private readonly Mock<IMapper> _mapperMock = new Mock<IMapper>();
   
    public AuthServiceTests()
    {
        _userService = new UserService(_hashingLogicMock.Object, _userRepositoryMock.Object, _mapperMock.Object);
    }
    
    [Fact]
    public void CreateUser_CreatesUserWithHashedPassword()
    {
        // Arrange
        var userDto = new UserDto { Username = "testuser", Email = "test@example.com", password = "password123" };
        var user = new User { Username = userDto.Username, Email = userDto.Email };

        _mapperMock.Setup(x => x.Map<User>(It.IsAny<UserDto>())).Returns(user);

        _hashingLogicMock.Setup(x => x.GenerateHash(userDto.password, out It.Ref<byte[]>.IsAny, out It.Ref<byte[]>.IsAny))
            .Callback((string password, out byte[] passwordHash, out byte[] passwordSalt) =>
            {
                passwordHash = new byte[] { 1, 2, 3 }; // Simulated hash and salt for the test
                passwordSalt = new byte[] { 4, 5, 6 }; 
            });

        _userRepositoryMock.Setup(x => x.CreateUser(It.IsAny<User>())).Returns((User u) =>
        {
            u.Id = 1; 
            return u;
        });

        // Act
        var result = _userService.CreateUser(userDto);

        // Assert
        Assert.NotNull(result);
        Assert.Equal("testuser", result.Username);
        Assert.Equal("test@example.com", result.Email);
        Assert.NotEmpty(result.HashPassword);
        Assert.NotEmpty(result.SaltPassword);
    }
    
    [Fact]
    public void DeleteUser_DeletesUserWhenFound()
    {
        // Arrange
        var userId = 1;
        var userToDelete = new User { Id = userId, Username = "userToDelete", Email = "delete@example.com" };

        _userRepositoryMock.Setup(x => x.GetUserById(userId)).Returns(userToDelete);
        _userRepositoryMock.Setup(x => x.DeleteUser(userId)).Returns(userToDelete);

        // Act
        var result = _userService.DeleteUser(userId);

        // Assert
        Assert.NotNull(result);
        _userRepositoryMock.Verify(x => x.DeleteUser(userId), Times.Once);
    }
    [Fact]
    public void GetUserByEmail_ReturnsUserWhenFound()
    {
        // Arrange
        var email = "found@example.com";
        var expectedUser = new User { Id = 1, Username = "foundByEmail", Email = email };
    
        _userRepositoryMock.Setup(x => x.GetUserByEmail(email)).Returns(expectedUser);

        // Act
        var result = _userService.GetUserByEmail(email);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(expectedUser.Email, result.Email);
    }

    [Fact]
    public void UpdateUser_DoesNotUpdatePasswordWhenNull()
    {
        // Arrange
        var userDto = new UserDto { Username = "updatedUser" }; // Password not set
        var originalPasswordHash = new byte[] { 1, 2, 3 };
        var originalPasswordSalt = new byte[] { 4, 5, 6 };
        var userBeforeUpdate = new User 
        { 
            Id = 1, 
            Username = "originalUser", 
            Email = "original@example.com",
            HashPassword = originalPasswordHash,
            SaltPassword = originalPasswordSalt
        };

        _mapperMock.Setup(x => x.Map(userDto, userBeforeUpdate)).Returns((UserDto source, User destination) => 
        {
            destination.Username = source.Username;
            return destination;
        });

        _userRepositoryMock.Setup(x => x.UpdateUser(It.IsAny<User>())).Returns((User u) => u);

        // Act
        var result = _userService.UpdateUser(userBeforeUpdate, userDto);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(userDto.Username, result.Username);
        Assert.Equal(originalPasswordHash, result.HashPassword); // Make sure that the original password hash is unchanged
        Assert.Equal(originalPasswordSalt, result.SaltPassword); // make sure that the original password salt is unchanged
    }

}