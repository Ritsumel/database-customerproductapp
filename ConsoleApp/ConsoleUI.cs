using ConsoleApp.Services;

namespace ConsoleApp;

internal class ConsoleUI
{
    private readonly ProductService _productService;
    private readonly CustomerService _customerService;
    private int currentOption = 0;

    public ConsoleUI(ProductService productService, CustomerService customerService)
    {
        _productService = productService;
        _customerService = customerService;
    }

    public void ShowMenu()
    {
        Console.Clear();
        Console.WriteLine("MENU OPTIONS:");
        DisplayMenuItem("Create Product", 0);
        DisplayMenuItem("Get Products", 1);
        DisplayMenuItem("Update Product", 2);
        DisplayMenuItem("Delete Product", 3);
        DisplayMenuItem("Create Customer", 4);
        DisplayMenuItem("Get Customers", 5);
        DisplayMenuItem("Update Customer", 6);
        DisplayMenuItem("Delete Customer", 7);
        DisplayMenuItem("Exit", 8);
        Console.WriteLine();
        ConsoleKeyInfo key = Console.ReadKey();

        switch (key.Key)
        {
            case ConsoleKey.UpArrow:
                currentOption = Math.Max(0, currentOption - 1);
                break;
            case ConsoleKey.DownArrow:
                currentOption = Math.Min(8, currentOption + 1);
                break;
            case ConsoleKey.Enter:
                PerformAction();
                break;
        }
    }

    private void DisplayMenuItem(string text, int option)
    {
        Console.Write($"{(currentOption == option ? "->" : "  ")} ");
        Console.WriteLine($"{text,-20}");
    }

    private void PerformAction()
    {
        switch (currentOption)
        {
            case 0:
                CreateProduct_UI();
                break;
            case 1:
                GetProducts_UI();
                break;
            case 2:
                UpdateProduct_UI();
                break;
            case 3:
                DeleteProduct_UI();
                break;
            case 4:
                CreateCustomer_UI();
                break;
            case 5:
                GetCustomers_UI();
                break;
            case 6:
                UpdateCustomer_UI();
                break;
            case 7:
                DeleteCustomer_UI();
                break;
            case 8:
                Console.WriteLine("Exiting...");
                Environment.Exit(0);
                break;
        }
    }

    public void Run()
    {
        while (true)
        {
            ShowMenu();
        }
    }


    //PRODUCTS
    public void CreateProduct_UI()
    {
        Console.Clear();
        Console.Write("Product Title: ");
        var title = Console.ReadLine()!;

        decimal price;
        Console.Write("Product Price: ");
        while (!decimal.TryParse(Console.ReadLine(), out price))
        {
            Console.WriteLine("Invalid input. Please enter a valid decimal for the product price.");
            Console.Write("Product Price: ");
        }

        Console.Write("Product Category: ");
        var categoryName = Console.ReadLine()!;

        var result = _productService.CreateProduct(title, price, categoryName);
        if (result != null)
        {
            Console.Clear();
            Console.WriteLine("Product was created!");
            Console.ReadKey();
        }
    }
    public void GetProducts_UI()
    {
        Console.Clear();

        var products = _productService.GetProducts();
        foreach (var product in products)
        {
            Console.WriteLine($"ID: {product.Id} - {product.Title} - {product.Category.CategoryName} ({product.Price} SEK)");
        }

        Console.ReadKey();
    }
    public void UpdateProduct_UI()
    {
        Console.Clear();
        int id;

        while (true)
        {
            Console.Write("Enter product by ID: ");

            if (int.TryParse(Console.ReadLine(), out id))
            {
                break;
            }
            else
            {
                Console.WriteLine("Invalid input. Please enter a valid number for the product ID.");
            }
        }

        var product = _productService.GetProductById(id);
        if (product != null)
        {
            Console.WriteLine($"{product.Title} - {product.Category.CategoryName} ({product.Price} SEK)");
            Console.WriteLine();

            Console.Write("New Product Title: ");
            product.Title = Console.ReadLine()!;

            var newProduct = _productService.UpdateProduct(product);
            Console.WriteLine($"{newProduct.Title} - {newProduct.Category.CategoryName} ({newProduct.Price} SEK) Updated");
        }
        else
        {
            Console.WriteLine($"No product found with ID: {id}");
        }

        Console.ReadKey();
    }
    public void DeleteProduct_UI()
    {
        Console.Clear();

        int id;
        bool validInput = false;

        do
        {
            Console.Write("Enter product by ID: ");

            if (int.TryParse(Console.ReadLine(), out id))
            {
                var product = _productService.GetProductById(id);
                if (product != null)
                {
                    _productService.DeleteProduct(product.Id);
                    Console.WriteLine("Product was deleted.");
                    validInput = true;
                }
                else
                {
                    Console.WriteLine("No product found.");
                }
            }
            else
            {
                Console.WriteLine("Invalid input. Please enter a valid number for the product ID.");
            }
        } while (!validInput);

        Console.ReadKey();
    }

    //CUSTOMERS
    public void CreateCustomer_UI()
    {
        Console.Clear();
        Console.Write("First Name: ");
        var firstName = Console.ReadLine()!;

        Console.Write("Last Name: ");
        var lastName = Console.ReadLine()!;

        Console.Write("Email: ");
        var email = Console.ReadLine()!;

        Console.Write("Street Name: ");
        var streetName = Console.ReadLine()!;

        Console.Write("Postal Code: ");
        var postalCode = Console.ReadLine()!;

        Console.Write("City: ");
        var city = Console.ReadLine()!;

        Console.Write("Role Name: ");
        var roleName = Console.ReadLine()!;

        var result = _customerService.CreateCustomer(firstName, lastName, email, roleName, streetName, postalCode, city);
        if (result != null)
        {
            Console.Clear();
            Console.WriteLine("Customer was created!");
            Console.ReadKey();
        }
    }
    public void GetCustomers_UI()
    {
        Console.Clear();

        var customers = _customerService.GetCustomers();
        foreach (var customer in customers)
        {
            Console.WriteLine($"{customer.FirstName} - {customer.LastName} ({customer.Role.RoleName})");
            Console.WriteLine($"{customer.Address.StreetName}, {customer.Address.PostalCode} {customer.Address.City}");
        }

        Console.ReadKey();
    }
    public void UpdateCustomer_UI()
    {
        Console.Clear();
        string email;

        while (true)
        {
            Console.Write("Enter customer email: ");
            email = Console.ReadLine()!;

            var customer = _customerService.GetCustomerByEmail(email);
            if (customer != null)
            {
                Console.WriteLine();
                Console.WriteLine($"{customer.FirstName} - {customer.LastName} ({customer.Role.RoleName})");
                Console.WriteLine($"{customer.Address.StreetName}, {customer.Address.PostalCode} {customer.Address.City}");
                Console.WriteLine();

                Console.Write("New last name: ");
                customer.LastName = Console.ReadLine()!;

                var newCustomer = _customerService.UpdateCustomer(customer);
                Console.WriteLine();
                Console.WriteLine($"{newCustomer.FirstName} - {newCustomer.LastName} ({newCustomer.Role.RoleName})");
                Console.WriteLine($"{newCustomer.Address.StreetName}, {newCustomer.Address.PostalCode} {newCustomer.Address.City}");
                Console.WriteLine();
                break;
            }
            else
            {
                Console.WriteLine("No customer found. Try again.");
            }
        }

        Console.ReadKey();
    }
    public void DeleteCustomer_UI()
    {
        Console.Clear();
        string email;
        bool validInput = false;

        do
        {
            Console.Write("Enter customer email: ");
            email = Console.ReadLine()!;

            var customer = _customerService.GetCustomerByEmail(email);
            if (customer != null)
            {
                Console.Clear();
                _customerService.DeleteCustomer(customer.Id);
                Console.WriteLine("Customer was deleted.");
                validInput = true;
            }
            else
            {
                Console.WriteLine("No customer found. Try again.");
            }
        } while (!validInput);

        Console.ReadKey();
    }
}
