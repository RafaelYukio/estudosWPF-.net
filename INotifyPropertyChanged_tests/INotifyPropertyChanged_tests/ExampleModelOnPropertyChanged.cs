using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

namespace INotifyPropertyChanged_tests
{
    public class ExampleModelOnPropertyChanged : ObservableObject
    {
        private string _randomFromModelOnPropertyChanged;

        public string RandomFromModelOnPropertyChanged
        {
            get { return _randomFromModelOnPropertyChanged; }
            set
            {
                _randomFromModelOnPropertyChanged = value;
                // Geralmente passam o nome da prop. que mudou, mas neste método passamos CallerMemberName que faz automático
                OnPropertyChange();
            }
        }


        // Aqui para testes, ver no console:
        public ExampleModelOnPropertyChanged()
        {
            Task.Run(() =>
            {
                while (true)
                {
                    // Abaixo método Random para atualizar a variável RandomFromModel da Model para a UI
                    Random r = new Random();
                    RandomFromModelOnPropertyChanged = r.Next(0, 1000).ToString();
                    Debug.WriteLine($"ExampleModel property RandomFromModelOnPropertyChanged (changing from Model): {RandomFromModelOnPropertyChanged}");
                    // Método não legal de fazer a Thread sleep, mas é apenas para testes:
                    Thread.Sleep(1000);
                }
            });
        }

    }
}
