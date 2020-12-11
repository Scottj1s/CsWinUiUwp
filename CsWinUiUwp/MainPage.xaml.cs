using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace CsWinUiUwp
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
        }

        private void myButton_Click(object sender, RoutedEventArgs e)
        {
            static WeakReference CreateObject(bool withCapture)
            {
                var obj = new Grid();
                var captured = withCapture ? obj : null;
                obj.SizeChanged +=
                       (object sender, SizeChangedEventArgs e) => Debug.Assert(sender == captured);
                return new WeakReference(obj);
            };

            // Succeeds, as there's no cycle between object and event handler
            //var withoutCapture = CreateObject(withCapture: false);
            //GC.Collect();
            //GC.WaitForPendingFinalizers();
            //Debug.Assert(!withoutCapture.IsAlive);

            // Fails, because there's a cycle between object and event handler
            var withCapture = CreateObject(withCapture: true);
            GC.Collect();
            GC.WaitForPendingFinalizers();
            Debug.Assert(!withCapture.IsAlive);
        }
    }
}
