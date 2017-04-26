using UnityEngine;
using System.Collections;

public class RoleInfoForRank
{
    public string roleId { get; private set; }
    public string roleLevel { get; private set; }
    public string roleColor { get; private set; }
    public string roleJunXian { get; private set; }

    public RoleInfoForRank(string roleId, string roleLevel, string roleColor, string roleJunXian)
    {
        this.roleId = roleId;
        this.roleLevel = roleLevel;
        this.roleColor = roleColor;
        this.roleJunXian = roleJunXian;
    }
}