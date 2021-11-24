using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Калькулятор
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    
    public partial class MainWindow : Window
    {
        Calculator c;

        public MainWindow()
        {
            InitializeComponent();
            this.Loaded += OnLoad;
        }

        void OnLoad(object sender, RoutedEventArgs e)
        {
            c = new Calculator(currentEquation);
            AddClickEvents();
        }

        void AddClickEvents()
        {

            b1.Click += (object sender, RoutedEventArgs e) =>
            {
                c.Press(1);
            };

            b2.Click += (object sender, RoutedEventArgs e) =>
            {
                c.Press(2);
            };

            b3.Click += (object sender, RoutedEventArgs e) =>
            {
                c.Press(3);
            };

            b4.Click += (object sender, RoutedEventArgs e) =>
            {
                c.Press(4);
            };

            b5.Click += (object sender, RoutedEventArgs e) =>
            {
                c.Press(5);
            };

            b6.Click += (object sender, RoutedEventArgs e) =>
            {
                c.Press(6);
            };

            b7.Click += (object sender, RoutedEventArgs e) =>
            {
                c.Press(7);
            };

            b8.Click += (object sender, RoutedEventArgs e) =>
            {
                c.Press(8);
            };

            b9.Click += (object sender, RoutedEventArgs e) =>
            {
                c.Press(9);
            };

            b0.Click += (object sender, RoutedEventArgs e) =>
            {
                c.Press(0);
            };


            bdot.Click += (object sender, RoutedEventArgs e) =>
            {
                c.Press('.');
            };

            bplus.Click += (object sender, RoutedEventArgs e) =>
            {
                c.Press(Calculator.Function.Add);
            };

            bminus.Click += (object sender, RoutedEventArgs e) =>
            {
                c.Press(Calculator.Function.Minus);
            };

            btimes.Click += (object sender, RoutedEventArgs e) =>
            {
                c.Press(Calculator.Function.Times);
            };

            bdivide.Click += (object sender, RoutedEventArgs e) =>
            {
                c.Press(Calculator.Function.Divide);
            };

            bequals.Click += (object sender, RoutedEventArgs e) =>
            {
                c.Press(Calculator.Function.Equals);
            };

            bdelete.Click += (object sender, RoutedEventArgs e) =>
            {
                c.Press(Calculator.Function.Delete);
            };

            bclear.Click += (object sender, RoutedEventArgs e) =>
            {
                c.Press(Calculator.Function.Clear);
            };
        }

        private class Calculator
        {
            TextBox tb;

            private static class States
            {
                public static bool hasDot;
                public static bool willReplace;
                public static Function currentFunction;
                public static Dictionary<float, Function> lastNumberAndFunction;

            }

            public enum Function
            {
                Add,
                Minus,
                Times,
                Divide,
                Equals,
                Delete,
                Clear,
                None
            }

            public Calculator(TextBox text)
            {
                tb = text;
                States.hasDot = false;
                States.willReplace = false;
                States.currentFunction = Function.None;
                States.lastNumberAndFunction = new Dictionary<float, Function>();
            }

            public void Press(int number)
            {
                if (States.willReplace)
                {
                    tb.Text = number.ToString();
                    States.willReplace = false;
                    return;
                };

                tb.Text += number;
            }

            public void Press(char c)
            {
                if (c == '.' && !States.hasDot)
                {
                    tb.Text += c;
                    States.hasDot = true;
                }
            }

            public void Press(Function fn)
            {
                switch (fn)
                {
                    case Function.Add:
                    case Function.Minus:
                    case Function.Times:
                    case Function.Divide:
                        States.lastNumberAndFunction.Add(float.Parse(tb.Text), States.currentFunction);
                        States.currentFunction = fn;
                        States.willReplace = true;
                        break;
                    case Function.Equals:
                        States.lastNumberAndFunction.Add(float.Parse(tb.Text), States.currentFunction);
                        Equals();
                        break;
                    case Function.Delete:
                        tb.Text = String.Join("", tb.Text.Reverse().Skip(1).Reverse());
                        if (!tb.Text.Contains(".")) States.hasDot = false;
                        break;
                    case Function.Clear:
                        tb.Text = "";
                        States.hasDot = false;
                        break;
                }
            }

            void Equals()
            {
                float final = 0;
                foreach (KeyValuePair<float, Function> numberAndFunction in States.lastNumberAndFunction)
                {
                    switch (numberAndFunction.Value)
                    {
                        case Function.None:
                            final = numberAndFunction.Key;
                            break;
                        case Function.Add:
                            final += numberAndFunction.Key;
                            break;
                        case Function.Minus:
                            final -= numberAndFunction.Key;
                            break;
                        case Function.Times:
                            final *= numberAndFunction.Key;
                            break;
                        case Function.Divide:
                            if (numberAndFunction.Key == 0)
                            {
                                final = 0;
                                break;
                            }

                            final /= numberAndFunction.Key;
                            break;
                    }
                }

                tb.Text = final.ToString();
                States.lastNumberAndFunction.Clear();
                States.hasDot = false;
                States.currentFunction = Function.None;
                States.willReplace = true;
            }
        }
    }
}
