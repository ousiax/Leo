// MIT License

using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Microsoft.Xaml.Behaviors;

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
            AssociatedObject.LoadingRow += OnLoadingRow;
            AssociatedObject.UnloadingRow += OnUnloadingRow;
        }

        protected override void OnDetaching()
        {
            base.OnDetaching();
            AssociatedObject.LoadingRow -= OnLoadingRow;
            AssociatedObject.UnloadingRow -= OnUnloadingRow;
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
            if (Command != null)
            {
                if (Command.CanExecute(null))
                {
                    Command.Execute(null); // Or pass any relevant parameters.
                }
            }
        }
    }
}
