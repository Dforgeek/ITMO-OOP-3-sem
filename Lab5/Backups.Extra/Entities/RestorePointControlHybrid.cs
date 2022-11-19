﻿using Backups.Entities;
using Backups.Extra.Interfaces;
using Backups.Extra.Models;

namespace Backups.Extra.Entities;

public class RestorePointControlHybrid : IRestorePointControl
{
    private List<IRestorePointControl> _restorePointControls;
    private HybridControlOption _controlOption;

    public RestorePointControlHybrid(List<IRestorePointControl> restorePointControls, HybridControlOption controlOption)
    {
        if (restorePointControls.Count == 0)
            throw new Exception();
        _restorePointControls = restorePointControls;
        _controlOption = controlOption;
    }

    public List<RestorePoint> GetRestorePointsToExclude(IReadOnlyCollection<RestorePoint> restorePoints)
    {
        switch (_controlOption)
        {
            case HybridControlOption.AllCriteria:
            {
                var listsOfRestorePoints = _restorePointControls
                    .Select(restorePointControl => restorePointControl
                        .GetRestorePointsToExclude(restorePoints)).ToList();
                List<RestorePoint> result = IntersectAll(listsOfRestorePoints);
                if (result.Count == restorePoints.Count)
                    throw new Exception();
                return result;
            }

            case HybridControlOption.AtLeastOneCriteria:
            {
                var result = new List<RestorePoint>();
                foreach (IRestorePointControl restorePointControl in _restorePointControls)
                {
                    result.AddRange(restorePointControl.GetRestorePointsToExclude(restorePoints));
                }

                if (result.Count == restorePoints.Count)
                    throw new Exception();
                return result;
            }

            default:
                throw new Exception();
        }
    }

    private List<T> IntersectAll<T>(List<List<T>> lists)
    {
        HashSet<T>? hashSet = null;
        foreach (IEnumerable<T> list in lists)
        {
            if (hashSet == null)
            {
                hashSet = new HashSet<T>(list);
            }
            else
            {
                hashSet.IntersectWith(list);
            }
        }

        return hashSet == null ? new List<T>() : hashSet.ToList();
    }
}