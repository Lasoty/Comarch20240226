using System.Globalization;
using System.Windows;
using System.Windows.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace WpfCalculator.App
{
    [INotifyPropertyChanged]
    public partial class MainViewModel
    {
        public MainViewModel()
        {
            Display = "0";
        }

        private bool isOperationActive;
        private string display;
        private double lastNumber, result;
        private SelectedOperator selectedOperator;

        #region Properties

        public string Display
        {
            get => display;
            set => SetProperty(ref display, value);
        }

        #endregion


        #region Commands

        public ICommand SetNumberCommand => new RelayCommand<string>(SetNumber);

        private void SetNumber(string sNumber)
        {
            if (Display == "0" || isOperationActive)
            {
                Display = sNumber;
            }
            else
            {
                Display += sNumber;
            }

            isOperationActive = false;
        }

        public ICommand CleanCommand => new RelayCommand(Clean);

        private void Clean()
        {
            Display = "0";
            result = 0;
            lastNumber = 0;
        }

        public ICommand ResultCommand => new RelayCommand(Result);

        private void Result()
        {
            double newNumber;
            if (double.TryParse(Display, out newNumber))
            {
                switch (selectedOperator)
                {
                    case SelectedOperator.Addition:
                        result = Add(lastNumber, newNumber);
                        break;
                    case SelectedOperator.Subtraction:
                        result = Subtract(lastNumber, newNumber);
                        break;
                    case SelectedOperator.Multiplication:
                        result = Multiply(lastNumber, newNumber);
                        break;
                    case SelectedOperator.Division:
                        result = Divide(lastNumber, newNumber);
                        break;
                }

                Display = result.ToString(CultureInfo.InvariantCulture);
            }
        }

        public ICommand PlusCommand => new RelayCommand(() =>
        {
            selectedOperator = SelectedOperator.Addition;
            lastNumber = int.Parse(Display);
            isOperationActive = true;
        });

        public ICommand MinusCommand => new RelayCommand(() =>
        {
            selectedOperator = SelectedOperator.Subtraction;
            lastNumber = int.Parse(Display);
            isOperationActive = true;
        });

        public ICommand MultiplyCommand => new RelayCommand(() =>
        {
            selectedOperator = SelectedOperator.Multiplication;
            lastNumber = int.Parse(Display);
            isOperationActive = true;
        });

        public ICommand DivisionCommand => new RelayCommand(() =>
        {
            selectedOperator = SelectedOperator.Division;
            lastNumber = int.Parse(Display);
            isOperationActive = true;
        });

        #endregion


        #region Private methods

        public double Add(double n1, double n2) => n1 + n2;
        public double Subtract(double n1, double n2) => n1 - n2;
        public double Multiply(double n1, double n2) => n1 * n2;
        public double Divide(double n1, double n2)
        {
            if (n2 == 0)
            {
                MessageBox.Show("Dzielenie przez zero jest niemożliwe", "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);
                return 0;
            }

            return n1 / n2;
        }

        #endregion

    }
    enum SelectedOperator
    {
        Addition,
        Subtraction,
        Multiplication,
        Division
    }
}


