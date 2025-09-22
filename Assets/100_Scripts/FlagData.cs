using UnityEngine;

[CreateAssetMenu(fileName = "New FlagData", menuName = "Scriptable Objects/Flags/FlagData")]
public class FlagData : ScriptableObject
{
    [SerializeField] bool isOn = false;

    public bool IsOn { get { return isOn; } }

    public void InitFlag()
    {
        isOn = false;
    }

    public void SetFlagStatus(bool value = true)
    {
        isOn = value;
    }

}
