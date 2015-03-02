using UnityEngine;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;


namespace Ui
{

    public class TransitionBackground : Core.MonoBehaviourGod
    {
        private Image Image { get; set; }

        protected override void Awake()
        {
            Image = GetComponent<Image>();
        }

        public void SetParent(RectTransform transform)
        {
            var rectTr = this.transform as RectTransform;

            rectTr.SetParent(transform);

            rectTr.offsetMin = Vector2.zero;
            rectTr.offsetMax = Vector2.zero;
            rectTr.localScale = Vector3.one;
        }

       
        public IEnumerator FadeCoroutine(bool toDark, float duration)
        {
            float startTime = Time.time;

            float t = 0;

            while (true)
            {
                t = Mathf.Clamp01((Time.time - startTime) / duration);

                var color = Image.color;
                color.a = toDark ? t : 1 - t;

                Core.Dbg.Log("Color " + color.a);

                Image.color = color;

                if (t < 1)
                    yield return 0;
                else
                    break;
            }
        }

    }

}