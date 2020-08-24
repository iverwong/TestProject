using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class CardFilters
{
    /// <summary>
    /// 筛选出Camp_Role的目标
    /// </summary>
    /// <param name="_object">筛选前目标列表</param>
    /// <returns>筛选后目标列表</returns>
    public static List<BaseInteractableObject> RoleFilter(List<BaseInteractableObject> _object, string[] _param)
    {
        List<BaseInteractableObject> result = new List<BaseInteractableObject>();
        foreach(BaseInteractableObject each in _object)
        {
            if (each.GetType().IsInstanceOfType(typeof(BattleArea_Object_Camp_Role)))
            {
                result.Add(each);
            }
        }
        return result;
    }
}
