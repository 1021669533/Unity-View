using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using UnityView.Animation;
using UnityView.Component;

namespace UnityView
{
    public delegate void OnClick(ButtonView button);
    public class ButtonView : UIView
    {
        public static ColorBlock AppearanceColorBlock = ColorBlock.defaultColorBlock;

        public readonly Button Button;

        private Text _titleTextComponent;
        public Text TitleTextComponent
        {
            set
            {
                _titleTextComponent = value;
            }
            get
            {
                if (_titleTextComponent != null) return _titleTextComponent;
                var text = TextView.BaseTextView();
                _titleTextComponent = text.GetComponent<Text>();
                text.transform.SetParent(RectTransform);
                RectFill(text.GetComponent<RectTransform>());
                return _titleTextComponent;
            }
        }

        public string Title
        {
            set
            {
                TitleTextComponent.text = value;
            }
            get
            {
                return TitleTextComponent.text;
            }
        }
        [Animatable]
        public Color TitleColor
        {
            set
            {
                if (UIAnimation.OnAnimate())
                {
                    UIAnimation.Append(new TextColorAction(TitleTextComponent, TitleTextComponent.color, value));
                }
                else
                {
                    TitleTextComponent.color = value;
                }
            }
            get
            {
                return TitleTextComponent.color;
            }
        }
        public float FontSize
        {
            set
            {
                TitleTextComponent.fontSize = Mathf.RoundToInt((value * UICanvas.FontCoefficient));
            }
            get
            {
                return TitleTextComponent.fontSize / UICanvas.FontCoefficient;
            }
        }

        public event OnClick OnClickListener;

        public void AddListener(UnityAction action)
        {
            OnClickListener += button => action();
        }

        public void OnClick()
        {
            if (OnClickListener != null) OnClickListener(this);
        }

        public ButtonView()
            : this(UIViewManager.GetInstance().RootView) { }

        public ButtonView(RectTransform transform) : base(transform)
        {
            Button = UIObject.AddComponent<Button>();
            Button.colors = AppearanceColorBlock;
            Button.onClick.AddListener(OnClick);
        }

        public ButtonView(UILayout layout)
            : base(layout)
        {
            Button = UIObject.AddComponent<Button>();
            Button.colors = AppearanceColorBlock;
            Button.onClick.AddListener(OnClick);
        }

        public ButtonView(GameObject gameObject)
            : base(gameObject)
        {
            Button = gameObject.GetComponent<Button>() ?? gameObject.AddComponent<Button>();
            Button.colors = AppearanceColorBlock;
            Button.onClick.AddListener(OnClick);
        }
    }

    public class RadioGroup
    {
        private readonly List<RadioButtonView> _buttons = new List<RadioButtonView>();

        public void AddRadioButton(RadioButtonView radioButton)
        {
            if (_buttons.Contains(radioButton)) return;
            _buttons.Add(radioButton);
            radioButton.RadioGroup = this;
        }
        public void RemoveRadioButton(RadioButtonView radioButton)
        {
            _buttons.Remove(radioButton);
            radioButton.RadioGroup = null;
        }

        public void Notify(RadioButtonView radioButton)
        {
            if (!_buttons.Contains(radioButton)) return;
            foreach (var radioButtonView in _buttons)
            {
                if (radioButtonView == radioButton)
                {
                    if (!radioButton.StateOn)
                    {
                        radioButton.StateOn = true;
                    }
                }
                else
                {
                    radioButtonView.StateOn = false;
                }
            }
        }

        public void Select(RadioButtonView radioButton)
        {
            if (!_buttons.Contains(radioButton)) return;
            if (radioButton.StateOn) return;
            foreach (var radioButtonView in _buttons)
            {
                if (radioButtonView == radioButton) continue;
                radioButtonView.StateOn = false;
            }
            radioButton.StateOn = true;
        }
    }
    public class RadioButtonView : ButtonView
    {
        private bool _stateOn;

        public Sprite ImageOn { set; get; }
        public Sprite ImageOff { set; get; }
        public string TitleOn { set; get; }
        public string TitleOff { set; get; }
        public bool StateOn
        {
            set
            {
                _stateOn = value;
                if (value)
                {
                    if (ImageOn != null)
                    {
                        Sprite = ImageOn;
                    }
                    if (TitleOn != null)
                    {
                        Title = TitleOn;
                    }
                    if (RadioGroup != null)
                    {
                        RadioGroup.Notify(this);
                    }
                }
                else
                {
                    if (ImageOff != null)
                    {
                        Sprite = ImageOff;
                    }
                    if (TitleOff != null)
                    {
                        Title = TitleOff;
                    }
                }
            }
            get
            {
                return _stateOn;
            }
        }

        public RadioGroup RadioGroup { set; get; }

        public bool Switch()
        {
            StateOn = !StateOn;
            return StateOn;
        }

        public RadioButtonView() { }
        public RadioButtonView(UILayout layout) : base(layout) { }
        public RadioButtonView(GameObject gameObject) : base(gameObject) { }
    }
}
