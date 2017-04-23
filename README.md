## UnityView
基于UGUI开发的UI框架，提供坐标布局、动画、高性能可复用的列表视图
* * *
### UI视图的坐标原点被重定义为左上角（与Android、iOS相同）
* * *
### Animation 动画组件
动画组件采用了与cocoa框架类似的调用方式，iOS开发者可以轻松上手
```C#
// 默认为0.25秒
UIAnimation.Animate(() =>
{
    RotateAngleView.RotateAngle(45);
});
```
### ListView 可变高度列表组件
ListView与Android的实现类似，采用视图-适配器的使用方案</br>
TableView(不可变高度列表)、GridView实现基本类似
```C#
public class MyAdapter : IListAdapter
{
    public int GetCount()
    {
        return 50;
    }

    public ILayout GetConvertView(int position, ILayout convertView)
    {
        ButtonView cell = convertView as ButtonView;
        if (cell == null)
        {
            cell = new ButtonView();
        }
        cell.Title = "Item:" + position;
        return cell;
    }

    public float GetItemSize(int index)
    {
        return RandomHeight[index];
    }
}
```
