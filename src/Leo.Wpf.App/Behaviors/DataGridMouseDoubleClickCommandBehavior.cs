using Microsoft.Xaml.Behaviors;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Leo.Wpf.App.Behaviors
{
    public class DataGridMouseDoubleClickCommandBehavior : Behavior<DataGrid>
    {
        public ICommand Command
        {
            get { return (ICommand)GetValue(CommandProperty); }
            set { SetValue(CommandProperty, value); }
        }

        public static readonly DependencyProperty CommandProperty =
            DependencyProperty.Register(
                "Command",
                typeof(ICommand),
                typeof(DataGridMouseDoubleClickCommandBehavior),
                new UIPropertyMetadata(null));

        protected override void OnAttached()
        {
            base.OnAttached();
            this.AssociatedObject.LoadingRow += OnLoadingRow;
            this.AssociatedObject.UnloadingRow += OnUnloadingRow;
        }

        protected override void OnDetaching()
        {
            base.OnDetaching();
            this.AssociatedObject.LoadingRow -= OnLoadingRow;
            this.AssociatedObject.UnloadingRow -= OnUnloadingRow;
        }

        private void OnLoadingRow(object? sender, DataGridRowEventArgs e)
        {
            e.Row.MouseDoubleClick += OnMouseDoubleClick;
        }

        private void OnUnloadingRow(object? sender, DataGridRowEventArgs e)
        {
            e.Row.MouseDoubleClick -= OnMouseDoubleClick;
        }

        void OnMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (this.Command != null)
            {
                if (this.Command.CanExecute(null))
                {
                    this.Command.Execute(null); // Or pass any relevant parameters.
                }
            }
        }
    }
}
