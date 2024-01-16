using UnityEngine;
using System.Collections;
using Cinemachine;

internal sealed class CineMachineCameraShaker : MonoBehaviour
{
    CinemachineBasicMultiChannelPerlin vCamPerlinNoiseConfig;

    public static CineMachineCameraShaker Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null) 
        {
            Instance = this;
        }
        else
        {
            Destroy(Instance);
        }
    }

    private void Start()
    {
        CinemachineVirtualCamera vCam = GetComponent<CinemachineVirtualCamera>();
        vCamPerlinNoiseConfig = vCam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
    }

    public IEnumerator ShakeOnce(float amplitude,float time) 
    {
        vCamPerlinNoiseConfig.m_AmplitudeGain = amplitude;
        yield return new WaitForSeconds(time);
        vCamPerlinNoiseConfig.m_AmplitudeGain = 0;
    }
}
