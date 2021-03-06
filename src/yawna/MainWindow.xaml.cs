﻿using ReactiveUI;
using yawna.ViewModel;

namespace yawna
{
  public partial class MainWindow : IViewFor<MainViewModel>
  {
    public MainViewModel MainViewModel { get; protected set; }
    public MainViewModel ViewModel { get { return MainViewModel; } set { MainViewModel = value; } }

    object IViewFor.ViewModel
    {
      get => ViewModel;
      set => ViewModel = (MainViewModel)value;
    }

    public MainWindow()
    {
      InitializeComponent();

      this.MainViewModel = new MainViewModel();
      this.DataContext = this.MainViewModel;

      this.WhenActivated(
        dispose =>
        {
        // d(CommonInteractions.CheckToProceed.RegisterHandler(
        //     async interaction =>
        //     {
        //       MessageDialogResult shouldContinue =
        //                         await
        //                             this.ShowMessageAsync(
        //                                 "Please confirm",
        //                                 interaction.Input,
        //                                 MessageDialogStyle.AffirmativeAndNegative);

        //       interaction.SetOutput(shouldContinue == MessageDialogResult.Affirmative);
        //     }));

        // d(CommonInteractions.GetStringResponse.RegisterHandler(
        //     async interaction =>
        //     {
        //       string input = await this.ShowInputAsync("Please confirm", interaction.Input);
        //       interaction.SetOutput(input);
        //     }));

          dispose(this.WhenAnyValue(x => x.MainViewModel).BindTo(this, x => x.DataContext));
          dispose(this.WhenAnyValue(x => x.ViewModel.IsInViewMode).BindTo(this, x => x.EditStackPanel.Visibility));
          dispose(this.WhenAnyValue(x => x.ViewModel.IsInEditMode).BindTo(this, x => x.SaveCancelStackPanel.Visibility));
          
          dispose(this.Bind(this.ViewModel, x => x.SearchTerm, x => x.SearchTextBox.Text));

          dispose(this.BindCommand(this.ViewModel, vm => vm.HomeCmd, v => v.HomeButton));
          dispose(this.BindCommand(this.ViewModel, vm => vm.NavigateBeforeCmd, v => v.NavigateBeforeButton));
          dispose(this.BindCommand(this.ViewModel, vm => vm.NavigateAfterCmd, v => v.NavigateNextButton));
          dispose(this.BindCommand(this.ViewModel, vm => vm.SearchCmd, v => v.SearchButton));
          dispose(this.BindCommand(this.ViewModel, vm => vm.SettingsCmd, v => v.SettingsButton));
          dispose(this.BindCommand(this.ViewModel, vm => vm.EditCmd, v => v.EditButton));
          dispose(this.BindCommand(this.ViewModel, vm => vm.SaveCmd, v => v.SaveButton));
          dispose(this.BindCommand(this.ViewModel, vm => vm.CancelCmd, v => v.CancelButton));
      });
    }
  }
}
