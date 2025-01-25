using Microsoft.AspNetCore.Razor.Hosting;
using Microsoft.AspNetCore.SignalR;
using SignalR.BusinessLayer.Abstract;
using SignalR.DataAccessLayer.Concrete;
using SignalR.EntityLayer.Entities;

namespace SignalRApi.Hubs
{
    public class SignalRHub : Hub
    {
        private readonly ICategoryService _categoryService;
        private readonly IProductService _productService;
        private readonly IOrderService _orderService;
        private readonly IMoneyCaseService _moneyCaseService;
        private readonly IMenuTableService _menuTableService;
        private readonly IBookingService _bookingService;
        private readonly INotificationService _notificationService;

        public SignalRHub(ICategoryService categoryService, IProductService productService, IOrderService orderService, IMoneyCaseService moneyCaseService, IMenuTableService menuTableService, IBookingService bookingService, INotificationService notificationService)
        {
            _categoryService = categoryService;
            _productService = productService;
            _orderService = orderService;
            _moneyCaseService = moneyCaseService;
            _menuTableService = menuTableService;
            _bookingService = bookingService;
            _notificationService = notificationService;
        }
        public static int clientCount { get; set; } = 0;

        public async Task SendStatistics()
        {
            var categoryCount = _categoryService.TCategoryCount();
            await Clients.All.SendAsync("ReceiveCategoryCount", categoryCount);

            var productCount = _productService.TProductCount();
            await Clients.All.SendAsync("ReceiveProductCount", productCount);

            var activeCategories = _categoryService.TActiveCategoryCount();
            await Clients.All.SendAsync("ReceiveActiveCategoryCount", activeCategories);

            var passiveCategories = _categoryService.TPassiveCategoryCount();
            await Clients.All.SendAsync("ReceivePassiveCategoryCount", passiveCategories);

            var productCountByHamburgerCategory = _productService.TProductCountByCategoryNameHamburger();
            await Clients.All.SendAsync("ReceiveProductCountByHamburgerCategory", productCountByHamburgerCategory);

            var productCountByDrinkCategory = _productService.TProductCountByCategoryNameDrink();
            await Clients.All.SendAsync("ReceiveProductCountByDrinkCategory", productCountByDrinkCategory);

            var avgPrice = _productService.TAvgProductPrice();
            await Clients.All.SendAsync("ReceiveAvgProductPrice", avgPrice.ToString("0.00"));

            var maxPrice = _productService.ProductNameByMaxPrice();
            await Clients.All.SendAsync("ReceiveProductNameByMaxPrice", maxPrice);

            var minPrice = _productService.ProductNameByMinPrice();
            await Clients.All.SendAsync("ReceiveProductNameByMinPrice", minPrice);

            var avgHamburgerPrice = _productService.TAvgProductPriceByHamburger();
            await Clients.All.SendAsync("ReceiveAvgHamburgerPrice", avgHamburgerPrice.ToString("0.00"));

            var totalOrderCount = _orderService.TotalOrderCount();
            await Clients.All.SendAsync("ReceiveTotalOrderCount", totalOrderCount);

            var activeOrderCount = _orderService.TActiveOrderCount();
            await Clients.All.SendAsync("ReceiveActiveOrderCount", activeOrderCount);

            var lastOrderPrice = _orderService.TLastOrderPrice();
            await Clients.All.SendAsync("ReceiveLastOrderPrice", lastOrderPrice.ToString("0.00"));

            var moneyCase = _moneyCaseService.TotalMoneyCaseAmount();
            await Clients.All.SendAsync("ReceiveTotalMoneyCaseAmount", moneyCase.ToString("0.00"));

            var todaysEarnings = _orderService.TotalEarningsToday();
            await Clients.All.SendAsync("ReceiveTotalEarningsToday", todaysEarnings.ToString("0.00"));

            var totalTableCount = _menuTableService.TotalMenuTableCount();
            await Clients.All.SendAsync("ReceiveTotalTableCount", totalTableCount);
        }

        public async Task SendProgress()
        {
            var totalMoney = _moneyCaseService.TotalMoneyCaseAmount();
            await Clients.All.SendAsync("ReceiveTotalMoneyCaseAmount", totalMoney.ToString("0.00"));

            var activeOrders = _orderService.TActiveOrderCount();
            await Clients.All.SendAsync("ReceiveActiveOrderCount", activeOrders);

            var tableCount = _menuTableService.TotalMenuTableCount();
            await Clients.All.SendAsync("ReceiveTotalMenuTableCount", tableCount);

            var avgPrice = _productService.TAvgProductPrice();
            await Clients.All.SendAsync("ReceiveAvgProductPrice", avgPrice.ToString("0.00"));
            
            var avgHamburgerPrice = _productService.TAvgProductPriceByHamburger();
            await Clients.All.SendAsync("ReceiveAvgProductPriceByHamburger", avgHamburgerPrice.ToString("0.00"));
            
            var drinkCount = _productService.TProductCountByCategoryNameDrink();
            await Clients.All.SendAsync("ReceiveDrinkCount", drinkCount);

            var orderCount = _orderService.TotalOrderCount();
            await Clients.All.SendAsync("ReceiveTotalOrderCount", orderCount);
            
            var steakBurgerPrice = _productService.TProductPriceBySteakBurger();
            await Clients.All.SendAsync("ReceiveSteakBurgerPrice", steakBurgerPrice);

            var totalDrinkPrice = _productService.TotalPriceByDrinkCategory();
            await Clients.All.SendAsync("ReceiveTotalPriceByDrinkCategory", totalDrinkPrice);
            
            var totalSaladPrice = _productService.TotalPriceBySaladCategory();
            await Clients.All.SendAsync("ReceiveTotalPriceBySaladCategory", totalSaladPrice);
        }

        public async Task GetBookingList()
        {
            var bookings = _bookingService.TGetListAll();
            await Clients.All.SendAsync("ReceiveBookingList", bookings);
        }

        public async Task SendNotification()
        {
            var FalseStatusnotifications = _notificationService.TNotificationCountByStatusFalse();
            await Clients.All.SendAsync("ReceiveNotificationCount", FalseStatusnotifications);

            var notificationListByFalse = _notificationService.TGetAllNotificationsByFalse();
            await Clients.All.SendAsync("ReceiveNotificationListByFalse", notificationListByFalse);
        }

        public async Task GetMenuTableStatus()
        {
            var values = _menuTableService.TGetListAll();
            await Clients.All.SendAsync("ReceiveMenuTableStatus", values);
        }

        public async Task SendMessage(string user,string message)
        {
            await Clients.All.SendAsync("ReceiveMessage", user, message);

            
        }

        public override async Task OnConnectedAsync()
        {
            clientCount++;
            await Clients.All.SendAsync("ReceiveClientCount",clientCount);
            await base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            clientCount--;
            await Clients.All.SendAsync("ReceiveClientCount", clientCount);
            await base.OnDisconnectedAsync(exception);
        }
    }
}
