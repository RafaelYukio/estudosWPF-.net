
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace INotifyPropertyChanged_tests
{
    public class ObservableObject : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        public void OnPropertyChange([CallerMemberName] string propertyName = null)
        {
            // Formas de checar null:
            // https://www.thomasclaudiushuber.com/2020/03/12/c-different-ways-to-check-for-null/
            // Ver conceito de ?? e _
            // ?? = null-coalescing operator:
            // https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/operators/null-coalescing-operator
            // _ = discards:
            // https://learn.microsoft.com/en-us/dotnet/csharp/fundamentals/functional/discards
            // ? = Null propagating operator:
            // https://www.codingame.com/playgrounds/5107/null-propagating-operator-in-c

            // Utiliza Reflection para atualizar a UI
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
