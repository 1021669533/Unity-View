using System.Collections.Generic;
using Assets.Scripts;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityView;
using UnityView.Component;

public class UICreator : MonoBehaviour
{
    public Dictionary<string, UILayout> Items;
    public UIGroup<UILayout> Group;
    public ListView TypeListView;

    protected string[] PartTitles;

    public static UIRect MainRect = new UIRect(UICanvas.DesignWidth / 4.25f, 0, UICanvas.DesignWidth - UICanvas.DesignWidth / 4.25f, UICanvas.DesignHeight);
    void Start()
    {
        var eventSystem = FindObjectOfType<EventSystem>();
        eventSystem.pixelDragThreshold = (int) (Screen.width / 50f);
        Items = new Dictionary<string, UILayout>();

        PartTitles = new [] {
            "Components",
            "TableView",
            "ListView",
            "GridView",
            "Animation"
        };

        Group = new UIGroup<UILayout>((layout) =>
        {
            layout.UIObject.SetActive(true);
        }, (layout) =>
        {
            if (layout.UIObject.activeSelf) layout.UIObject.SetActive(false);
        });

        TypeListView = new ListView
        {
            Name = "Type List View",
            UIRect = new UIRect(15, 0, UICanvas.DesignWidth / 4.25f - 30, 720),
            Adapter = new TypeListAdapter(PartTitles, Group),
            BackgroundColor = new Color(0, 0, 0, 0.75f),
            OnItemSelectedListener = (index) =>
            {
                Debug.Log("Select : " + index);
                Group.Select(Items[PartTitles[index]]);
            }
        };

        // Components
        UILayout components = new Part1();
        components.Name = "Components";
        Items.Add(PartTitles[0], components);
        Group.Add(components);
        components.Visible = false;

        // TableView
        UILayout tableView = new Part2();
        tableView.Name = "Table View";
        Items.Add(PartTitles[1], tableView);
        Group.Add(tableView);
        tableView.Visible = false;

        // ListView
        ListView listView = new Part3();
        listView.Name = "List View";

        TextView textView = new TextView(listView);
        textView.UIFrame = new UIFrame(0, 0, UICanvas.DesignWidth - UICanvas.DesignWidth / 4.25f, 150);
        textView.TextComponent.alignment = TextAnchor.MiddleCenter;
        textView.FontSize = 30;
        textView.Text = "List View Header";
        listView.HeaderView = textView;

        listView.Adapter = new Part3Adapter();
        Items.Add(PartTitles[2], listView);
        Group.Add(listView);
        listView.Visible = false;

        // GridView
        GridView gridView = new Part4();
        gridView.Name = "Grid View";
        gridView.ItemSize = new Vector2(150, 150);
        gridView.Adapter = new Part4Adapter();
        Items.Add(PartTitles[3], gridView);
        Group.Add(gridView);
        gridView.Visible = false;

        // Animation
        UILayout animationPanel = new Part5();
        Group.Add(animationPanel);
    }
}
