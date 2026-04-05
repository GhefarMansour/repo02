using Calculator.App.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace Calculator.App.ViewModels;

public partial class CalculatorViewModel : ObservableObject
{
    private readonly CalculatorEngine _engine = new();
    private bool _isNewEntry = true;
    private bool _hasOperator;

    [ObservableProperty]
    private string _display = "0";

    [ObservableProperty]
    private string _expression = string.Empty;

    [RelayCommand]
    private void Digit(string digit)
    {
        if (_isNewEntry)
        {
            Display = digit;
            _isNewEntry = false;
        }
        else
        {
            Display = Display == "0" ? digit : Display + digit;
        }
    }

    [RelayCommand]
    private void Decimal()
    {
        if (_isNewEntry)
        {
            Display = "0.";
            _isNewEntry = false;
        }
        else if (!Display.Contains('.'))
        {
            Display += ".";
        }
    }

    [RelayCommand]
    private void Operator(string op)
    {
        if (_hasOperator)
        {
            Equals();
        }

        _engine.FirstOperand = double.Parse(Display);
        _engine.Operation = op;
        Expression = $"{_engine.FirstOperand} {op}";
        _hasOperator = true;
        _isNewEntry = true;
    }

    [RelayCommand]
    private void Equals()
    {
        if (!_hasOperator)
            return;

        _engine.SecondOperand = double.Parse(Display);
        var result = _engine.Calculate();

        Expression = $"{_engine.FirstOperand} {_engine.Operation} {_engine.SecondOperand} =";
        Display = double.IsNaN(result) ? "Cannot divide by zero" : result.ToString("G15");

        _hasOperator = false;
        _isNewEntry = true;
    }

    [RelayCommand]
    private void Clear()
    {
        Display = "0";
        Expression = string.Empty;
        _engine.FirstOperand = 0;
        _engine.SecondOperand = 0;
        _engine.Operation = string.Empty;
        _hasOperator = false;
        _isNewEntry = true;
    }

    [RelayCommand]
    private void ClearEntry()
    {
        Display = "0";
        _isNewEntry = true;
    }

    [RelayCommand]
    private void Backspace()
    {
        if (_isNewEntry || Display == "Cannot divide by zero")
            return;

        Display = Display.Length > 1 ? Display[..^1] : "0";
    }

    [RelayCommand]
    private void Negate()
    {
        if (Display == "0" || Display == "Cannot divide by zero")
            return;

        Display = Display.StartsWith('-') ? Display[1..] : "-" + Display;
    }
}
