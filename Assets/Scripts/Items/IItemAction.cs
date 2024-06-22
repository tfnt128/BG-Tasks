using System.Collections.Generic;
using UnityEngine;

public interface IItemAction
{
    public string ActionName { get;}
    bool PerformAction(GameObject character);
}
