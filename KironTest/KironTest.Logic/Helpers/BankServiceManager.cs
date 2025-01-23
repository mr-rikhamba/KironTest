using System;

namespace KironTest.Logic.Helpers;

public class BankServiceManager
{
    public bool IsServiceEnabled { get; set; }

    public void EnableService() => IsServiceEnabled = true;
    public void DisableService() => IsServiceEnabled = false;
}
