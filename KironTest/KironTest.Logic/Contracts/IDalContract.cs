using KironTest.Logic.Models;

namespace KironTest.Logic.Contracts;

public interface IDalContract
{
    Task<List<NavigationModel>> GetNavigation();
}
