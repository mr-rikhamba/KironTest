using Dapper;
using KironTest.Logic.Contracts;
using KironTest.Logic.Helpers;
using KironTest.Logic.Models;

namespace KironTest.Logic.Services;
public class DalService(ConnectionManager _connectionMgr, ICacheContract _cacheSvc) : IDalContract
{
    public async Task<List<NavigationModel>> GetNavigation()
    {
        try
        {
            List<NavigationModel> result;
            var cached = _cacheSvc.Get<List<NavigationModel>>("nav");
            if (cached.HasData)
            {
                return cached.Data;
            }
            _connectionMgr.CloseAndDiscard();
            var query = "SELECT * FROM Navigation";
            var activeConnection = _connectionMgr.GetConnection();
            result = (await activeConnection.QueryAsync<NavigationModel>(query)).ToList();
            _cacheSvc.Set<List<NavigationModel>>("nav", result);
            return result;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return null;
        }
        finally
        {
            _connectionMgr.CloseAndDiscard();
        }
    }
}
