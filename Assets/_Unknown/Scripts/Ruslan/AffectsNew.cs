using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.SceneManagement;

namespace Unknown.Ruslan
{
    public class AffectsNew : MonoBehaviour
    {
        public PostProcessVolume postProcessVolume;
        private AutoExposure autoExposure;
        public float increaseStep = 0.1f;
        public float dicreaseStep = 0.1f;
        public float maxLimit = 2.0f;
        public float minLimit = 0.1f;
        public float interval = 1.0f;
        public float getsblack;


        private Vignette gettingblack;

        float seconds = 0;

        // Start is called before the first frame update
        void Start()
        {

        
            
            if (postProcessVolume.profile.TryGetSettings(out gettingblack))
            {



            }


            if (postProcessVolume.profile.TryGetSettings(out autoExposure))
            {

                InvokeRepeating(nameof(IncreaseMinExposure), interval * 5, interval);

            }


            if (postProcessVolume.profile.TryGetSettings(out autoExposure))
            {

                InvokeRepeating(nameof(DireaseMinExposure), interval, interval);

            }
        }

        // Update is called once per frame
        void Update()
        {

            seconds += Time.deltaTime;
            gettingblack.intensity.value = getsblack;
        


        }

        private void IncreaseMinExposure()

        {

            if (autoExposure != null && seconds > 5)

            {

                increaseStep = 1;
                autoExposure.minLuminance.value = Mathf.Min(autoExposure.minLuminance.value + increaseStep, maxLimit);



            }

            
        }


        private void DireaseMinExposure()

        {
            if (autoExposure != null && autoExposure.minLuminance.value == maxLimit)

            {

                getsblack += 0.4f;
                autoExposure.minLuminance.value = Mathf.Min(autoExposure.minLuminance.value - dicreaseStep, minLimit);

                if (autoExposure.minLuminance.value == 0.1f)
                {

                    increaseStep = 0;
                    seconds = 0;

                }
            }
        }


    }
}
