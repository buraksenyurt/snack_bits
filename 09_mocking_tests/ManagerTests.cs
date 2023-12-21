using KanbanWorld;
using Moq;

namespace _09_mocking_tests;

public class ManagerTests
{
    [Fact]
    public void GetCount_Returns_3_Test()
    {
        var mockFileLoader = new Mock<IFileLoader>();
        mockFileLoader.Setup(m => m.LoadLines(It.IsAny<string>())).Returns(new List<string>{
           "1|Haftasonu unit test konusuna bak|50|M|false",
           "2|Denizler Altında Yirmi Bin Fersah kitabının özetini çıkart|90|XL|false",
           "3|5 Km yürüyüş yap|100|S|true",
        });

        var manager = new Manager(mockFileLoader.Object);
        var actual = manager.GetCount();
        Assert.Equal(3, actual);
    }
}