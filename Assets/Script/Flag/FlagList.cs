using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "New FlagList", menuName = "Scriptable Objects/Flags/FlagList")]
public class FlagList : ScriptableObject
{
    [SerializeField]
    List<FlagData> flags = new();

    public List<FlagData> Flags { get { return flags; } }

    public void InitFlags()
    {
        foreach(FlagData f in flags)
        {
            f.InitFlag();
        }
    }

    public void SetFlag(FlagData flag, bool value = true)
    {
        foreach(FlagData f in flags)
        {
            if(f == flag)
            {
                f.SetFlagStatus(value);
                return;
            }
        }
    }

    public bool GetFlagStatus(FlagData flag)
    {
        foreach (FlagData f in flags)
        {
            if (f == flag)
            {
                return f.IsOn;
            }
        }

        return false;
    }
}
