using UnityEngine;

public enum AxisType
{
    X_Positive,
    X_Negative,
    Y_Positive,
    Y_Negative,
    Z_Positive,
    Z_Negative
};//enum end

public class InteractionPoint : MonoBehaviour, IInteract
{
    [SerializeField] AxisType axisType;
    
    private BoxMesh _mesh = null;
    private Vector3 _scaleCache;
    
    private void Start()
    {
        _mesh = GetComponentInParent<BoxMesh>();
        _scaleCache = transform.localScale;
    }//Start() end

    public void Interact() => _mesh.Resize(axisType);

    public void ResetScale()
    {
        Transform parent = transform.parent;
        transform.SetParent(null);
        transform.localScale = _scaleCache;
        transform.SetParent(parent);
    }//ResetScale() end

}//class end