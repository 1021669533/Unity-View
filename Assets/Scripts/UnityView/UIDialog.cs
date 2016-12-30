using UnityEngine;
using UnityEngine.Events;
using UnityView.Component;

namespace UnityView
{
    public class UIDialog : AbsDialog
    {
        public static void ShowDialog(string title)
        {

        }

        public static void ShowDialog(string title, string pTitle)
        {

        }

        public static void ShowDialog(string title, string pTitle, UnityAction pAction)
        {

        }
        public static void ShowDialog(string title, string pTitle, UnityAction pAction, string nTitle, UnityAction nAction)
        {

        }

        public static float AppearanceButtonHeight = 24f;
        public static float AppearanceMargin = 16f;

        public TextView TitleView;
        public ButtonView PositiveButton;
        public ButtonView NagetiveButton;

        public UIDialog(string title, string pTitle, UnityAction pAction, string nTitle, UnityAction nAction)
        {
            ContentView.BackgroundColor = Color.white;
            float buttonWidth = (AppearanceWidth - 3 * AppearanceMargin) / 2;

            TitleView = new TextView(ContentView)
            {
                UIRect = new UIRect(0, AppearanceMargin, AppearanceWidth, AppearanceHeight - AppearanceButtonHeight - AppearanceMargin * 2),
                Text = title,
                TextColor = Color.black,
                Alignment = TextAnchor.MiddleCenter,
                FontSize = 24
            };

            PositiveButton = new ButtonView(ContentView);
            PositiveButton.UIRect = new UIRect(buttonWidth + AppearanceMargin * 2, AppearanceHeight - AppearanceButtonHeight - AppearanceMargin * 0.5f, buttonWidth, AppearanceButtonHeight);
            PositiveButton.BackgroundColor = Color.gray;
            PositiveButton.Title = pTitle;
            PositiveButton.FontSize = 20;
            if (pAction != null)
            {
                PositiveButton.OnClickListener += (b => pAction());
            }
            PositiveButton.OnClickListener += (b => Destory());

            NagetiveButton = new ButtonView(ContentView);
            NagetiveButton.UIRect = new UIRect(AppearanceMargin, AppearanceHeight - AppearanceButtonHeight - AppearanceMargin * 0.5f, buttonWidth, AppearanceButtonHeight);
            NagetiveButton.Title = nTitle;
            NagetiveButton.BackgroundColor = new Color(0.8f, 0.8f, 0.8f);
            NagetiveButton.FontSize = 20;
            if (nAction != null)
            {
                NagetiveButton.OnClickListener += (b => nAction());
            }
            NagetiveButton.OnClickListener += (b => Destory());
        }
    }
}
