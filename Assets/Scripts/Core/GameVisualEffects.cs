using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Core
{
    public class GameVisualEffects : MonoBehaviour
    {
        [SerializeField] private Graphic _fadeImage;
        [SerializeField] private TMP_Text _fadeText;
        private RoomConfig _roomConfig;

        public void Construct(GameCycle gameCycle, RoomConfig roomConfig)
        {
            _roomConfig = roomConfig;
            gameCycle.Started += PlayBegin;
            gameCycle.Ended += PlayEnded;
        }

        private void PlayBegin()
        {
            var endColor = new Color(0, 0, 0, .2f);
            StartFade(_fadeImage, Color.black, endColor, 1f, false);
        }

        private void PlayEnded()
        {
            StartFade(_fadeImage, _fadeImage.color, Color.white, 5f, true);
            StartFade(_fadeText, _fadeImage.color, Color.black, 5f, false);

            _fadeText.text = _roomConfig.FinalMessage;
        }

        private void StartFade(Graphic graphic, Color from, Color to, float duration,
            bool enableRayCastTarget)
        {
            StartCoroutine(FadeRoutine(graphic, from, to, duration, enableRayCastTarget));
        }

        private IEnumerator FadeRoutine(Graphic graphic, Color from, Color to, float duration,
            bool enableRayCastTarget)
        {
            graphic.raycastTarget = enableRayCastTarget;
            float t = 0f;
            while (t < 1f)
            {
                t += Time.deltaTime / duration;
                graphic.color = Color.Lerp(from, to, t);
                yield return null;
            }

            graphic.color = to;
        }
    }
}