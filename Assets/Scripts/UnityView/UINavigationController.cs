using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UnityView
{
    public class UINavigationController : UIViewController
    {
        public readonly Stack<UIViewController> ControllerStack = new Stack<UIViewController>();
        public UINavigationController(UIViewController root)
        {
            ControllerStack.Push(root);
        }
        public void Push(UIViewController viewController)
        {
            viewController.NavigationController = this;
        }

        public void Pop(bool animated = false)
        {
            ControllerStack.Pop();            
        }

        public void PopToRoot(bool animated = false)
        {
            while(ControllerStack.Count > 1)
            {
                Pop(animated);
            }
        }
    }
}
