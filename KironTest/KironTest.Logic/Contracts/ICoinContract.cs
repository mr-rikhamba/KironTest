using System;
using KironTest.Logic.Models;

namespace KironTest.Logic.Contracts;

public interface ICoinContract
{
    Task<CoinModel> GetCoins();
}
