using System.Text.Json;

class Expense
{
    public string Description { get; set; }
    public decimal Amount { get; set; }
    public DateTime Date { get; set; } = DateTime.Now;
}

class ExpenseTracker
{
    private List<Expense> expenses = new();
    private string filePath = "expenses.json";

    public ExpenseTracker()
    {
        LoadExpenses();
    }

    public void AddExpense()
    {
        Console.Write("Description: ");
        string desc = Console.ReadLine() ?? "";

        Console.Write("Amount: ");
        decimal amount = decimal.Parse(Console.ReadLine() ?? "0");

        expenses.Add(new Expense { Description = desc, Amount = amount });
        SaveExpenses();

        Console.WriteLine("Expense added successfully!\n");
    }

    public void ViewExpenses()
    {
        Console.WriteLine("\n--- Expense List ---");
        foreach (var exp in expenses)
        {
            Console.WriteLine($"{exp.Date.ToShortDateString()} | {exp.Description} - {exp.Amount} USD");
        }
        Console.WriteLine("----------------------\n");
    }

    public void ShowTotal()
    {
        decimal total = expenses.Sum(e => e.Amount);
        Console.WriteLine($"Total spent: {total} USD\n");
    }

    private void SaveExpenses()
    {
        File.WriteAllText(filePath, JsonSerializer.Serialize(expenses));
    }

    private void LoadExpenses()
    {
        if (File.Exists(filePath))
        {
            string json = File.ReadAllText(filePath);
            expenses = JsonSerializer.Deserialize<List<Expense>>(json) ?? new();
        }
    }
}

class Program
{
    static void Main()
    {
        ExpenseTracker tracker = new();

        while (true)
        {
            Console.WriteLine("=== Expense Tracker ===");
            Console.WriteLine("1. Add Expense");
            Console.WriteLine("2. View Expenses");
            Console.WriteLine("3. Show Total");
            Console.WriteLine("4. Exit");
            Console.Write("Choose an option: ");

            string choice = Console.ReadLine() ?? "";
            Console.Clear();

            switch (choice)
            {
                case "1": tracker.AddExpense(); break;
                case "2": tracker.ViewExpenses(); break;
                case "3": tracker.ShowTotal(); break;
                case "4": return;
                default: Console.WriteLine("Invalid option!\n"); break;
            }
        }
    }
}