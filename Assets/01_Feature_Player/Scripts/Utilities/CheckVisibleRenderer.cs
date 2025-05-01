using UnityEngine;

public class CheckVisibleRenderer : MonoBehaviour
{
    private bool isRendered = false;

    public bool GetRendererStatus()
    {
        return isRendered;
    }
    
    private void OnBecameVisible()
    {
        isRendered = true;
    }

    private void OnBecameInvisible()
    {
        isRendered = false;
    }
}
