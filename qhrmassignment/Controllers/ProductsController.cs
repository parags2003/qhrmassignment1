using Dapper;
using Microsoft.AspNetCore.Mvc;
using qhrmassignment.Models;
using System.Data;

namespace qhrmassignment.Controllers
{
    public class ProductsController : Controller
    {
        private readonly IDbConnection _dbConnection;

        public ProductsController(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        // Display all products
        public IActionResult Index()
        {
            var query = "SELECT * FROM Products";
            var products = _dbConnection.Query<Product>(query).ToList();
            return View(products);
        }

        // Show the add product form
        [HttpGet]
        public IActionResult AddProduct()
        {
            return View(new Product());
        }

        // Add new product to database
        [HttpPost]
        public IActionResult AddProduct(Product product)
        {
            var query = "INSERT INTO Products (Name, Description, Price) VALUES (@Name, @Description, @Price)";
            _dbConnection.Execute(query, product);
            return RedirectToAction("Index");
        }

        // Show the edit product form
        [HttpGet]
        public IActionResult EditProduct(int id)
        {
            var query = "SELECT * FROM Products WHERE ProductId = @Id";
            var product = _dbConnection.QuerySingleOrDefault<Product>(query, new { Id = id });
            if (product == null) return NotFound();
            return View(product);
        }

        // Update product details
        [HttpPost]
        public IActionResult EditProduct(Product product)
        {
            var query = "UPDATE Products SET Name = @Name, Description = @Description, Price = @Price WHERE ProductId = @ProductId";
            _dbConnection.Execute(query, product);
            return RedirectToAction("Index");
        }

        // Delete a product
        public IActionResult DeleteProduct(int id)
        {
            var query = "DELETE FROM Products WHERE ProductId = @Id";
            _dbConnection.Execute(query, new { Id = id });
            return RedirectToAction("Index");
        }
    }
}
