using UnityEngine;
using System.Collections;



public class TiltControl : MonoBehaviour
{
    public float forceLimit;
    public Vector3 offset; //only needed if manual numeric offset?
    public bool enableVibration = true; //styr om denna boll ska trigga vibration (undviker dubbel vibration ifall enemyball fins)

    private Joystick joystick;
    private SliderControl slider; //Two slider control, horizon/ vectical
    private Rigidbody rb;
    //private Vector3 control;    //Control of tilt, can be tilt, WASD, Joystick
    private Vector3 lastVelocity;

    // Sensitivity & Deadzone
    //[Header("Tilt Settings")]
    private float sensitivity = 1f;   // Multiplier for tilt strength


    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.interpolation = RigidbodyInterpolation.Interpolate;  // Makes ball render in higher (120) fps while physics has 50 tps, smooth graphics
        joystick = FindFirstObjectByType<Joystick>();

        offset = GameSettings.calibrationOffset;
        sensitivity = GameSettings.sensitivity;

        if (SystemInfo.supportsGyroscope)
        {
            Input.gyro.enabled = true;
        }
    }

    void FixedUpdate()
    {
        rb.AddForce(Vector3.ClampMagnitude(getControl() * forceLimit, forceLimit), ForceMode.Acceleration);
        chechImpactVibration();
    }

    void chechImpactVibration()
    {
        float deltaV = (rb.linearVelocity - lastVelocity).magnitude;
        lastVelocity = rb.linearVelocity;

        if (deltaV > 1 && enableVibration)
        {
            //Vibration.Vibrate(Mathf.Clamp((int)deltaV * 20, 0, 500));
            Vibration.Vibrate(1);
        }
    }
    public Vector3 getLastVelocity()
    {
        return lastVelocity;
    }

    public Vector3 getControl() //Can be Tilt, WASD, Joystick, slider
    {
        Vector3 control = Vector3.zero;

        switch (GameSettings.controlMode) //  v�lj input-l�ge  tilt , joystick eller slider
        {
            case ControlMode.Tilt:

                Vector3 g = (Input.gyro.gravity - GameSettings.calibrationOffset) * Mathf.Pow(sensitivity, 2);
                control = new Vector3(g.y, 0, -g.x);

                if (control.magnitude > GameSettings.deadZone)
                {
                    float force = (control.magnitude - GameSettings.deadZone) / (1f - GameSettings.deadZone);
                    control = control.normalized * force;    // dir * force
                }
                else
                {
                    control = Vector3.zero;
                }

                break;

            case ControlMode.Joystick:
                if (joystick != null)
                {
                    control = new Vector3(joystick.getPosition().y, 0, -joystick.getPosition().x);
                }
                break;

            case ControlMode.Slider:
                if (slider != null)
                {
                    control = new Vector3(slider.GetY(), 0, -slider.GetX());
                }
                break;
        }

        control += Keyboard.getWASD();  //Keyboard

        return control;
    }

    //Sensitivity slider hook
    public void SetSensitivity(float value)
    {
        sensitivity = value;
        GameSettings.sensitivity = value;

    }

    public void SetJoystick(Joystick joystick)
    {
        this.joystick = joystick;
    }

    public void SetSliderControl(SliderControl slider)
    {
        this.slider = slider;
    }

}

public static class Keyboard
{
    public static Vector3 getWASD()
    {
        Vector3 move = Vector3.zero;

        if (Input.GetKey(KeyCode.W)) move += Vector3.right;
        if (Input.GetKey(KeyCode.S)) move += Vector3.left;
        if (Input.GetKey(KeyCode.A)) move += Vector3.forward;
        if (Input.GetKey(KeyCode.D)) move += Vector3.back;

        return move;
    }
}

public static class Vibration   // Vibration on android, copied from website
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