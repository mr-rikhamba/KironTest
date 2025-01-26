using System;
using KironTest.Logic.Models;

namespace KironTest.Logic.Contracts;

public interface INavigationContract
{
    Task<BaseResponseModel<List<NavigationTreeModel>>> GetNavigation();
}
