﻿using System.Windows;
using NLog;

namespace DBQueryTool.Views
{
    public class WindowBase : Window
    {
        protected static readonly Logger Logger = LogManager.GetCurrentClassLogger();
    }
}