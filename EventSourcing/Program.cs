// See https://aka.ms/new-console-template for more information
using EventSourcing.Repo;

Console.WriteLine("Hello, World!");
var sku = "Item";
var warehouseProductRepository = new WarehouseProductRepository();
var warehouseProduct = warehouseProductRepository.Get(sku);
warehouseProduct.ReceiveProduct(10);
warehouseProduct.ShipProduct(10);
warehouseProduct.ReceiveProduct(10);
warehouseProduct.AdjustInventory(10, "test");
warehouseProductRepository.Save(warehouseProduct);

Console.ReadLine();
