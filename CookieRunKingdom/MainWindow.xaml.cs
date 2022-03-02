﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Rectangle = System.Drawing.Rectangle;

namespace CookieRunKingdom
{
    /// <summary>
    /// MainWindow.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class MainWindow : Window
    {
        private IntPtr _hwnd = IntPtr.Zero;
        private Rectangle _winRect = new Rectangle();

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            AddProcess();
        }

        private void cbProcessList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox comboBox = (ComboBox)sender;
            if (comboBox.Items.Count > 0)
            {
                string windowTitle = comboBox.SelectedValue.ToString();

                _hwnd = Dll.FindWindow(null, windowTitle);
                // 녹스의 경우 아래까지 지금은 녹스만 사용
                _hwnd = Dll.FindWindowEx(_hwnd, 0, "Qt5QWindowIcon", "ScreenBoardClassWindow");

                //Rectangle rect = Util.GetWindowRect(_hwnd);
                _winRect = Util.GetWindowRect(_hwnd);
            }
        }

        private void btnCapture_Click(object sender, RoutedEventArgs e)
        {
            capture();
        }

        private void btnPrpcessRefresh_Click(object sender, RoutedEventArgs e)
        {
            AddProcess();
        }
    }
}
