using System;

namespace KironTest.Logic.Models;

public class NavigationModel
{
    public int ID { get; set; }
    public required string Text { get; set; }
    public int ParentID { get; set; }

}

public class NavigationTreeModel
{
    public string Text { get; set; }
    public List<NavigationTreeModel> Children { get; set; }
}