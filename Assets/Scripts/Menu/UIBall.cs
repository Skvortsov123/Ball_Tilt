
using UnityEngine;
using System.Collections.Generic;

public class UIBall : MonoBehaviour
{
    [Header("Rörelse")]
    public float speed = 500f;          // Hur stark tilt-kraften är
    public float damping = 0.98f;       // Friktion (lägre = mer glid)
    public float bounceDamping = 0.8f;  // Studs mot väggar
    public Vector2 offset;              // Justering av tilt-riktning

    [Header("Kollision")]
    public float radius = 50f;          // Radie för kollision (pixlar)

    private RectTransform rect;         // UI-transform
    private RectTransform parentRect;   // Canvasens area

    private Vector2 velocity;           // Nuvarande hastighet

    // Lista på alla bollar (för kollision mellan dem)
    private static List<UIBall> allBalls = new List<UIBall>();

    void Awake()
    {
        // Lägg till denna boll i listan
        allBalls.Add(this);
    }

    void OnDestroy()
    {
        // Ta bort om objektet förstörs
        allBalls.Remove(this);
    }

    void Start()
    {
        // Hämta referenser
        rect = GetComponent<RectTransform>();
        parentRect = GetComponentInParent<Canvas>().GetComponent<RectTransform>();
    }

    void Update()
    {
        ApplyTilt();        // Applicera mobilens tilt
        Move();             // Flytta bollen
        HandleCollisions(); // Krock med andra bollar
        ClampToBounds();    // Håll inom skärmen
    }

    //  Läs av telefonens tilt och gör om till acceleration
    void ApplyTilt()
    {
        Vector3 tilt = Input.acceleration;

        // Konvertera till 2D (UI-plan)
        Vector2 force = new Vector2(tilt.x, -tilt.y) + offset;

        // Lägg till kraft på hastigheten
        velocity += force * speed * Time.deltaTime;

        // Dämpning = "friktion"
        velocity *= damping;
    }

    //  Flytta UI-elementet
    void Move()
    {
        rect.anchoredPosition += velocity * Time.deltaTime;
    }

    //  Hantera kollision mellan bollar
    void HandleCollisions()
    {
        foreach (var other in allBalls)
        {
            if (other == this) continue;

            Vector2 posA = rect.anchoredPosition;
            Vector2 posB = other.rect.anchoredPosition;

            Vector2 diff = posA - posB;
            float dist = diff.magnitude;

            float minDist = radius + other.radius;

            // Om de överlappar
            if (dist < minDist && dist > 0f)
            {
                Vector2 normal = diff.normalized;

                // Separera bollarna (push bort varandra)
                float overlap = minDist - dist;
                rect.anchoredPosition += normal * (overlap * 0.5f);
                other.rect.anchoredPosition -= normal * (overlap * 0.5f);

                // Enkel "studs" (byt hastighet längs normal)
                float relativeVelocity = Vector2.Dot(velocity - other.velocity, normal);

                if (relativeVelocity < 0f)
                {
                    float bounce = 0.8f;

                    Vector2 impulse = normal * relativeVelocity * bounce;

                    velocity -= impulse;
                    other.velocity += impulse;
                }
            }
        }
    }

    //  Håll bollen inom canvasens gränser
    void ClampToBounds()
    {
        Vector2 screenPos = RectTransformUtility.WorldToScreenPoint(null, rect.position);

        float halfW = rect.rect.width / 2;
        float halfH = rect.rect.height / 2;

        // Höger
        if (screenPos.x > Screen.width - halfW)
        {
            screenPos.x = Screen.width - halfW;
            velocity.x *= -bounceDamping;
        }
        // Vänster
        else if (screenPos.x < halfW)
        {
            screenPos.x = halfW;
            velocity.x *= -bounceDamping;
        }

        // Topp
        if (screenPos.y > Screen.height - halfH)
        {
            screenPos.y = Screen.height - halfH;
            velocity.y *= -bounceDamping;
        }
        // Botten
        else if (screenPos.y < halfH)
        {
            screenPos.y = halfH;
            velocity.y *= -bounceDamping;
        }

        // Tillbaka till UI position
        Vector2 worldPos;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            parentRect,
            screenPos,
            null,
            out worldPos
        );

        rect.anchoredPosition = worldPos;
    }
}

