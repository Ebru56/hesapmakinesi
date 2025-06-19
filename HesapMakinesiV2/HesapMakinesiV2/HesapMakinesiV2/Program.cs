using System;
using System.Linq;

class Program
{
    static void Main()
    {
        Console.WriteLine("Gelişmiş Hesap Makinesi");
        Console.WriteLine("-------------------");

        for (int operationCount = 1; operationCount <= 3; operationCount++)
        {
            try
            {
                Console.WriteLine($"\nİşlem {operationCount}/3");
                Console.WriteLine("-------------------");

                Console.WriteLine("İşlem türünü seçin:");
                Console.WriteLine("1. Temel İşlemler (+, -, *, /)");
                Console.WriteLine("2. Gelişmiş İşlemler (üs alma, kök alma, mod alma)");
                Console.WriteLine("3. Trigonometrik İşlemler (sin, cos, tan)");

                int operationType = GetValidOperationType();

                double result;
                if (operationType == 1)
                {
                    double firstNumber = GetValidNumber("Birinci sayıyı girin: ");
                    double secondNumber = GetValidNumber("İkinci sayıyı girin: ");
                    char operation = GetValidOperation();
                    result = Calculate(firstNumber, secondNumber, operation);
                    Console.WriteLine($"\n{firstNumber} {operation} {secondNumber} = {result}");
                }
                else if (operationType == 2)
                {
                    double firstNumber = GetValidNumber("Birinci sayıyı girin: ");
                    double secondNumber = GetValidNumber("İkinci sayıyı girin: ");
                    string advancedOperation = GetValidAdvancedOperation();
                    result = CalculateAdvanced(firstNumber, secondNumber, advancedOperation);
                    Console.WriteLine($"\n{firstNumber} {advancedOperation} {secondNumber} = {result}");
                }
                else
                {
                    double angle = GetValidNumber("Açıyı derece cinsinden girin: ");
                    string trigOperation = GetValidTrigOperation();
                    result = CalculateTrigonometric(angle, trigOperation);
                    Console.WriteLine($"\n{trigOperation}({angle}°) = {result}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\nHata oluştu: {ex.Message}");
            }

            if (operationCount < 3)
            {
                Console.WriteLine("\nSonraki işleme geçmek için bir tuşa basın...");
                Console.ReadKey();
                Console.Clear();
            }
        }

        Console.WriteLine("\nProgramı kapatmak için bir tuşa basın...");
        Console.ReadKey();
    }

    static int GetValidOperationType()
    {
        while (true)
        {
            Console.Write("Seçiminiz (1-3): ");
            if (int.TryParse(Console.ReadLine(), out int choice) && choice >= 1 && choice <= 3)
            {
                return choice;
            }
            Console.WriteLine("Geçersiz seçim! Lütfen 1, 2 veya 3 girin.");
        }
    }

    static double GetValidNumber(string prompt)
    {
        while (true)
        {
            Console.Write(prompt);
            string input = Console.ReadLine();

            if (double.TryParse(input, out double number))
            {
                return number;
            }
            Console.WriteLine("Geçersiz sayı! Lütfen tekrar deneyin.");
        }
    }

    static char GetValidOperation()
    {
        while (true)
        {
            Console.Write("İşlem seçin (+, -, *, /): ");
            string input = Console.ReadLine();

            if (input.Length == 1 && "+-*/".Contains(input[0]))
            {
                return input[0];
            }
            Console.WriteLine("Geçersiz işlem! Lütfen +, -, * veya / girin.");
        }
    }

    static string GetValidAdvancedOperation()
    {
        while (true)
        {
            Console.WriteLine("Gelişmiş işlem seçin:");
            Console.WriteLine("1. Üs alma (^)");
            Console.WriteLine("2. Karekök (√)");
            Console.WriteLine("3. Mod alma (%)");
            Console.Write("Seçiminiz (1-3): ");

            string input = Console.ReadLine();
            switch (input)
            {
                case "1": return "^";
                case "2": return "√";
                case "3": return "%";
                default:
                    Console.WriteLine("Geçersiz seçim! Lütfen 1, 2 veya 3 girin.");
                    break;
            }
        }
    }

    static string GetValidTrigOperation()
    {
        while (true)
        {
            Console.WriteLine("Trigonometrik işlem seçin:");
            Console.WriteLine("1. Sinüs (sin)");
            Console.WriteLine("2. Kosinüs (cos)");
            Console.WriteLine("3. Tanjant (tan)");
            Console.Write("Seçiminiz (1-3): ");

            string input = Console.ReadLine();
            switch (input)
            {
                case "1": return "sin";
                case "2": return "cos";
                case "3": return "tan";
                default:
                    Console.WriteLine("Geçersiz seçim! Lütfen 1, 2 veya 3 girin.");
                    break;
            }
        }
    }

    static double Calculate(double firstNumber, double secondNumber, char operation)
    {
        switch (operation)
        {
            case '+':
                return firstNumber + secondNumber;
            case '-':
                return firstNumber - secondNumber;
            case '*':
                return firstNumber * secondNumber;
            case '/':
                if (secondNumber == 0)
                {
                    throw new DivideByZeroException("Sıfıra bölme hatası!");
                }
                return firstNumber / secondNumber;
            default:
                throw new ArgumentException("Geçersiz işlem!");
        }
    }

    static double CalculateAdvanced(double firstNumber, double secondNumber, string operation)
    {
        switch (operation)
        {
            case "^":
                return Math.Pow(firstNumber, secondNumber);
            case "√":
                if (firstNumber < 0)
                {
                    throw new ArgumentException("Negatif sayının karekökü alınamaz!");
                }
                return Math.Sqrt(firstNumber);
            case "%":
                if (secondNumber == 0)
                {
                    throw new DivideByZeroException("Sıfıra bölme hatası!");
                }
                return firstNumber % secondNumber;
            default:
                throw new ArgumentException("Geçersiz işlem!");
        }
    }

    static double CalculateTrigonometric(double angle, string operation)
    {
        // Convert angle from degrees to radians
        double radians = angle * Math.PI / 180;

        switch (operation)
        {
            case "sin":
                return Math.Round(Math.Sin(radians), 4);
            case "cos":
                return Math.Round(Math.Cos(radians), 4);
            case "tan":
                if (Math.Cos(radians) == 0)
                {
                    throw new ArgumentException("Tanjant tanımsız!");
                }
                return Math.Round(Math.Tan(radians), 4);
            default:
                throw new ArgumentException("Geçersiz işlem!");
        }
    }
}