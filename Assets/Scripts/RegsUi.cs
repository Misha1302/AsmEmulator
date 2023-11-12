using TMPro;
using UnityEngine;

public sealed class RegsUi : MonoBehaviour
{
    [SerializeField] private TMP_Text[] registers;

    public void UpdateRegs(int[] regs)
    {
        for (var i = 0; i < regs.Length; i++)
            registers[i].text = regs[i].ToString();
    }

    public void Clear()
    {
        foreach (var t in registers)
            t.text = "0";
    }
}