﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using PrintIt_Desktop_4.ViewModels;

namespace PrintIt_Desktop_4.UserControls
{
    /// <summary>
    /// Interaction logic for PrinterList.xaml
    /// </summary>
    public partial class PrinterList : UserControl
    {
        public PrinterList()
        {
            InitializeComponent();
            (DataContext as PrinterListViewModel).CurrentControl = this;
        }
    }
}
