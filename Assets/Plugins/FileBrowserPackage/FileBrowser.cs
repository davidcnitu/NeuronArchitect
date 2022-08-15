#if UNITY_STANDALONE_WIN
using Ookii.Dialogs;
using System;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using UnityEngine;

namespace FileBrowserPackage.Windows
{
    public class FileBrowser
    {
        [DllImport("user32.dll")]
        private static extern IntPtr GetActiveWindow();
        public FileBrowser() { }
        public string OpenFolderBrowser()
        {
            var ofd = new VistaFolderBrowserDialog();
            ofd.ShowDialog(new WindowWrapper(GetActiveWindow()));
            return ofd.SelectedPath;
        }
    }
    public class WindowWrapper : IWin32Window
    {
        public WindowWrapper(IntPtr handle)
        {
            _hwnd = handle;
        }

        public IntPtr Handle
        {
            get { return _hwnd; }
        }

        private IntPtr _hwnd;
    }
}
#endif