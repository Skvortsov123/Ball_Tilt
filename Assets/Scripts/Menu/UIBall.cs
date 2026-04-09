using UnityEngine;

public class UIBall : MonoBehaviour
{

    public float speed = 1f;
    public Vector3 offset;

    private Rigidbody rb;
    private Vector3 lastVelocity;

    void Start()
    {


        rb = GetComponent<Rigidbody>();
        rb.useGravity = true;
        rb.interpolation = RigidbodyInterpolation.Interpolate;  // Makes ball render in higher (120) fps while physics has 50 tps, smooth graphics

        // Hitta canvasen som detta UI-element tillhör
        Canvas canvas = GetComponentInParent<Canvas>();
        parentRect = canvas.GetComponent<RectTransform>();
    }

    void FixedUpdate()
    {
        ApplyTiltMovement();
        Move();
        ClampToBounds();
        HandleTargetCollisions();
    }

    //  Tilt  acceleration
    void ApplyTiltMovement()
    {
 

        Vector3 tilt = Input.acceleration;
        rb.AddForce((new Vector3(tilt.y, tilt.z, -tilt.x) + offset) * speed * rb.mass);
       
    }

    //  Flytta bollen
    void Move()
    {
        rect.anchoredPosition += velocity * Time.deltaTime;
    }

    //  Studs mot kanter (panelens bounds)
    void ClampToBounds()
    {
        Vector3 screenPos = RectTransformUtility.WorldToScreenPoint(null, rect.position);

        float halfW = rect.rect.width / 2;
        float halfH = rect.rect.height / 2;

        // Höger
        if (screenPos.x > Screen.width - halfW)
        {
            screenPos.x = Screen.width - halfW;
            velocity.x = -Mathf.Abs(velocity.x) * bounceDamping;
        }

        // Vänster
        if (screenPos.x < halfW)
        {
            screenPos.x = halfW;
            velocity.x = Mathf.Abs(velocity.x) * bounceDamping;
        }

        // Topp
        if (screenPos.y > Screen.height - halfH)
        {
            screenPos.y = Screen.height - halfH;
            velocity.y = -Mathf.Abs(velocity.y) * bounceDamping;
        }

        // Botten
        if (screenPos.y < halfH)
        {
            screenPos.y = halfH;
            velocity.y = Mathf.Abs(velocity.y) * bounceDamping;
        }

        // Konvertera tillbaka till world/UI position
        rect.position = screenPos;
    }

    

 
}