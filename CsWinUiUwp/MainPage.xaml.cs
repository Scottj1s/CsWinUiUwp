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

        static WeakReference CreateObject(bool withCapture)
        {
            var obj = new Grid();
            var captured = withCapture ? obj : null;
            obj.SizeChanged +=
                    (object sender, SizeChangedEventArgs e) => Debug.Assert(sender == captured);
            return new WeakReference(obj);
        }

        WeakReference withoutCapture;
        private void WithoutCapture_Click(object sender, RoutedEventArgs e)
        {
            // Succeeds, as there's no cycle between object and event handler
            withoutCapture = CreateObject(withCapture: false);
        }
        private void WithoutCapture_Check(object sender, RoutedEventArgs e)
        {
            GC.Collect();
            GC.WaitForPendingFinalizers();
            Status.Text = withoutCapture.IsAlive ? "Grid leaked" : "Grid collected";
        }

        WeakReference withCapture;
        private void WithCapture_Click(object sender, RoutedEventArgs e)
        {
            // Succeeds, even with a cycle between object and event handler
            withCapture = CreateObject(withCapture: true);
        }
        private void WithCapture_Check(object sender, RoutedEventArgs e)
        {
            GC.Collect();
            GC.WaitForPendingFinalizers();
            Status.Text = withCapture.IsAlive ? "Grid leaked" : "Grid collected";
        }

    }
}
