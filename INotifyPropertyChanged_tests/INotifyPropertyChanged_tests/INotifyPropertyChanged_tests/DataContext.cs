using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace INotifyPropertyChanged_tests
{
    public class DataContext
    {
        public ExampleModel ExampleModel { get; set; }
        public ExampleModelOnPropertyChanged ExampleModelOnPropertyChanged { get; set; }

        public DataContext()
        {
            ExampleModel = new ExampleModel();
            ExampleModelOnPropertyChanged = new ExampleModelOnPropertyChanged();
        }
    }
}
