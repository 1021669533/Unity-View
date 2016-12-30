using UnityEngine;
using UnityView;
using UnityView.Component;

namespace Assets.Scripts
{
    public class Part1 : UIView
    {
        public ButtonView ButtonView;

        public TextView TextView;

        public TextField TextField;

        public Part1()
        {
            UIRect = UICreator.MainRect;
            BackgroundColor = Color.grey;

            ButtonView = new ButtonView(this);
            ButtonView.UIRect = new UIRect(20, 20, 300, 50);
            ButtonView.BackgroundColor = Color.black;
            ButtonView.FontSize = 25;
            ButtonView.Title = "Button(Make Toast)";
            ButtonView.AddListener(MakeToast);

            TextView = new TextView(this);
            TextView.UIRect = new UIRect(20, 90, 300, 50);
            TextView.FontSize = 25;
            TextView.Text = "Text View";
            TextView.Alignment = TextAnchor.MiddleCenter;

            TextField = new TextField(this);
            TextField.UIRect = new UIRect(20, 160, 300, 50);
            TextField.BackgroundColor = Color.black;
            TextField.FontSize = 25;
            TextField.PlaceHolder = "Input";
            TextField.Aligment = TextAnchor.MiddleLeft;
        }

        public void MakeToast()
        {
            UIToast.MakeToast("Make Toast");
        }
    }
}
