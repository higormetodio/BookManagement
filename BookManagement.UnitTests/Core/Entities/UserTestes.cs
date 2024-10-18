using System.Data;
using System.Xml.Linq;
using System;
using BookManagement.Core.Entities;

namespace BookManagement.UnitTests.Core.Entities;
public class UserTestes
{
    [Fact]
    public void Should_Create_New_User_With_Corret_Properties()
    {
        //Arrange
        var name = "Antônio Albuquerque";
        var email = "antonio@gmail.com";
        var password = "123456789";
        var role = "user";
        var birthDate = new DateTime(1979, 12, 4);

        //Act
        var user = new User(name, email, password, role, birthDate);

        //Assert
        Assert.Equal(name, user.Name);
        Assert.Equal(email, user.Email);
        Assert.Equal(password, user.Password);
        Assert.Equal(role, user.Role);
        Assert.Equal(birthDate, user.BirthDate);
        Assert.Null(user.Loans);
    }

    [Fact]
    public void Should_Update_User_With_New_Data()
    {
        //Arrange
        var name = "Antônio Albuquerque";
        var email = "antonio@gmail.com";
        var password = "123456789";
        var role = "user";
        var birthDate = new DateTime(1979, 12, 4);

        var newName = "Antônio Albuquerque";
        var newEmail = "antonio@gmail.com";
        var newPassword = "123456789";
        var newBirthDate = new DateTime(1979, 12, 4);

        var user = new User(name, email, password, role, birthDate);

        //Act
        user.Update(newName, newEmail, newBirthDate, newPassword);

        //Assert
        Assert.Equal(newName, user.Name);
        Assert.Equal(newEmail, user.Email);
        Assert.Equal(newPassword, user.Password);
        Assert.Equal(newBirthDate, user.BirthDate);
    }

    [Fact]
    public void Should_Update_User_Only_Activete_Propertie()
    {
        //Arrange
        var name = "Antônio Albuquerque";
        var email = "antonio@gmail.com";
        var password = "123456789";
        var role = "user";
        var birthDate = new DateTime(1979, 12, 4);

        var user = new User(name, email, password, role, birthDate);

        var active = false;

        //Act
        user.ToActive(active);

        //Assert
        Assert.False(user.Active);
    }
}
