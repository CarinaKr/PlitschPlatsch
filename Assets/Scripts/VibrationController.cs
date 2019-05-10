using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//https://gist.github.com/munkbusiness/9e0a7d41bb9c0eb229fd8f2313941564
//https://developer.android.com/reference/android/os/Vibrator.html
//https://developer.android.com/reference/android/os/VibrationEffect.html

public class VibrationController : MonoBehaviour
{

    private static AndroidJavaObject vibrator;
    private static AndroidJavaClass vibrationEffect;
    

    // Start is called before the first frame update
    void Start()
    {
        if(UsingAndroid())
        {
            vibrator = new AndroidJavaClass("com.unity3d.player.UnityPlayer").GetStatic<AndroidJavaObject>("currentActivity")// Get the Current Activity from the Unity Player.
                .Call<AndroidJavaObject>("getSystemService", "vibrator");// Then get the Vibration Service from 

            if(UsedAPI()>=26)
            {
                vibrationEffect = new AndroidJavaClass("android.os.VibrationEffect");
            }
        }      
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private static int UsedAPI()
    {
        if (UsingAndroid())
        {
            using (var version = new AndroidJavaClass("android.os.Build$VERSION"))
            {
                return version.GetStatic<int>("SDK_INT");
            }
        }
        else
        {
            return -1;
        }
    }

    private static bool UsingAndroid()
    {
#if UNITY_ANDROID && !UNITY_EDITOR
	    return true;
#else
        return false;
#endif
    }
}
