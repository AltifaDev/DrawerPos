using DrawerPos.Shared;

namespace DrawerPos.Blazor.Services
{
    public static class OrderHelper
    {
        public static OrderItem ToOrderItem(CartItem cartItem, string billNo = "") // Regular static method
        {
            return new OrderItem
            {
                BillNo = billNo,
                // ... (rest of the mapping)
            };
        }
    }
}
