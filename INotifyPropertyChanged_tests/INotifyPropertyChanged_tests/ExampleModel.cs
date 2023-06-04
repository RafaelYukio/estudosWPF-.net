using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

namespace INotifyPropertyChanged_tests
{
    public class ExampleModel
    {
        // Aqui é a model, que também está funcionando como umd datacontext
        // Geralmente o datacontext vem da ViewModel

        // Isto é um POCO
        // https://www.macoratti.net/19/07/c_dtovopc1.htm

        // Explicação do porque usar field e property
        // https://stackoverflow.com/questions/35569627/why-create-private-fields-for-properties?rq=1
        // Em props onde não é feito nada além de setar e retornar o valor passado, usamos o "auto-implemented" (que é o mais visto)
        // Doc. Microsoft:
        // https://learn.microsoft.com/en-us/dotnet/csharp/programming-guide/classes-and-structs/auto-implemented-properties
        // Para ter uma valor iniciado na prop.: public string FirstName { get; set; } = "Jane";
        // Diferente maneiras de declarar props. com get, set e init:
        // - Declare only a get accessor (immutable everywhere except the constructor).
        // - Declare a get accessor and an init accessor(immutable everywhere except during object construction).
        // - Declare the set accessor as private (immutable to consumers).

        // Para declarar um field e sua prop., usar "propfull"

        private string _triggerDefault;

        public string TriggerDefault
        {
            get { return _triggerDefault; }
            set { _triggerDefault = value; }
        }

        private string _triggerPropertyChanged;

        public string TriggerPropertyChanged
        {
            get { return _triggerPropertyChanged; }
            set { _triggerPropertyChanged = value; }
        }

        private string _randomFromModel;

        public string RandomFromModel
        {
            get { return _randomFromModel; }
            set { _randomFromModel = value; }
        }

        // Aqui para testes, ver no console:
        public ExampleModel()
        {
            Task.Run(() =>
            {
                while (true)
                {
                    // Abaixo método Random para atualizar a variável RandomFromModel da Model para a UI
                    Random r = new Random();
                    RandomFromModel = r.Next(0, 1000).ToString();
                    Debug.WriteLine($"ExampleModel property TriggerDefault: {TriggerDefault}");
                    Debug.WriteLine($"ExampleModel property TriggerPropertyChanged: {TriggerPropertyChanged}");
                    Debug.WriteLine($"ExampleModel property RandomFromModel (changing from Model): {RandomFromModel}");
                    // Método não legal de fazer a Thread sleep, mas é apenas para testes:
                    Thread.Sleep(1000);
                }
            });
        }

    }
}
