using UnityEngine;
using DG.Tweening;

public class BoxMesh : MonoBehaviour
{
    [SerializeField] float RotationSpeed    = 10f;
    [SerializeField] float ScaleFactor      = 0.5f;
    [SerializeField] InputController Inputs = null;
 
    private InteractionPoint[] Points = null;
    
    private void Start() => Points = GetComponentsInChildren<InteractionPoint>();

    private void Update() => HandleRotation();

    private void HandleRotation()
    {
        transform.RotateAround(transform.position, Vector3.up   , -Inputs.MoveDeltaX * RotationSpeed);
        transform.RotateAround(transform.position, Vector3.right, Inputs.MoveDeltaY  * RotationSpeed);
    }//HandleRotation() end
    
    public void Resize(AxisType axisType)
    {
        switch(axisType)
        {
            case AxisType.X_Positive:
                MoveAndScale(ScaleFactor, transform.right, Vector3.right);
            break;
            case AxisType.X_Negative:
                MoveAndScale(-ScaleFactor, transform.right, Vector3.right);
            break;
            case AxisType.Y_Positive:
                MoveAndScale(ScaleFactor, transform.up, Vector3.up);
            break;
            case AxisType.Y_Negative:
                MoveAndScale(-ScaleFactor, transform.up, Vector3.up);
            break;
            case AxisType.Z_Positive:
                MoveAndScale(ScaleFactor, transform.forward, Vector3.forward);
            break;
            case AxisType.Z_Negative:
                MoveAndScale(-ScaleFactor, transform.forward, Vector3.forward);
            break;
        }//switch end
    }//Resize() end

    private void MoveAndScale(float amount, Vector3 movedirection, Vector3 scaleDirection)
    {
        transform.DOLocalMove((transform.localPosition + (movedirection * (amount / 2f))), 0.25f).SetEase(Ease.Flash);
        transform.DOScale((transform.localScale + (scaleDirection * Mathf.Abs(amount))), 0.25f).SetEase(Ease.Flash).OnComplete(()=>
        {
            foreach(InteractionPoint interactionPoint in Points)
                interactionPoint.ResetScale();
        });//Tween end
    }//MoveAndScale() end

}//class end