using TMAWarehouse.Models;

namespace TMAWarehouse.Data;

public class DbSeeder
{
    private readonly TmaDbContext _dbContext;

    public DbSeeder(TmaDbContext dbContext)
    {
        this._dbContext = dbContext;
    }
    public void Seed()
    {
        if (_dbContext.Database.CanConnect())
        {
            if (!_dbContext.Photos.Any())
            {
                SeedPhotos();
            }
            if (!_dbContext.Items.Any())
            {
                SeedItems();
            }
            if (!_dbContext.Requests.Any())
            {
                SeedRequests();
            }
        }
    }
    private void SeedPhotos()
    {
        var photos = new List<Photo>
        {
            new Photo { PhotoBinary = File.ReadAllBytes("Data/Pictures/laptop.jpg")},
            new Photo { PhotoBinary = File.ReadAllBytes("Data/Pictures/printer.jpg")},
            new Photo { PhotoBinary = File.ReadAllBytes("Data/Pictures/smartphone.jpg")},
            new Photo { PhotoBinary = File.ReadAllBytes("Data/Pictures/table.jpg")},
            new Photo { PhotoBinary = File.ReadAllBytes("Data/Pictures/desktop.jpg")},
            new Photo { PhotoBinary = File.ReadAllBytes("Data/Pictures/scanner.jpg")},
            new Photo { PhotoBinary = File.ReadAllBytes("Data/Pictures/water.jpg")},
            new Photo { PhotoBinary = File.ReadAllBytes("Data/Pictures/coffee.jpg")},
            new Photo { PhotoBinary = File.ReadAllBytes("Data/Pictures/keyboard.jpg")},
            new Photo { PhotoBinary = File.ReadAllBytes("Data/Pictures/boots.jpg")},
        };
        _dbContext.Photos.AddRange(photos);
        _dbContext.SaveChanges();
    }
    private void SeedItems()
    {
        var photos = _dbContext.Photos.ToList();
        if (photos == null) return;
        var items = new[]
        {
            new Item  {
                ItemName = "Laptop",
                ItemGroupId = 1,
                MeasurementUnitId = 1,
                Quantity = 20,
                PriceWithoutVat = 1200.00m,
                ItemStatusId = 2,
                StorageLocation = "Warehouse A",
                ContactPerson = "John Doe",
                PhotoId = photos[0].PhotoId,
                },
            new Item
            {
                ItemName = "Printer",
                ItemGroupId = 1,
                MeasurementUnitId = 1,
                Quantity = 10,
                PriceWithoutVat = 300.00m,
                ItemStatusId = 2,
                StorageLocation = "Warehouse B",
                ContactPerson = "Jane Smith",
                PhotoId = photos[1].PhotoId,
            },
            new Item
            {
                ItemName = "Smartphone",
                ItemGroupId = 1,
                MeasurementUnitId = 1,
                Quantity = 15,
                PriceWithoutVat = 800.00m,
                ItemStatusId = 2,
                StorageLocation = "Warehouse C",
                ContactPerson = "Alice Johnson",
                PhotoId = photos[2].PhotoId,
            },
            new Item
            {
                ItemName = "Table",
                ItemGroupId = 4,
                MeasurementUnitId = 1,
                Quantity = 12,
                PriceWithoutVat = 500.00m,
                ItemStatusId = 2,
                StorageLocation = "Warehouse B",
                ContactPerson = "Bob Williams",
                PhotoId = photos[3].PhotoId,
            },
            new Item
            {
                ItemName = "Desktop Computer",
                ItemGroupId = 1,
                MeasurementUnitId = 1,
                Quantity = 8,
                PriceWithoutVat = 1500.00m,
                ItemStatusId = 2,
                StorageLocation = "Warehouse A",
                ContactPerson = "Eva Brown",
                PhotoId = photos[4].PhotoId,
            },
            new Item
            {
                ItemName = "Scanner",
                ItemGroupId = 1,
                MeasurementUnitId = 1,
                Quantity = 5,
                PriceWithoutVat = 200.00m,
                ItemStatusId = 1,
                StorageLocation = "Warehouse C",
                ContactPerson = "David Miller",
                PhotoId = photos[5].PhotoId,
            },
            new Item
            {
                ItemName = "Drinking water",
                ItemGroupId = 6,
                MeasurementUnitId = 7,
                Quantity = 50,
                PriceWithoutVat = 1.00m,
                ItemStatusId = 1,
                StorageLocation = "Warehouse B",
                ContactPerson = "Grace Wilson",
                PhotoId = photos[6].PhotoId,
            },
            new Item
            {
                ItemName = "Tea",
                ItemGroupId = 5,
                MeasurementUnitId = 3,
                Quantity = 2,
                PriceWithoutVat = 12.00m,
                ItemStatusId = 1,
            },
            new Item
            {
                ItemName = "Coffee",
                ItemGroupId = 6,
                MeasurementUnitId = 8,
                Quantity = 2,
                PriceWithoutVat = 20.00m,
                ItemStatusId = 1,
                StorageLocation = "Warehouse A",
                ContactPerson = "Frank Harris",
                PhotoId = photos[7].PhotoId,
            },
            new Item
            {
                ItemName = "Paper",
                ItemGroupId = 5,
                MeasurementUnitId = 3,
                Quantity = 50,
                PriceWithoutVat = 20.00m,
                ItemStatusId = 1,
            },
            new Item
            {
                ItemName = "Keyboard",
                ItemGroupId = 1,
                MeasurementUnitId = 1,
                Quantity = 20,
                PriceWithoutVat = 30.00m,
                ItemStatusId = 2,
                StorageLocation = "Warehouse C",
                ContactPerson = "Sophia Lee",
                PhotoId = photos[8].PhotoId,
            },
            new Item
            {
                ItemName = "Gum boots",
                ItemGroupId = 5,
                MeasurementUnitId = 9,
                Quantity = 15,
                PriceWithoutVat = 50.00m,
                ItemStatusId = 2,
                StorageLocation = "Warehouse B",
                ContactPerson = "Mason Taylor",
                PhotoId = photos[9].PhotoId,
            },
        };

        _dbContext.Items.AddRange(items);
        _dbContext.SaveChanges();
    }


    private void SeedRequests()
    {
        var items = _dbContext.Items.ToList();
        if (items == null) return;
        var requests = new[]
        {
            new Request { EmployeeName = "Alice", ItemId = items[0].ItemId, MeasurementUnitId = 1, Quantity = 2, PriceWithoutVat = 2400.00m, Comment = "Urgent request", RequestStatusId = 1 },
            new Request { EmployeeName = "Bob", ItemId = items[1].ItemId, MeasurementUnitId = 1, Quantity = 1, PriceWithoutVat = 300.00m, Comment = "Need it for a presentation", RequestStatusId = 1 },
            new Request { EmployeeName = "Charlie", ItemId = items[2].ItemId, MeasurementUnitId = 1, Quantity = 5, PriceWithoutVat = 4000.00m, Comment = "For new employees", RequestStatusId = 1 },
            new Request { EmployeeName = "David", ItemId = items[3].ItemId, MeasurementUnitId = 1, Quantity = 3, PriceWithoutVat = 1500.00m, Comment = "For development team", RequestStatusId = 1 },
            new Request { EmployeeName = "Emma", ItemId = items[4].ItemId, MeasurementUnitId = 1, Quantity = 2, PriceWithoutVat = 3000.00m, Comment = "For marketing campaign", RequestStatusId = 1 }
        };

        _dbContext.Requests.AddRange(requests);
        _dbContext.SaveChanges();
    }
}
