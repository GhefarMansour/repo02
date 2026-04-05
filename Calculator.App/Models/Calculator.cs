namespace Calculator.App.Models;

public class CalculatorEngine
{
    public double FirstOperand { get; set; }
    public double SecondOperand { get; set; }
    public string Operation { get; set; } = string.Empty;

    public double Calculate()
    {
        return Operation switch
        {
            "+" => FirstOperand + SecondOperand,
            "-" => FirstOperand - SecondOperand,
            "×" => FirstOperand * SecondOperand,
            "÷" => SecondOperand != 0 ? FirstOperand / SecondOperand : double.NaN,
            "%" => FirstOperand % SecondOperand,
            _ => SecondOperand
        };
    }
}
