
using UnityEngine.Events;

public class CollectableProp : ObjectInteraction
{
    public UnityEvent onUse;
    public override void Use()
    {
        base.Use();
        onUse.Invoke();
    }
}
