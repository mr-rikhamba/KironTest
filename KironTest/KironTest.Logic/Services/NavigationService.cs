using System;
using System.Data;
using Dapper;
using KironTest.Logic.Contracts;
using KironTest.Logic.Helpers;
using KironTest.Logic.Models;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Logging;

namespace KironTest.Logic.Services;

public class NavigationService(ILogger<NavigationService> _logger, IRepositoryContract _repository) : INavigationContract
{

    public async Task<BaseResponseModel<List<NavigationTreeModel>>> GetNavigation()
    {
        try
        {
            var result = await _repository.Execute<NavigationModel>("SP_GetNavigations");
            List<NavigationTreeModel> treeData = MapOutTree(result);
            return new BaseResponseModel<List<NavigationTreeModel>>
            {
                ResponseData = treeData
            };
        }
        catch (SqlException ex)
        {
            _logger.LogError(ex, "SQL Exception.");
            return new BaseResponseModel<List<NavigationTreeModel>> { IsSuccessful = false, ResponseMessage = ex.Message };
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Unknown Error.");
            throw;
        }
    }

    private List<NavigationTreeModel> MapOutTree(List<NavigationModel> result, int parantId = -1)
    {
        List<NavigationTreeModel> tree = new();
        tree = result.Where(z => z.ParentID == parantId).Select(c => new NavigationTreeModel
        {
            Text = c.Text.Trim(),
            Children = MapOutTree(result, c.ID)
        }).ToList();
        return tree;
    }
}
