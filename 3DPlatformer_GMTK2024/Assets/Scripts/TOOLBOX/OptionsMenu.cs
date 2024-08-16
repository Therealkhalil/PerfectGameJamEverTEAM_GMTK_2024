using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;
using Cinemachine; 
public class OptionsMenu : MonoBehaviour, IDataPersistence
{

    public GameObject Sensitivity, MasterVol, AmbientVol, SFX, Music, Dialogue,MotionBlur , AimLock , ADSSensitivity, SniperSensitivity, FOV;
    public Volume Volume;

    public void ApplyChanges()
    {
        MotionBlur tmp;
        if (Volume.profile.TryGet<MotionBlur>(out tmp))
            tmp.active= MotionBlur.GetComponentInChildren<Toggle>().isOn;

        LensDistortion tmp2;
        if (Volume.profile.TryGet<LensDistortion>(out tmp2))
            tmp2.active = MotionBlur.GetComponentInChildren<Toggle>().isOn;

        //Camera.SetBaseZoom(40+ 30*FOV.GetComponentInChildren<Slider>().value);
        
    }

    public void LoadData(GameData data)
    {
        Debug.Log("tried to load Options menu");
    }

    public void SaveData(GameData data)
    {
        Debug.Log("Tried to save Options menu");
    }
}
 