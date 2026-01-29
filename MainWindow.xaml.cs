using System;
using System.Windows;
using System.Windows.Controls;
using System.Xml.Serialization;

namespace Calculadora
{
    public partial class MainWindow : Window
    {
        private string? actualOperator = null;
        private bool cleanScreen = false;
        private bool lastOpWasNull = false;

        private Tools calc = new Tools();

        public MainWindow()
        {
            InitializeComponent();
        }

        private string SimbolOfOperator(string? op)
        {
            return op switch

            {   
                "Root" => "√",
                "C" => "C",
                "AC" => "AC",
                "Plus" => "+",
                "Minus" => "-",
                "Times" => "×",
                "Divide" => "÷",
                _ => ""
            };
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var botao = (Button)sender;

            if (cleanScreen)
            {
                DisplayText.Text = "";
                cleanScreen = false;
            }

            if(DisplayText.Text == "0") 
            {
                DisplayText.Text = botao.Content.ToString();
            }
            else 
            {
                DisplayText.Text += botao.Content.ToString();
            }
                
        }

        private void OpClick(object sender, RoutedEventArgs e)
        {
            var button = (Button)sender;

            string? operation = button.Tag.ToString();
            string? visualOp = button.Content.ToString();

            if (operation == "Minus" && cleanScreen)
            { 
                DisplayText.Text = "-"; 
                cleanScreen = false; 
                return; 
            }

            if (operation == null && lastOpWasNull)
            {
                DisplaySecundario.Text = "";
                lastOpWasNull = false;
                return;
            }

            if (operation == null)
            {
                lastOpWasNull = true;
            }
            else if (operation == "AC") 
            {
                DisplaySecundario.Text = "";
                DisplayText.Text = "0";
                actualOperator = null;
                calc.DefineOperation(null);
                cleanScreen = false;
                return;

            }
            else if (operation == "C")
            {
                
                DisplayText.Text = "0";
                cleanScreen = true;
                return;

            }           
            else
            {
                lastOpWasNull = false;
            }


            if (cleanScreen && actualOperator != null)
            {
                actualOperator = operation;
                calc.DefineOperation(operation);
                DisplaySecundario.Text = $"{calc.GetValue()} {visualOp}";
                return;
            }

            double actualValue; 
            
            if (!double.TryParse(DisplayText.Text, out actualValue))
                {
                    actualValue = 0; // ou o valor que fizer sentido }
                }



            if (actualOperator == null)
            {
                calc.DefineResult(actualValue);
            }
            else if (!cleanScreen)
            {
                double result = calc.Calculate(actualValue);
                DisplayText.Text = result.ToString();
                calc.DefineResult(result);
            }

            actualOperator = operation;
            calc.DefineOperation(operation);

            DisplaySecundario.Text = $"{calc.GetValue()} {visualOp}";

            cleanScreen = true;
        }

        private void EqualsClick(object sender, RoutedEventArgs e)
        {
            try
            {
                int counter = 0;
                double oldValue = calc.GetValue();
                double newValue;
                if (!double.TryParse(DisplayText.Text, out newValue)) 
                {
                 newValue = 0;
                }
                double result = calc.Calculate(newValue);

               if(lastOpWasNull == false) {
                    
                DisplaySecundario.Text = $"{oldValue} {SimbolOfOperator(actualOperator)} {newValue} =";

                DisplayText.Text = result.ToString();
                }
                else if(counter < 0)
                {
                    DisplaySecundario.Text = $"{SimbolOfOperator(actualOperator)} {newValue} =";

                    DisplayText.Text = "0";
                    counter++;
                }
                else 
                {
                    DisplaySecundario.Text = "";

                    DisplayText.Text = "0";
                    counter--;
                }
                cleanScreen = true;
                actualOperator = null;

                calc.DefineOperation(null);

                lastOpWasNull = true;

            }
            catch (DivideByZeroException ex)
            {
                DisplayText.Text = ex.Message;
                cleanScreen = true;
            }




            
        }

    }
}
