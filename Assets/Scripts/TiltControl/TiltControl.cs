using UnityEngine;
using System.Collections;


public class TiltControl : MonoBehaviour
{
    public float speed = 1f;
    public Vector3 offset;
    public bool keyboardControl;

    private Vector2 moveInput;
    private Rigidbody rb;
    private Vector3 lastVelocity;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.useGravity = true;
        rb.interpolation = RigidbodyInterpolation.Interpolate;  // Makes ball render in higher (120) fps while physics has 50 tps, smooth graphics
    }

    void FixedUpdate()
    {
        Vector3 tilt = Input.acceleration;
        rb.AddForce((new Vector3(tilt.y, tilt.z, -tilt.x) + offset ) * speed * rb.mass);
        if (keyboardControl)
            keyboard();
        vibrate();
    }

    void keyboard()
    {
        Vector3 move = Vector3.zero;

        if (Input.GetKey(KeyCode.W)) move += Vector3.right;
        if (Input.GetKey(KeyCode.S)) move += Vector3.left;
        if (Input.GetKey(KeyCode.A)) move += Vector3.forward;
        if (Input.GetKey(KeyCode.D)) move += Vector3.back;

        rb.AddForce(move * speed/4);
    }

    void vibrate()
    {
        float deltaV = (rb.linearVelocity - lastVelocity).magnitude;
        lastVelocity = rb.linearVelocity;

        //#if UNITY_ANDROID && !UNITY_EDITOR
        if(deltaV > 1) {
            Vibration.Vibrate((int)deltaV*20);
        }
        //#endif

    }

}



public static class Vibration   // Works on android, copied from website
{

#if UNITY_ANDROID && !UNITY_EDITOR
    public static AndroidJavaClass unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
    public static AndroidJavaObject currentActivity = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");
    public static AndroidJavaObject vibrator = currentActivity.Call<AndroidJavaObject>("getSystemService", "vibrator");
#else
    public static AndroidJavaClass unityPlayer;
    public static AndroidJavaObject currentActivity;
    public static AndroidJavaObject vibrator;
#endif

    public static void Vibrate()
    {
        if (isAndroid())
            vibrator.Call("vibrate");
        else
            Handheld.Vibrate();
    }


    public static void Vibrate(long milliseconds)
    {
        if (isAndroid())
            vibrator.Call("vibrate", milliseconds);
        else
            Handheld.Vibrate();
    }

    public static void Vibrate(long[] pattern, int repeat)
    {
        if (isAndroid())
            vibrator.Call("vibrate", pattern, repeat);
        else
            Handheld.Vibrate();
    }

    public static bool HasVibrator()
    {
        return isAndroid();
    }

    public static void Cancel()
    {
        if (isAndroid())
            vibrator.Call("cancel");
    }

    private static bool isAndroid()
    {
#if UNITY_ANDROID && !UNITY_EDITOR
	return true;
#else
        return false;
#endif
    }
}