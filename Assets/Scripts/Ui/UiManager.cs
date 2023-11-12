using UnityEngine;

[RequireComponent(typeof(RamUi))]
[RequireComponent(typeof(RegsUi))]
[RequireComponent(typeof(InOutUi))]
[RequireComponent(typeof(BlinkUi))]
public sealed class UiManager : MonoBehaviour
{
    public RamUi RamUi { get; private set; }
    public RegsUi RegsUi { get; private set; }
    public InOutUi InOutUi { get; private set; }
    public BlinkUi BlinkUi { get; private set; }


    private void Awake()
    {
        RamUi = GetComponent<RamUi>();
        RegsUi = GetComponent<RegsUi>();
        InOutUi = GetComponent<InOutUi>();
        BlinkUi = GetComponent<BlinkUi>();
    }
}