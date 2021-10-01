using System;
using System.Collections;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    /// <summary>
    /// Sets the alpha value to all images in object to 'Standard' value.
    /// On Action sets the alpha value to 'Action' value
    /// and change this to 'Standard' value with 'AlphaStep'
    /// in every Frame with 'Delay' after 'StartDelay'
    /// <n>Standard alpha set from Editor. One for all images.</n>  
    /// <n>Action alpha for every image get from this image.</n>
    /// </summary>
    public class ChangeAlphaOfAllImages : MonoBehaviour
    {
        [SerializeField] private float alphaStep;
        [SerializeField] private float alphaStandard;
        
        
        [Header("Coroutine properties")]
        [SerializeField] private float delay;
        [SerializeField] private float delayBefore;

        private ImageData[] _imagesAlphaMap;
        private bool _coroutineIsWork;

        private void Awake()
        {
            var images = GetComponentsInChildren<Image>();
            _imagesAlphaMap = new ImageData[images.Length];
            
            for (int i = 0; i < images.Length; i++)
            {
                _imagesAlphaMap[i] = new ImageData(images[i], images[i].color.a);
                var color = images[i].color;
                color.a = 0;
                images[i].color = color;
            }

            _coroutineIsWork = false;
        }

        public void Action()
        {
            foreach (var imageData in _imagesAlphaMap)
            {
                var color = imageData.Image.color;
                color.a = imageData.Alpha;

                imageData.Image.color = color;
            }
            
            if(_coroutineIsWork) StopCoroutine(Fading());
            StartCoroutine(Fading());
        }

        private IEnumerator Fading()
        {
            yield return new WaitForSeconds(delayBefore);
            
            var maxAlpha = _imagesAlphaMap.Select(
                imageData => Math.Abs(imageData.Alpha - alphaStandard)
                ).Max();
            _coroutineIsWork = true;

            while (true)
            {
                foreach (var imageData in _imagesAlphaMap)
                {
                    var color = imageData.Image.color;
                    
                    if (color.a == alphaStandard) continue;
                
                    if (color.a > alphaStandard)
                    {
                        color.a = color.a - alphaStep < alphaStandard ? alphaStandard : color.a - alphaStep;
                    }
                    else
                    {
                        color.a = color.a + alphaStep > alphaStandard ? alphaStandard : color.a + alphaStep;
                    }
                    imageData.Image.color = color;
                }

                maxAlpha -= alphaStep;
                if (maxAlpha <= 0)
                {
                    _coroutineIsWork = false;
                    yield break;
                }
                else
                {
                    yield return new WaitForSeconds(delay);
                }
            }
        }

        private readonly struct ImageData
        {
            public readonly Image Image;
            public readonly float Alpha;

            public ImageData(Image image, float alpha)
            {
                Image = image;
                Alpha = alpha;
            }
        }
    }
}