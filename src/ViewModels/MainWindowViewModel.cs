﻿using System;
using System.Reactive.Linq;

using MessageBox.Avalonia.DTO;
using MessageBox.Avalonia.Enums;
using ReactiveUI;

using Yatsugi.Views;
using Yatsugi.Models;
using Yatsugi.Models.DataTypes;

namespace Yatsugi.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        public UserSettings Settings { get; set; }

        private ViewModelBase content;
        public ViewModelBase Content
        {
            get => content;
            private set => this.RaiseAndSetIfChanged(ref content, value);
        }

        public MainWindowViewModel()
        {
            MoveToStartMenu();
        }

        public override async void OnWindowOpened(MainWindow mainWindow)
        {
            try
            {
                ToolDataBase.LoadAll();
            }
            catch(Exception ex)
            {
                var msBoxStandardWindow = MessageBox.Avalonia.MessageBoxManager.GetMessageBoxStandardWindow(new MessageBoxStandardParams{
                    ButtonDefinitions = ButtonEnum.Ok,
                    ContentTitle = "Error ocurred: It seems to fail to load setting files.",
                    ContentMessage = ex.ToString(),
                    Icon = Icon.Error,
                    Style = Style.MacOs
                });
                await msBoxStandardWindow.ShowDialog(mainWindow);
                throw ex;
            }
        }

        public void MoveToStartMenu()
        {
            var startMenu = new StartMenuViewModel();
            startMenu.OnLentButtonClicked
                .Take(1)
                .Subscribe((unit) =>
                {
                    MoveToLentView();
                });
            startMenu.OnReturnButtonClicked
                .Take(1)
                .Subscribe((unit) =>
                {
                    MoveToReturnView();
                });
            startMenu.OnManageButtonClicked
                .Take(1)
                .Subscribe((unit) =>
                {
                    MoveToToolManagerView();
                });
            startMenu.OnSettingKeyDown
                .Take(1)
                .Subscribe((unit) =>
                {
                    MoveToUserSettings();
                });
            Content = startMenu;
        }

        public void MoveToLentView()
        {
            var lentView = new LentViewModel();
            lentView.OnBackButtonClicked
                .Take(1)
                .Subscribe((unit) =>
                {
                    MoveToStartMenu();
                });
            Content = lentView;
        }

        public void MoveToReturnView()
        {
            Content = new ReturnViewModel();
        }

        public void MoveToToolManagerView()
        {
            var toolManagerViewModel = new ToolManagerViewModel();
            toolManagerViewModel.OnBackButtonClicked
                .Take(1)
                .Subscribe((unit) =>
                {
                    MoveToStartMenu();
                });
            Content = toolManagerViewModel;
        }

        public void MoveToUserSettings()
        {
            Content = new UserSettingsViewModel();
        }
    }
}
