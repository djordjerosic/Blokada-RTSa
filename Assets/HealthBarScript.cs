using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Image fillImage;
    [SerializeField] private Vector3 offset = new Vector3(0, 1.5f, 0); 

    private Transform target;

    public void Initialize(Transform target)
    {
        this.target = target;
    }

    public void SetHealth(float current, float max)
    {
        float fill = Mathf.Clamp01(current / max);
        fillImage.fillAmount = fill;
    }

    void LateUpdate()
    {
        if (target != null)
        {
            transform.position = target.position + offset;
            transform.LookAt(Camera.main.transform); 
        }
    }
}