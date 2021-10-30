using System.Collections;
using TMPro;
using UnityEngine;

namespace Template.UI.Text
{
    public abstract class BaseTypingText : MonoBehaviour
    {
        [SerializeField] protected TMP_Text text;
        [SerializeField] protected float typingDelay = 0.05f;
        protected string typingStr;

#if UNITY_EDITOR
        protected void OnValidate()
        {
            if (!text)
                text = GetComponent<TMP_Text>();
            if (!text) Debug.LogError("Cant find TMP_Text", this);
        }
#endif

        public virtual void Init(string str)
        {
            Reset();
            typingStr = str;
        }

        public abstract void Launch();

        public virtual void Break()
        {            
            text.text = typingStr;            
        }

        protected virtual void Reset()
        {
            text.text = string.Empty;
        }
        protected IEnumerator StartTypingText()
        {
            int charInd = 0;
            while (text.text != typingStr)
            {
                text.text += typingStr[charInd];
                charInd++;
                yield return new WaitForSeconds(typingDelay);
            }
            Break();
        }

    }
}