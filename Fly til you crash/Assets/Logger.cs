using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Logger : MonoBehaviour
{
    static Logger logger;
    public AnimationCurve ac;
    List<Keyframe> keys;

    // Start is called before the first frame update
    void Start()
    {
        keys = new List<Keyframe>();
        ac = new AnimationCurve(keys.ToArray());

        if (logger is null)
            logger = this;
        else
            Destroy(this);
    }

    public static void Log(float value){logger.keys.Add(new Keyframe(Time.time, value));}
    public static void Log(bool value) { logger.keys.Add(new Keyframe(Time.time, value ? 1 : 0));}

    void Update(){ac.keys = keys.ToArray();}

}
