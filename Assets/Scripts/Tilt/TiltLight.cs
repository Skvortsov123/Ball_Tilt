using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;

public class TiltLight : MonoBehaviour
{
    public Vector3 offset;
    public float speed;
    public float amplitude;
    private Light light;

    void Start()
    {
        light = GetComponent<Light>();
        light.color = Color.white;
    }

    void Update()
    {
        Vector3 tilt = Input.acceleration.normalized;
        print(tilt);
        Quaternion target = Quaternion.Euler(new Vector3(tilt.x, tilt.y, tilt.z) * amplitude + offset);
        light.transform.rotation = Quaternion.Lerp(light.transform.rotation, target, Time.deltaTime * speed);
    }
}
