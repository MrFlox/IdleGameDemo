using UnityEngine;
using UnityEngine.Serialization;

namespace Modules.Utils
{
    public class FPSSettings : MonoBehaviour
    {
        [FormerlySerializedAs("targetFPS")] public int TargetFPS = 60;

        private void Awake()
        {
            QualitySettings.vSyncCount = 0;
            Application.targetFrameRate = TargetFPS;
        }
    }
}