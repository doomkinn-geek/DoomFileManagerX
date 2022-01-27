﻿using DoomFileManagerX.Models;
using DoomFileManagerX.ViewModels;
using System;
using System.Collections.Generic;
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

namespace DoomFileManagerX
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindowViewModel MainWindowView { get; set; }
        public MainWindow()
        {
            InitializeComponent();
            DriveRootItem rootItem = new DriveRootItem();
            MainWindowView = new MainWindowViewModel(rootItem);
            base.DataContext = MainWindowView;
            mainTree.DataContext = MainWindowView;
        }
    }
}