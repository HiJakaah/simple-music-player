﻿using System;
using System.Reactive.Linq;
using System.Reactive.Threading.Tasks;
using System.Windows;
using System.Windows.Interop;
using MahApps.Metro.Controls;
using ReactiveUI;
using SimpleMusicPlayer.Core;
using SimpleMusicPlayer.ViewModels;

namespace SimpleMusicPlayer.Views
{
    /// <summary>
    /// Interaction logic for MedialibView.xaml
    /// </summary>
    public partial class MedialibView : MetroWindow, System.Windows.Forms.IWin32Window, IViewFor<MedialibViewModel>
    {
        public MedialibView(MedialibViewModel medialibViewModel)
        {
            this.ViewModel = medialibViewModel;

            this.InitializeComponent();

            this.AllowDrop = true;

            this.WhenAnyValue(x => x.ViewModel).BindTo(this, x => x.DataContext);

            this.Events().SourceInitialized.Subscribe(e => this.FitIntoScreen());

            this.WhenActivated(d => this.WhenAnyValue(x => x.ViewModel)
                                        .Subscribe(vm => {
                                            this.Events().Closed.InvokeCommand(vm.FileSearchWorker.StopSearchCmd);
                                            this.Events().PreviewDragEnter.Merge(this.Events().PreviewDragOver).Subscribe(vm.OnDragOverAction);
                                            this.Events().PreviewDrop.SelectMany(x => vm.OnDropAction(x).ToObservable()).Subscribe();
                                        }));
        }

        public override IWindowPlacementSettings GetWindowPlacementSettings()
        {
            return this.ViewModel.CustomWindowPlacementSettings;
        }

        // only for ShowDialog from FolderBrowserDialog
        public IntPtr Handle
        {
            get
            {
                var intPtr = ((HwndSource)PresentationSource.FromVisual(this)).Handle;
                return intPtr;
            }
        }

        public MedialibViewModel ViewModel
        {
            get { return (MedialibViewModel)GetValue(ViewModelProperty); }
            set { SetValue(ViewModelProperty, value); }
        }

        public static readonly DependencyProperty ViewModelProperty =
            DependencyProperty.Register("ViewModel", typeof(MedialibViewModel), typeof(MedialibView), new PropertyMetadata(null));

        object IViewFor.ViewModel
        {
            get { return ViewModel; }
            set { ViewModel = (MedialibViewModel)value; }
        }
    }
}